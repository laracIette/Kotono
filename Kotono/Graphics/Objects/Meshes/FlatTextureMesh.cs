﻿using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        internal FlatTextureMesh()
            : base(
                JsonParser.Parse<MeshSettings>(Path.ASSETS + @"Meshes\flatTextureMesh.json")
            )
        {
        }

        public override void Draw()
        {
            Material.Use();

            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            Model.Draw();

            Texture.Unbind();
        }
    }
}
