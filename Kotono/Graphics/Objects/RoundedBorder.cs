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
            set => _thickness = Math.Clamp(value, 0, Math.Min(Dest.W, Dest.H) - FallOff * 2);
        }

        public override float CornerSize
        {
            get => _cornerSize;
            // _cornerSize has a minimum value of Thickness / 2 + FallOff and a maximum value of the smallest value between the box's width and height divided by 2
            set => _cornerSize = Math.Clamp(value, Thickness / 2 + FallOff, Math.Min(Dest.W, Dest.H) / 2);
        }

        protected override Matrix4 Model =>
            Matrix4.CreateScale((Dest + new Rect(w: FallOff * 2 + Thickness)).WorldSpace.W, (Dest + new Rect(h: FallOff * 2 + Thickness)).WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public RoundedBorder(Rect dest, Color color, int layer, float fallOff, float cornerSize, float thickness)
            : base(dest, color, layer, fallOff, cornerSize)
        {
            _thickness = thickness;
        }

        public override void Draw()
        {
            ShaderManager.RoundedBorder.SetMatrix4("model", Model);
            ShaderManager.RoundedBorder.SetColor("color", Color);
            ShaderManager.RoundedBorder.SetRect("dest", new Rect(Dest.X, KT.Size.Y - Dest.Y, Dest.W, Dest.H));
            ShaderManager.RoundedBorder.SetFloat("fallOff", FallOff);
            ShaderManager.RoundedBorder.SetFloat("cornerSize", CornerSize);
            ShaderManager.RoundedBorder.SetFloat("thickness", Thickness);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
