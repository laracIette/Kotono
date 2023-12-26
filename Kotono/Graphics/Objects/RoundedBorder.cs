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
            set
            {
                _thickness = value;
                UpdateValues();
            }
        }

        protected override Matrix4 Model =>
            Matrix4.CreateScale((Dest + new Rect(w: FallOff * 2 + Thickness)).WorldSpace.W, (Dest + new Rect(h: FallOff * 2 + Thickness)).WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public RoundedBorder(Rect dest, Color color, int layer, float fallOff, float cornerSize, float thickness)
            : base(dest, color, layer, fallOff, cornerSize)
        {
            Thickness = thickness;
        }

        protected override void UpdateValues()
        {
            float minSize = Point.Min(Dest.Size);

            /// Thickness has :
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the border's Width and Height
            _thickness = Math.Clamp(Thickness, 0, minSize);

            /// CornerSize has :
            ///     a minimum value of the border's Thickness / 2 + its FallOff,
            ///     a maximum value of the smallest value between the border's Width and Height divided by 2
            _cornerSize = Math.Clamp(CornerSize, Thickness / 2 + FallOff, minSize / 2);

            /// FallOff has : 
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl,
            ///     a maximum value of the border's Width - its Thickness
            _fallOff = Math.Clamp(FallOff, 0.000001, Dest.W - Thickness);
        }

        public override void Draw()
        {
            ShaderManager.RoundedBorder.SetMatrix4("model", Model);
            ShaderManager.RoundedBorder.SetPoint("windowSize", KT.Size);
            ShaderManager.RoundedBorder.SetColor("color", Color);
            ShaderManager.RoundedBorder.SetRect("dest", Dest.WorldSpace);
            ShaderManager.RoundedBorder.SetFloat("fallOff", FallOff);
            ShaderManager.RoundedBorder.SetFloat("cornerSize", CornerSize);
            ShaderManager.RoundedBorder.SetFloat("thickness", Thickness);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
