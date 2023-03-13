using Assimp;

using System.Globalization;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Path = Kotono.Utils.Path;


namespace Kotono.Graphics.Objects
{
    public static class ObjectManager
    {
        public static readonly List<IMesh> Meshes = new();
        public static readonly List<PointLight> PointLights = new();

        private static readonly Dictionary<string, Tuple<int, int, int>> _paths = new();

        public static void LoadMeshOBJ(string path, Vector3 position, Vector3 angle, Vector3 scale, string diffusePath, string specularPath)
        {
            var diffuseMap = TextureManager.LoadTexture(diffusePath);
            var specularMap = TextureManager.LoadTexture(specularPath);

            if (!_paths.ContainsKey(path))
            {
                AssimpContext importer = new();
                Scene scene = importer.ImportFile(Path.Assets + path, PostProcessSteps.Triangulate);

                // Extract the vertices from the mesh
                Mesh mesh = scene.Meshes[0];
                Vector3D[] positions = mesh.Vertices.ToArray();
                Vector2D[] texCoords = mesh.TextureCoordinateChannels[0].Select(v => new Vector2D(v.X, v.Y)).ToArray();
                Vector3D[] normals = mesh.Normals.ToArray();

                // Combine the vertex data into a single array
                Vertex[] vertices = new Vertex[mesh.VertexCount];
                for (int i = 0; i < mesh.VertexCount; i++)
                {
                    vertices[i].Position = positions[i];
                    vertices[i].TexCoord = texCoords[i];
                    vertices[i].Normal = normals[i];
                }

                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vertex.SizeInBytes, vertices, BufferUsageHint.StaticDraw);

                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                int positionAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int texCoordAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int normalAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 5);

                _paths[path] = Tuple.Create(vertexArrayObject, vertexBufferObject, vertices.Length);
            }

            Meshes.Add(new MeshOBJ(_paths[path].Item1, _paths[path].Item2, _paths[path].Item3, position, angle, scale, diffuseMap, specularMap));
        }

        public static void LoadPointLight(Vector3 position)
        {
            PointLights.Add(new PointLight(position));
        }
    }
}