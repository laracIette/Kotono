using Kotono.Graphics.Objects;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal sealed class Material : IMaterial
    {
        private MaterialTexture _albedo = new(Path.FromAssets(@"Default\Textures\Material\albedo.jpg")) { TextureUnit = TextureUnit.Texture0 };

        private MaterialTexture _normal = new(Path.FromAssets(@"Default\Textures\Material\normal.jpg")) { TextureUnit = TextureUnit.Texture1 };

        private MaterialTexture _metallic = new(Path.FromAssets(@"Default\Textures\Material\metallic.jpg")) { TextureUnit = TextureUnit.Texture2 };

        private MaterialTexture _roughness = new(Path.FromAssets(@"Default\Textures\Material\roughness.jpg")) { TextureUnit = TextureUnit.Texture3 };

        private MaterialTexture _ambientOcclusion = new(Path.FromAssets(@"Default\Textures\Material\ambientOcclusion.jpg")) { TextureUnit = TextureUnit.Texture4 };

        private MaterialTexture _emissive = new(Path.FromAssets(@"Default\Textures\Material\emissive.jpg")) { TextureUnit = TextureUnit.Texture5 };

        /// <summary>
        /// The base color of the surface.
        /// </summary>
        internal MaterialTexture Albedo
        {
            get => _albedo;
            set
            {
                _albedo = value;
                _albedo.TextureUnit = TextureUnit.Texture0;
            }
        }

        /// <summary>
        /// The way light rays are refracted on the surface to simulate surface details like bumps and grooves.
        /// </summary>
        internal MaterialTexture Normal
        {
            get => _normal;
            set
            {
                _normal = value;
                _normal.TextureUnit = TextureUnit.Texture1;
            }
        }

        /// <summary>
        /// The metallic level of the surface, which determines how metallic the surface appears.
        /// Higher values make the surface look more metallic.
        /// </summary>
        internal MaterialTexture Metallic
        {
            get => _metallic;
            set
            {
                _metallic = value;
                _metallic.TextureUnit = TextureUnit.Texture2;
            }
        }

        /// <summary>
        /// The roughness level of the surface, which affects the blurriness or smoothness of reflections. 
        /// Higher values result in a rougher, blurrier surface.
        /// </summary>
        internal MaterialTexture Roughness
        {
            get => _roughness;
            set
            {
                _roughness = value;
                _roughness.TextureUnit = TextureUnit.Texture3;
            }
        }

        /// <summary>
        /// Ambient occlusion texture, which adds shading to simulate how ambient light is blocked by objects. 
        /// It enhances the depth and detail by darkening crevices and corners.
        /// </summary>
        internal MaterialTexture AmbientOcclusion
        {
            get => _ambientOcclusion;
            set
            {
                _ambientOcclusion = value;
                _ambientOcclusion.TextureUnit = TextureUnit.Texture4;
            }
        }

        /// <summary>
        /// Emissive texture that defines how much light the surface emits. 
        /// This can be used to create glowing effects or simulate light sources on the surface.
        /// </summary>
        internal MaterialTexture Emissive
        {
            get => _emissive;
            set
            {
                _emissive = value;
                _emissive.TextureUnit = TextureUnit.Texture5;
            }
        }


        public void Use()
        {
            Albedo.Use();
            Normal.Use();
            Metallic.Use();
            Roughness.Use();
            AmbientOcclusion.Use();
            Emissive.Use();
        }
    }
}
