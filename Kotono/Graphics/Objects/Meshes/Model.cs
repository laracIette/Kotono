using Assimp;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Model
    {
        private static readonly Dictionary<string, Model> _models = [];

        internal string Path { get; }

        internal VertexArraySetup VertexArraySetup { get; } = new();

        internal int IndicesCount { get; }

        internal Vector Center { get; }

        internal Vector[] Vertices { get; }

        internal ModelTriangle[] Triangles { get; }

        private Model(ModelSettings settings)
        {
            Path = settings.Path;

            Vertex3D[][] models;
            int[][] indices;

            using (var importer = new AssimpContext())
            {
                var scene = importer.ImportFile(Path, PostProcessSteps.Triangulate);

                Triangles = new ModelTriangle[scene.Meshes[0].Faces.Count];

                for (int i = 0; i < scene.Meshes[0].Faces.Count; i++)
                {
                    var face = scene.Meshes[0].Faces[i];

                    Triangles[i] = new ModelTriangle(
                        this,
                        (Vector)scene.Meshes[0].Vertices[face.Indices[0]],
                        (Vector)scene.Meshes[0].Vertices[face.Indices[1]],
                        (Vector)scene.Meshes[0].Vertices[face.Indices[2]]
                    );
                }

                models = new Vertex3D[scene.Meshes.Count][];
                indices = new int[scene.Meshes.Count][];

                for (int i = 0; i < scene.Meshes.Count; i++)
                {
                    var mesh = scene.Meshes[i];

                    models[i] = new Vertex3D[mesh.Vertices.Count];

                    for (int j = 0; j < mesh.Vertices.Count; j++)
                    {
                        var loc = new Vector(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                        var normal = new Vector(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                        var texCoord = new Point(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                        models[i][j] = new Vertex3D(loc, normal, texCoord);
                    }

                    indices[i] = mesh.GetIndices();
                }
            }

            IndicesCount = indices[0].Length;

            var vertices = new List<Vector>();
            foreach (var vertex in models[0])
            {
                if (!vertices.Any(v => v == vertex.Location))
                {
                    vertices.Add(vertex.Location);
                }
            }
            Vertices = [.. vertices];

            Center = Vector.Zero;
            foreach (var vertex in Vertices)
            {
                Center += vertex;
            }
            Center /= Vertices.Length;

            VertexArraySetup.VertexArrayObject.Bind();
            VertexArraySetup.VertexBufferObject.SetData(models[0], Vertex3D.SizeInBytes);

            int locationAttributeLocation = settings.Shader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, 0);

            int normalAttributeLocation = settings.Shader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalAttributeLocation);
            GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, Vector.SizeInBytes);

            int texCoordAttributeLocation = settings.Shader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordAttributeLocation);
            GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, Vector.SizeInBytes * 2);

            // Create element buffer
            int elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IndicesCount * sizeof(int), indices[0], BufferUsageHint.StaticDraw);
        }

        internal static Model Load(ModelSettings settings)
        {
            if (!_models.TryGetValue(settings.Path, out Model? value))
            {
                value = new Model(settings);

                _models[settings.Path] = value;
            }

            return value;
        }

        internal void Draw()
        {
            VertexArraySetup.VertexArrayObject.Bind();
            VertexArraySetup.VertexBufferObject.Bind();

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
