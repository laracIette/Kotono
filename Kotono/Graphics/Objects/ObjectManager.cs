using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Globalization;
using Path = Kotono.Utils.Path;

namespace Kotono.Graphics.Objects
{
    public static class ObjectManager
    {
        public static readonly List<IMesh> Meshes = new List<IMesh>();
        public static readonly List<ILight> Lights = new List<ILight>();

        private static readonly Dictionary<string, Tuple<int, int, int>> _paths = new Dictionary<string, Tuple<int, int, int>>();

        public static void LoadMeshOBJ(string path, Vector3 position, Vector3 angle, Vector3 scale, string diffusePath, string specularPath)
        {
            var diffuseMap = TextureManager.LoadTexture(diffusePath);
            var specularMap = TextureManager.LoadTexture(specularPath);

            if (!_paths.ContainsKey(path))
            {
                var positions = new List<Vector3>();
                var texCoords = new List<Vector2>();
                var normals = new List<Vector3>();
                var vertices = new List<Vertex>();

                foreach (var line in File.ReadAllLines(Path.Assets + path))
                {
                    string[] tokens = line.Split(' ');

                    switch (tokens[0])
                    {
                        case "v":
                            var pos = new Vector3(
                                float.Parse(tokens[1], CultureInfo.InvariantCulture),
                                float.Parse(tokens[2], CultureInfo.InvariantCulture),
                                float.Parse(tokens[3], CultureInfo.InvariantCulture)
                            );
                            positions.Add(pos);
                            break;

                        case "vt":
                            var texCoord = new Vector2(
                                float.Parse(tokens[1], CultureInfo.InvariantCulture),
                                float.Parse(tokens[2], CultureInfo.InvariantCulture)
                            );
                            texCoords.Add(texCoord);
                            break;

                        case "vn":
                            var normal = new Vector3(
                                float.Parse(tokens[1], CultureInfo.InvariantCulture),
                                float.Parse(tokens[2], CultureInfo.InvariantCulture),
                                float.Parse(tokens[3], CultureInfo.InvariantCulture)
                            );
                            normals.Add(normal);
                            break;

                        case "f":
                            for (int i = 1; i < tokens.Length; i++)
                            {
                                string[] vertexTokens = tokens[i].Split('/');

                                var vertex = new Vertex
                                {
                                    position = positions[int.Parse(vertexTokens[0]) - 1],
                                    texCoords = texCoords[int.Parse(vertexTokens[1]) - 1],
                                    normal = normals[int.Parse(vertexTokens[2]) - 1]
                                };
                                vertices.Add(vertex);
                            }
                            break;
                    }

                }

                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vertex.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                int positionAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int texCoordAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vector3.SizeInBytes);

                int normalAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vector3.SizeInBytes + Vector2.SizeInBytes);

                _paths[path] = Tuple.Create(vertexArrayObject, vertexBufferObject, vertices.Count);
            }

            Meshes.Add(new MeshOBJ(_paths[path].Item1, _paths[path].Item2, _paths[path].Item3, position, angle, scale, diffuseMap, specularMap));
        }

        public static void LoadPointLight()
        {

        }
    }
}