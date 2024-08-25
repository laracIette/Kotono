using Assimp;
using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class ModelVertices
    {
        internal string Path { get; }

        internal Vector Center { get; }

        internal Vector[] Vertices { get; }

        internal ModelTriangle[] Triangles { get; }

        internal VertexArraySetup VertexArraySetup { get; } = new();

        internal int[] Indices { get; }

        internal int IndicesCount { get; }

        internal ModelVertices(string path)
        {
            Path = path;

            Vertex3D[][] models;
            int[][] indices;

            using (var importer = new AssimpContext())
            {
                var scene = importer.ImportFile(Path, PostProcessSteps.Triangulate | PostProcessSteps.CalculateTangentSpace);

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
                        var location = new Vector(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                        var normal = new Vector(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                        var tangent = new Vector(mesh.Tangents[j].X, mesh.Tangents[j].Y, mesh.Tangents[j].Z);
                        var texCoord = new Point(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                        models[i][j] = new Vertex3D
                        {
                            Location = location,
                            Normal = normal,
                            Tangent = tangent,
                            TexCoord = texCoord
                        };
                    }

                    indices[i] = mesh.GetIndices();
                }
            }

            var modelVertices = models[0];
            var modelIndices = indices[0];

            var vertices = new List<Vector>();
            foreach (var vertex in modelVertices)
            {
                if (!vertices.Contains(vertex.Location))
                {
                    vertices.Add(vertex.Location);
                }
            }
            Vertices = [.. vertices];

            Indices = modelIndices;
            IndicesCount = modelIndices.Length;

            Center = Vector.Avg(Vertices);

            VertexArraySetup.VertexArrayObject.Bind();
            VertexArraySetup.VertexBufferObject.SetData(modelVertices, Vertex3D.SizeInBytes);
        }

        internal void Draw()
        {
            VertexArraySetup.VertexArrayObject.Bind();
            VertexArraySetup.VertexBufferObject.Bind();

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
