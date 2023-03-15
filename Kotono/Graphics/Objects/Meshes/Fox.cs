using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Meshes
{
    public class Fox : MeshOBJ
    {
        public Fox(Vector3 position, Vector3 angle, Vector3 scale, string diffusePath, string specularPath, Vector3 color)
            : base(
                  "fox.obj",
                  position,
                  angle,
                  scale,
                  diffusePath,
                  specularPath,
                  ShaderManager.Lighting,
                  color
              )
        {
        }
    }
}
