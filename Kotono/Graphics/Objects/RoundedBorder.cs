using Kotono.Utils;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class RoundedBorder : RoundedBox
    {
        private float _thickness;

        public float Thickness
        {
            get => _thickness;
            set => _thickness = Math.Clamp(value, 0, float.PositiveInfinity);
        }



        protected override Matrix4 Model =>
            Matrix4.CreateScale((Dest + new Rect(w: FallOff * 2 + Thickness)).WorldSpace.W, (Dest + new Rect(h: FallOff * 2 + Thickness)).WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public RoundedBorder(Rect dest, Color color, float fallOff, float cornerSize, float thickness)
            : base(dest, color, fallOff, cornerSize)
        {
            _thickness = thickness;
        }

        public override void Draw()
        {
            ShaderManager.SetMatrix4(ShaderType.RoundedBorder, "model", Model);
            ShaderManager.SetColor(ShaderType.RoundedBorder, "color", Color);
            ShaderManager.SetRect(ShaderType.RoundedBorder, "dest", new Rect(Dest.X, KT.Size.Y - Dest.Y, Dest.W, Dest.H));
            ShaderManager.SetFloat(ShaderType.RoundedBorder, "fallOff", FallOff);
            ShaderManager.SetFloat(ShaderType.RoundedBorder, "cornerSize", CornerSize);
            ShaderManager.SetFloat(ShaderType.RoundedBorder, "thickness", Thickness);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
