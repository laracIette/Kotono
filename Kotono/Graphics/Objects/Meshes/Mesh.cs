using Assimp;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Shapes;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;

namespace Kotono.Graphics.Objects.Meshes
{
    internal abstract class Mesh : Object3D, IMesh
    {
        private class FileSettings
        {
            internal int VertexArrayObject;

            internal int VertexBufferObject;

            internal int IndicesCount;

            internal Vector Center;

            internal Vector[] Vertices = [];

            internal Triangle[] Triangles = [];
        }

        private static readonly Dictionary<string, FileSettings> _paths = [];

        private readonly FileSettings _fileSettings;

        private readonly List<Hitbox> _hitboxes;

        private readonly string _model;

        protected readonly Shader _shader;

        internal PBRMaterial Material { get; }

        internal bool IsGravity { get; set; } = false;

        internal CollisionState CollisionState { get; set; }

        internal int VertexArrayObject => _fileSettings.VertexArrayObject;

        internal int VertexBufferObject => _fileSettings.VertexBufferObject;

        internal int IndicesCount => _fileSettings.IndicesCount;

        internal Vector Center => _fileSettings.Center;

        internal Vector[] Vertices => _fileSettings.Vertices;

        internal Triangle[] Triangles => _fileSettings.Triangles;

        public override Vector RotationVelocity
        {
            get => Vector.Deg(base.RotationVelocity);
            set => base.RotationVelocity = Vector.Rad(value);
        }

        public bool IsFizix { get; set; } = false;

        public float LastIntersectionCheckTime { get; private set; } = 0.0f;

        public Vector IntersectionLocation { get; private set; } = Vector.Zero;

        public float IntersectionDistance { get; private set; } = 0.0f;

        public override bool IsHovered
        {
            get
            {
                if (Time.Now - LastIntersectionCheckTime > IMesh.IntersectionCheckFrequency)
                {
                    LastIntersectionCheckTime = Time.Now;

                    IntersectionLocation = Vector.Zero;
                    IntersectionDistance = 0.0f;

                    foreach (var triangle in Triangles)
                    {
                        triangle.Transform = Transform;

                        if (Intersection.IntersectRayTriangle(ObjectManager.ActiveCamera.Location, Mouse.Ray, triangle, out Vector intersectionLocation, out float intersectionDistance))
                        {
                            IntersectionLocation = intersectionLocation;
                            IntersectionDistance = intersectionDistance;

                            return true;
                        }
                    }
                }

                return false;
            }
        }

        internal Mesh(MeshSettings settings)
            : base(settings)
        {
            _model = settings.Model;
            _hitboxes = settings.Hitboxes;
            Color = settings.Color;

            Material = settings.Textures.Length switch
            {
                1 => new PBRMaterial
                {
                    Albedo = new Texture(settings.Textures[0], TextureUnit.Texture0),
                },
                2 => new PBRMaterial
                {
                    Albedo = new Texture(settings.Textures[0], TextureUnit.Texture0),
                    Roughness = new Texture(settings.Textures[1], TextureUnit.Texture1)
                },
                _ => new PBRMaterial()
            };

            _shader = settings.Shader switch
            {
                "lighting" => ShaderManager.Lighting,
                "pointLight" => ShaderManager.PointLight,
                "gizmo" => ShaderManager.Gizmo,
                "flatTexture" => ShaderManager.FlatTexture,
                _ => throw new SwitchException(typeof(string), settings.Shader)
            };

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = Location;
                hitbox.Rotation = Vector.Zero;
                hitbox.Scale = Scale * 2;
                hitbox.Color = Color.Red;
            }

            if (!_paths.TryGetValue(_model, out FileSettings? value))
            {
                List<Vertex>[] models;
                List<int>[] indices;
                List<Triangle> triangles = [];

                using (var importer = new AssimpContext())
                {
                    var scene = importer.ImportFile(_model, PostProcessSteps.Triangulate);

                    foreach (var face in scene.Meshes[0].Faces)
                    {
                        triangles.Add(new Triangle(
                            (Vector)scene.Meshes[0].Vertices[face.Indices[0]],
                            (Vector)scene.Meshes[0].Vertices[face.Indices[1]],
                            (Vector)scene.Meshes[0].Vertices[face.Indices[2]],
                            Transform.Default,
                            Color.White
                        ));
                    }

                    models = new List<Vertex>[scene.Meshes.Count];
                    indices = new List<int>[scene.Meshes.Count];
                    for (int i = 0; i < scene.Meshes.Count; i++)
                    {
                        var mesh = scene.Meshes[i];
                        var tempVertices = new List<Vertex>();

                        for (int j = 0; j < mesh.Vertices.Count; j++)
                        {
                            var loc = new Vector(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                            var normal = new Vector(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                            var texCoord = new Point(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                            tempVertices.Add(new Vertex(loc, normal, texCoord));
                        }

                        models[i] = tempVertices;
                        indices[i] = [.. mesh.GetIndices()];
                    }
                }

                var center = Vector.Zero;
                models[0].ForEach(v => center += v.Location);
                center /= models[0].Count;

                var vertices = new List<Vector>();
                foreach (var vertex in models[0])
                {
                    if (!vertices.Any(v => v == vertex.Location))
                    {
                        vertices.Add(vertex.Location);
                    }
                }

                // Create vertex array
                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                // Create vertex buffer
                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, models[0].Count * Vertex.SizeInBytes, models[0].ToArray(), BufferUsageHint.StaticDraw);

                int locationAttributeLocation = _shader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(locationAttributeLocation);
                GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int normalAttributeLocation = _shader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int texCoordAttributeLocation = _shader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

                // Create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.StaticDraw);

                value = new FileSettings
                {
                    VertexArrayObject = vertexArrayObject,
                    VertexBufferObject = vertexBufferObject,
                    IndicesCount = indices[0].Count,
                    Center = center,
                    Vertices = [.. vertices],
                    Triangles = [.. triangles]
                };

                _paths[settings.Model] = value;
            }

            _fileSettings = value;
        }

        public override void Update()
        {
            var tempLoc = Location;

            if (IsGravity)
            {
                tempLoc += Fizix.Gravity * Time.Delta;
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = tempLoc;

                if ((CollisionState == CollisionState.BlockAll) && hitbox.IsColliding)
                {
                    hitbox.Location = Location;
                    tempLoc = Location;
                }
            }

            Location = tempLoc;

            if (Mouse.IsButtonPressed(MouseButton.Left))
            {
                OnMouseLeftButtonPressed();
            }

            Color = IsSelected ? (IsActive ? Color.Green : Color.Orange) : Color.White;
        }

        public void UpdateFizix()
        {
            Fizix.Update(this);
        }

        public override void Draw()
        {
            Material.Use();

            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void OnMouseLeftButtonPressed()
        {
            // If gizmo isn't selected
            if (!Gizmo.IsSelected)
            {
                // If mesh is clicked
                if (IsHovered)
                {
                    // If left control is down
                    if (Keyboard.IsKeyDown(Keys.LeftControl))
                    {
                        IsSelected = true;
                        ISelectable.Selected.Remove(this);
                    }
                    // If left control is up
                    else
                    {
                        IsSelected = !IsSelected;
                    }
                }

                // If mesh isn't clicked
                else
                {
                    // If left control is up
                    if (!Keyboard.IsKeyDown(Keys.LeftControl))
                    {
                        IsSelected = false;
                    }
                }
            }

            // If mesh is in ISelectable.Selected
            if (ISelectable.Selected.Contains(this))
            {
                // If mesh isn't selected
                if (!IsSelected)
                {
                    ISelectable.Selected.Remove(this);
                }
            }
            // If mesh isn't in ISelectable.Selected and is selected
            else if (IsSelected)
            {
                ISelectable.Selected.Add(this);
            }
        }

        public override void Save()
        {
            if (_settings is MeshSettings settings)
            {
                settings.Model = _model;
                settings.Shader = _shader.Name;
                settings.Textures = Material.Paths;
            }

            base.Save();
        }

        public override void Delete()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Delete();
            }

            base.Delete();
        }
    }
}