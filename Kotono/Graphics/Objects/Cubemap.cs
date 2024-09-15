//#define USE_MODEL

using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Shaders;
using Kotono.Graphics.Textures;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects
{
#if USE_MODEL
    internal sealed class Cubemap : Drawable, ICubemap
    {
        private static readonly Model _model = new(Path.FromAssets(@"Meshes\cube.obj"));

        internal CubemapTexture Texture { get; set; } = new(Path.FromAssets(@"Default\Textures\Cubemap"));

        public override Shader Shader => CubemapShader.Instance;

        public override bool IsHovered => false;

        public override bool IsActive => false;

        static Cubemap() => _model.SetVertexAttributesLayout(CubemapShader.Instance);

        public override void UpdateShader()
        {
            if (Shader is CubemapShader cubemapShader)
            {
                cubemapShader.SetSkybox(Texture.TextureUnit);
            }
        }

        public override void Draw()
        {
            Texture.Use();

            _model.Draw();

            ITexture.Unbind(TextureTarget.TextureCubeMap);
        }
    }
#else
    internal sealed class Cubemap : Drawable, ICubemap
    {
        private static readonly float[] _vertices =
        [
            -1.0f, -1.0f, -1.0f, // Front face
             1.0f, -1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,

            -1.0f, -1.0f,  1.0f, // Back face
             1.0f, -1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,

            -1.0f,  1.0f,  1.0f, // Left face
            -1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f,  1.0f,

             1.0f,  1.0f,  1.0f, // Right face
             1.0f,  1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f,  1.0f,

            -1.0f, -1.0f, -1.0f, // Bottom face
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,

            -1.0f,  1.0f, -1.0f, // Top face
             1.0f,  1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,
        ];

        private static readonly int[] _indices =
        [
             0,  1,  2, // Front face
             2,  3,  0,

             4,  5,  6, // Back face
             6,  7,  4,

             8,  9, 10, // Left face
            10, 11,  8,

            12, 13, 14, // Right face
            14, 15, 12,

            16, 17, 18, // Bottom face
            18, 19, 16,

            20, 21, 22, // Top face
            22, 23, 20
        ];

        private static VertexArraySetup VertexArraySetup { get; } = new();

        private static ElementBufferObject ElementBufferObject { get; } = new();

        internal CubemapTexture Texture { get; set; } = new(Path.FromAssets(@"Default\Textures\Cubemap"));

        public override Shader Shader => CubemapShader.Instance;

        public override bool IsHovered => false;

        public override bool IsActive => false;

        static Cubemap()
        {
            VertexArraySetup.SetData(_vertices, sizeof(float));
            ElementBufferObject.SetData(_indices, sizeof(int));
            VertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(CubemapShader.Instance);
        }

        public override void UpdateShader()
        {
            if (Shader is CubemapShader cubemapShader)
            {
                cubemapShader.SetSkybox(Texture.TextureUnit);
            }
        }

        public override void Draw()
        {
            Texture.Use();

            VertexArraySetup.VertexArrayObject.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            ITexture.Unbind(TextureTarget.TextureCubeMap);
        }
    }
#endif
}
