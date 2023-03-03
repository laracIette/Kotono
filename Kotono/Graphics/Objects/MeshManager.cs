using Kotono.Graphics.Objects.Meshes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Globalization;
using Path = Kotono.Utils.Path;

namespace Kotono.Graphics.Objects
{
    public static class MeshManager
    {
        public static List<IMesh> _meshes = new List<IMesh>();

        public static void LoadMeshOBJ(string path, Vector3 position, Vector3 angle)
        {
            string[] lines = File.ReadAllLines(Path.Assets + path);

            var positions = new List<Vector3>();
            var texCoords = new List<Vector2>();
            var normals = new List<Vector3>();
            var indices = new List<uint>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.StartsWith("v "))
                {
                    string[] parts = line.Split(' ');

                    float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[3], CultureInfo.InvariantCulture);

                    positions.Add(new Vector3(x, y, z));
                }
                else if (line.StartsWith("vt "))
                {
                    string[] parts = line.Split(' ');

                    float u = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float v = float.Parse(parts[2], CultureInfo.InvariantCulture);

                    texCoords.Add(new Vector2(u, v));
                }
                else if (line.StartsWith("vn "))
                {
                    string[] parts = line.Split(' ');

                    float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[3], CultureInfo.InvariantCulture);

                    normals.Add(new Vector3(x, y, z));
                }
                else if (line.StartsWith("f "))
                {
                    string[] parts = line.Split(' ');

                    for (int j = 1; j < 5; j++)
                    {
                        string[] indicesParts = parts[j].Split('/');

                        uint positionIndex = uint.Parse(indicesParts[0]) - 1;
                        uint texCoordIndex = uint.Parse(indicesParts[1]) - 1;
                        uint normalIndex = uint.Parse(indicesParts[2]) - 1;

                        indices.Add(positionIndex);
                        indices.Add(texCoordIndex);
                        indices.Add(normalIndex);
                    }
                }
            }


            var vertices = new List<float>();

            for (int i = 0; i < positions.Count; i++)
            {
                vertices.Add(positions[i].X);
                vertices.Add(positions[i].Y);
                vertices.Add(positions[i].Z);

                if (texCoords.Count > i)
                {
                    vertices.Add(texCoords[i].X);
                    vertices.Add(texCoords[i].Y);
                }

                if (normals.Count > i)
                {
                    vertices.Add(normals[i].X);
                    vertices.Add(normals[i].Y);
                    vertices.Add(normals[i].Z);
                }
            }

            int vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(), BufferUsageHint.StaticDraw);

            int vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            int positionAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionAttributeLocation);
            GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            int texCoordAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aTexCoords");
            if (texCoordAttributeLocation != -1)
            {
                GL.EnableVertexAttribArray(texCoordAttributeLocation);
                GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            }

            int normalAttributeLocation = ShaderManager.LightingShader.GetAttribLocation("aNormal");
            if (normalAttributeLocation != -1)
            {
                GL.EnableVertexAttribArray(normalAttributeLocation);
                GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
            }

            int indexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);

            _meshes.Add(new MeshOBJ(vertexArrayObject, indexBufferObject, indices.Count, position, angle));
        }
    }
}
