using Assimp;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed record class ModelData : IDisposable
    {
        private sealed record class Data(Vertex3D[] Vertices, int[] Indices, ModelTriangle[] Triangles);

        private static readonly Dictionary<string, Data[]> _datas = [];

        internal VertexArraySetup VertexArraySetup { get; } = new();

        internal ElementBufferObject ElementBufferObject { get; } = new();

        internal Vector Center { get; }

        internal Vector[] Vertices { get; }

        internal ModelTriangle[] Triangles { get; }

        internal int[] Indices { get; }

        internal int IndicesCount => Indices.Length;

        private ModelData(Data data)
        {
            Vertices = [.. data.Vertices.Select(v => v.Pos).Distinct()];
            Center = Vector.Avg(Vertices);
            Indices = data.Indices;
            Triangles = data.Triangles;

            VertexArraySetup.SetData(data.Vertices, Vertex3D.SizeInBytes);
            ElementBufferObject.SetData(data.Indices, sizeof(int));
        }

        internal void Draw()
        {
            VertexArraySetup.VertexArrayObject.Bind();
            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public void Dispose()
        {
            VertexArraySetup.Dispose();
            ElementBufferObject.Dispose();
        }

        /// <summary>
        /// Get all the <see cref="ModelData"/>s of the input model path.
        /// </summary>
        internal static ModelData[] Parse(string path)
        {
            using var importer = new AssimpContext();
            var scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.CalculateTangentSpace);
            
            if (!_datas.TryGetValue(path, out var data))
            {
                data = new Data[scene.MeshCount];

                for (int i = 0; i < scene.MeshCount; i++)
                {
                    var mesh = scene.Meshes[i];

                    var vertices = new Vertex3D[mesh.VertexCount];
                    var indices = mesh.GetIndices();
                    var triangles = new ModelTriangle[mesh.FaceCount];

                    for (int j = 0; j < mesh.VertexCount; j++)
                    {
                        vertices[j] = new Vertex3D
                        {
                            Pos = (Vector)mesh.Vertices[j],
                            Normal = (Vector)mesh.Normals[j],
                            Tangent = (Vector)mesh.Tangents[j],
                            TexCoords = (Point)mesh.TextureCoordinateChannels[0][j]
                        };
                    }

                    for (int j = 0; j < mesh.FaceCount; j++)
                    {
                        var face = mesh.Faces[j];
                        triangles[j] = new ModelTriangle(
                            (Vector)mesh.Vertices[face.Indices[0]],
                            (Vector)mesh.Vertices[face.Indices[1]],
                            (Vector)mesh.Vertices[face.Indices[2]]
                        );
                    }

                    data[i] = new Data(vertices, indices, triangles);
                }

                _datas[path] = data;
            }

            return [.. data.Select(d => new ModelData(d))];
        }
    }
}
