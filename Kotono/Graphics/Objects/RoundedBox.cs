using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    public class RoundedBox : Object2D
    {
        public override Rect Dest
        {
            get => base.Dest;
            set
            {
                base.Dest = value;
                UpdateValues();
            }
        }

        public override Point Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                UpdateValues();
            }
        }

        protected float _fallOff;

        public float FallOff
        {
            get => _fallOff;
            set
            {
                _fallOff = value;
                UpdateValues();
            }
        }

        protected float _cornerSize;

        public float CornerSize
        {
            get => _cornerSize;
            set
            {
                _cornerSize = value;
                UpdateValues();
            }
        }

        public Color Color { get; set; }

        protected virtual Matrix4 Model =>
            Matrix4.CreateScale(
                (Dest + new Rect(w: FallOff * 2.0f)).NDC.W,
                (Dest + new Rect(h: FallOff * 2.0f)).NDC.H,
                1.0f
            )
            * Matrix4.CreateTranslation(Dest.NDC.X, Dest.NDC.Y, 0.0f);

        public RoundedBox(Rect dest, Color color, int layer, float fallOff, float cornerSize)
            : base()
        {
            Dest = dest;
            Color = color;
            Layer = layer;
            FallOff = fallOff;
            CornerSize = cornerSize;
        }

        protected virtual void UpdateValues()
        {
            /// CornerSize has : 
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the box's Width and Height divided by 2
            _cornerSize = Math.Clamp(CornerSize, 0.0f, Math.Min(Dest.W, Dest.H) / 2.0f);

            /// FallOff has :
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl
            _fallOff = Math.Max(0.000001f, FallOff);
        }

        public override void Draw()
        {
            ShaderManager.RoundedBox.SetMatrix4("model", Model);
            ShaderManager.RoundedBox.SetRect("viewportDest", ComponentManager.ActiveViewport.Dest);
            ShaderManager.RoundedBox.SetColor("color", Color);
            ShaderManager.RoundedBox.SetRect("dest", Dest.NDC);
            ShaderManager.RoundedBox.SetFloat("fallOff", FallOff);
            ShaderManager.RoundedBox.SetFloat("cornerSize", CornerSize);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
