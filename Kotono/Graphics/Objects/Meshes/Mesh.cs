using Assimp;
using Kotono.File;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Shapes;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    internal abstract class Mesh : Object3D, IMesh
    {
        private struct MeshHiddenSettings
        {
            internal int VertexArrayObject;

            internal int VertexBufferObject;

            internal int IndicesCount;

            internal Vector Center;

            internal Vector[] Vertices;

            internal Triangle[] Triangles;
        }

        private readonly MeshHiddenSettings _meshSettings;

        private static readonly Dictionary<string, MeshHiddenSettings> _paths = [];

        private readonly List<Hitbox> _hitboxes;

        private readonly string _model;

        private readonly Texture[] _textures;

        private bool _isMouseOn = false;

        private Vector _intersectionLocation = Vector.Zero;

        private float _distance = 0.0f;

        protected readonly Shader _shader;

        internal bool IsGravity { get; set; } = false;

        internal CollisionState CollisionState { get; set; }

        internal int VertexArrayObject => _meshSettings.VertexArrayObject;

        internal int VertexBufferObject => _meshSettings.VertexBufferObject;

        internal int IndicesCount => _meshSettings.IndicesCount;

        internal Vector Center => _meshSettings.Center;

        internal Vector[] Vertices => _meshSettings.Vertices;

        internal Triangle[] Triangles => _meshSettings.Triangles;

        private Vector _rotationVelocity;

        internal Vector RotationVelocity
        {
            get => Vector.Deg(_rotationVelocity);
            set => _rotationVelocity = Vector.Rad(value);
        }

        internal static float IntersectionCheckFrequency => 0.1f;

        internal float LastIntersectionCheckTime { get; set; } = 0.0f;

        public bool IsFizix { get; set; } = false;

        internal Mesh(MeshSettings settings)
            : base(settings)
        {
            _model = settings.Model;
            Color = settings.Color;
            _hitboxes = settings.Hitboxes;

            _textures = new Texture[settings.Textures.Length];
            for (int i = 0; i < _textures.Length; i++)
            {
                _textures[i] = new Texture(settings.Textures[i], TextureUnit.Texture0 + i);
            }

            _shader = settings.Shader switch
            {
                "lighting" => ShaderManager.Lighting,
                "pointLight" => ShaderManager.PointLight,
                "gizmo" => ShaderManager.Gizmo,
                "flatTexture" => ShaderManager.FlatTexture,
                _ => throw new Exception($"error: Shader \"{settings.Shader}\" isn't valid.")
            };

            foreach (var hitbox in _hitboxes)
            {
                hitbox.Location = Location;
                hitbox.Rotation = Vector.Zero;
                hitbox.Scale = Scale * 2;
                hitbox.Color = Color.Red;
            }

            if (!_paths.TryGetValue(_model, out MeshHiddenSettings value))
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

                // create vertex array
                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                // create vertex buffer
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

                // create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.StaticDraw);

                value = new MeshHiddenSettings
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

            _meshSettings = value;
        }

        public override void Update()
        {
            Gizmo.TryAttachTo(this);

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
        }

        public void UpdateFizix()
        {
            Fizix.Update(this);
        }

        public override void Draw()
        {
            foreach (var texture in _textures)
            {
                texture.Use();
            }

            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <summary>
        /// Get whether the mouse intersects the Mesh.
        /// </summary>
        /// <param name="intersectionLocation"> The location at which the mouse intersects the mesh. </param>
        /// <param name="distance"> The distance of the intersectionLocation from the Camera. </param>
        /// <returns> <see langword="true"/> if the mouse interects the Mesh, else returns <see langword="false"/>. </returns>
        public bool IsMouseOn(out Vector intersectionLocation, out float distance)
        {
            if (Time.Now - LastIntersectionCheckTime > IntersectionCheckFrequency)
            {
                LastIntersectionCheckTime = Time.Now;

                _isMouseOn = false;
                _intersectionLocation = Vector.Zero;
                _distance = 0.0f;

                foreach (var triangle in Triangles)
                {
                    triangle.Transform = Transform;
                    if (Intersection.IntersectRayTriangle(CameraManager.ActiveCamera.Location, Mouse.Ray, triangle, out _intersectionLocation, out _distance))
                    {
                        _isMouseOn = true;
                        break;
                    }
                }
            }

            intersectionLocation = _intersectionLocation;
            distance = _distance;

            return _isMouseOn;
        }

        public override void Save()
        {
            if (_settings is MeshSettings settings)
            {
                settings.Model = _model;
                settings.Shader = _shader.Name;
                settings.Textures = _textures.Select(t => t.Path).ToArray();
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