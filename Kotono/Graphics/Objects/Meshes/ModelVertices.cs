using Assimp;
using Kotono.Utils.Coordinates;
using System.Linq;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class ModelVertices
    {
        internal string Path { get; }

        internal Vector Center { get; }

        internal Vector[] Vertices { get; }

        internal ModelTriangle[] Triangles { get; }

        internal int[] Indices { get; }

        internal int IndicesCount => Indices.Length;

        internal VertexArraySetup VertexArraySetup { get; } = new();

        internal ModelVertices(string path)
        {
            Path = path;

            LoadModelData(out var vertices, out var indices, out var triangles);

            var modelVertices = vertices[0];
            var modelIndices = indices[0];
            var modelTriangles = triangles[0];

            Vertices = [.. modelVertices.Select(v => v.Location).Distinct()];
            Center = Vector.Avg(Vertices);
            Indices = modelIndices;
            Triangles = modelTriangles;

            VertexArraySetup.SetData(modelVertices, Vertex3D.SizeInBytes);
        }

        private void LoadModelData(out Vertex3D[][] vertices, out int[][] indices, out ModelTriangle[][] triangles)
        {
            using var importer = new AssimpContext();
            var scene = importer.ImportFile(Path, PostProcessSteps.Triangulate | PostProcessSteps.CalculateTangentSpace);

            triangles = new ModelTriangle[scene.MeshCount][];
            vertices = new Vertex3D[scene.MeshCount][];
            indices = new int[scene.MeshCount][];

            for (int i = 0; i < scene.MeshCount; i++)
            {
                var mesh = scene.Meshes[i];

                triangles[i] = new ModelTriangle[mesh.FaceCount];
                vertices[i] = new Vertex3D[mesh.VertexCount];
                indices[i] = mesh.GetIndices();

                for (int j = 0; j < mesh.FaceCount; j++)
                {
                    var face = mesh.Faces[j];
                    triangles[i][j] = new ModelTriangle(
                        this,
                        (Vector)mesh.Vertices[face.Indices[0]],
                        (Vector)mesh.Vertices[face.Indices[1]],
                        (Vector)mesh.Vertices[face.Indices[2]]
                    );
                }

                for (int j = 0; j < mesh.VertexCount; j++)
                {
                    vertices[i][j] = new Vertex3D
                    {
                        Location = (Vector)mesh.Vertices[j],
                        Normal = (Vector)mesh.Normals[j],
                        Tangent = (Vector)mesh.Tangents[j],
                        TexCoord = (Point)mesh.TextureCoordinateChannels[0][j]
                    };
                }
            }
        }
    }
}
