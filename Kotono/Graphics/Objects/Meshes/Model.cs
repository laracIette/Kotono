using Assimp;
using Kotono.Graphics.Objects.Shapes;
using Kotono.Utils;
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

        internal int VertexArrayObject { get; }

        internal int VertexBufferObject { get; }

        internal int IndicesCount { get; }

        internal Vector Center { get; }

        internal Vector[] Vertices { get; }

        internal Triangle[] Triangles { get; }

        private Model(ModelSettings settings)
        {
            Path = settings.Path;

            List<Vertex>[] models;
            List<int>[] indices;
            List<Triangle> triangles = [];

            using (var importer = new AssimpContext())
            {
                var scene = importer.ImportFile(Path, PostProcessSteps.Triangulate);

                foreach (var face in scene.Meshes[0].Faces)
                {
                    triangles.Add(new Triangle(
                        (Vector)scene.Meshes[0].Vertices[face.Indices[0]],
                        (Vector)scene.Meshes[0].Vertices[face.Indices[1]],
                        (Vector)scene.Meshes[0].Vertices[face.Indices[2]],
                        Transform.Default,
                        Color.White
                    ));
                    triangles[^1].IsDraw = false;
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

            Triangles = [.. triangles];

            IndicesCount = indices[0].Count;

            Center = Vector.Zero;
            foreach (var vertex in models[0])
            {
                Center += vertex.Location;
            }
            Center /= models[0].Count;

            var vertices = new List<Vector>();
            foreach (var vertex in models[0])
            {
                if (!vertices.Any(v => v == vertex.Location))
                {
                    vertices.Add(vertex.Location);
                }
            }
            Vertices = [.. vertices];

            // Create vertex array
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Create vertex buffer
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, models[0].Count * Vertex.SizeInBytes, models[0].ToArray(), BufferUsageHint.StaticDraw);

            int locationAttributeLocation = settings.Shader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

            int normalAttributeLocation = settings.Shader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalAttributeLocation);
            GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

            int texCoordAttributeLocation = settings.Shader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordAttributeLocation);
            GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

            // Create element buffer
            int elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IndicesCount * sizeof(int), indices[0].ToArray(), BufferUsageHint.StaticDraw);
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
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
