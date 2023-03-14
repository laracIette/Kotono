using Assimp;

using System.Globalization;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Path = Kotono.Utils.Path;
using System.Reflection;


namespace Kotono.Graphics.Objects
{
    public static class ObjectManager
    {
        public static readonly List<IMesh> Meshes = new();
        public static readonly List<PointLight> PointLights = new();

        private static readonly Dictionary<string, Tuple<int, int, int>> _paths = new();

        //private static int ID = 0;

        public static void LoadMeshOBJ(string path, Vector3 position, Vector3 angle, Vector3 scale, string diffusePath, string specularPath, Shader shader, Vector4 color)
        {
            var diffuseMap = TextureManager.LoadTexture(diffusePath);
            var specularMap = TextureManager.LoadTexture(specularPath);

            if (!_paths.ContainsKey(path))
            {
                List<Vertex>[] models;
                List<int>[] indices;

                using (var importer = new AssimpContext())
                {
                    var scene = importer.ImportFile(Path.Assets + path, PostProcessSteps.Triangulate);

                    models = new List<Vertex>[scene.Meshes.Count];
                    indices = new List<int>[scene.Meshes.Count];
                    for (int i = 0; i < scene.Meshes.Count; i++)
                    {
                        var mesh = scene.Meshes[i];
                        var vertices = new List<Vertex>();

                        for (int j = 0; j < mesh.Vertices.Count; j++)
                        {
                            var pos = new Vector3(mesh.Vertices[j].X, mesh.Vertices[j].Y, mesh.Vertices[j].Z);
                            var normal = new Vector3(mesh.Normals[j].X, mesh.Normals[j].Y, mesh.Normals[j].Z);
                            var texCoord = new Vector2(mesh.TextureCoordinateChannels[0][j].X, mesh.TextureCoordinateChannels[0][j].Y);

                            vertices.Add(new Vertex(pos, normal, texCoord));
                        }

                        models[i] = vertices;
                        indices[i] = mesh.GetIndices().ToList();
                    }
                }

                // create vertex array
                int vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayObject);

                // create vertex buffer
                int vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, models[0].Count * Vertex.SizeInBytes, models[0].ToArray(), BufferUsageHint.DynamicDraw);

                int positionAttributeLocation = shader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

                int normalAttributeLocation = shader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 3);

                int texCoordAttributeLocation = shader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, sizeof(float) * 6);

                // create element buffer
                int elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices[0].Count * sizeof(int), indices[0].ToArray(), BufferUsageHint.DynamicDraw);

                _paths[path] = Tuple.Create(vertexArrayObject, vertexBufferObject, indices[0].Count);
            }

            Meshes.Add(new MeshOBJ(_paths[path].Item1, _paths[path].Item2, _paths[path].Item3, position, angle, scale, diffuseMap, specularMap, shader, color));
        }

        public static void LoadPointLight(Vector3 position)
        {
            PointLights.Add(new PointLight(position, Meshes.Count));
            LoadMeshOBJ("sphere.obj", position, Vector3.Zero, new Vector3(0.2f), "white.png", "white.png", ShaderManager.PointLight, new Vector4(0.0f, 0.0f, 1.0f, 1.0f));
        }

        public static void RemovePointLight(int index)
        {
            Meshes.RemoveAt(PointLights[index].MeshIndex);
            PointLights.RemoveAt(index);
        }
    }
}