using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    public class RoundedBox : IObject2D
    {
        private Rect _dest;

        public Rect Dest 
        {
            get => _dest;
            set
            {
                _dest = value;
                // check if corner should be resized
                CornerSize = _cornerSize;
            }
        }

        public Color Color { get; set; }

        private float _fallOff;

        public float FallOff 
        {
            get => _fallOff;
            // _fallOff has a minimum value of 0.000001 so that there is no division by 0 in glsl
            set => _fallOff = Math.Clamp(value, 0.000001, float.PositiveInfinity);
        }

        private float _cornerSize;

        public float CornerSize 
        {
            get => _cornerSize;
            // _cornerSize has a maximum value of the smallest value between the box's width and height divided by 2
            set => _cornerSize = Math.Clamp(value, 0, Math.Min(Dest.W, Dest.H) / 2);
        } 

        public bool IsDraw { get; private set; } = true;

        public int Layer { get; set; } = 0;
        
        protected virtual Matrix4 Model =>
            Matrix4.CreateScale((Dest + new Rect(w: FallOff * 2)).WorldSpace.W, (Dest + new Rect(h: FallOff * 2)).WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public RoundedBox(Rect dest, Color color, int layer, float fallOff, float cornerSize) 
        {
            Dest = dest;
            Color = color;
            Layer = layer;
            FallOff = fallOff;
            CornerSize = cornerSize;

            ObjectManager.CreateRoundedBox(this);
        }

        public void Init()
        {

        }

        public void Update()
        {

        }

        public void UpdateShaders()
        {

        }

        public virtual void Draw()
        {
            ShaderManager.RoundedBox.SetMatrix4("model", Model);
            ShaderManager.RoundedBox.SetColor("color", Color);
            ShaderManager.RoundedBox.SetRect("dest", new Rect(Dest.X, KT.Size.Y - Dest.Y, Dest.W, Dest.H));
            ShaderManager.RoundedBox.SetFloat("fallOff", FallOff);
            ShaderManager.RoundedBox.SetFloat("cornerSize", CornerSize);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public void Save()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
