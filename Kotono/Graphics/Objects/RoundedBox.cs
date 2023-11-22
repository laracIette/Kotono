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

        public virtual Rect Dest 
        {
            get => _dest;
            set
            {
                _dest = value;
                UpdateValues();
            }
        }

        public Point Position
        {
            get => _dest.Position;
            set
            {
                _dest.Position = value;
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

            ObjectManager.Create(this);
        }

        public virtual void Init()
        {

        }

        public virtual void Update()
        {

        }

        protected virtual void UpdateValues()
        {
            /// CornerSize has : 
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the box's Width and Height divided by 2
            _cornerSize = Math.Clamp(CornerSize, 0, Math.Min(Dest.W, Dest.H) / 2);

            /// FallOff has :
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl
            _fallOff = Math.Max(0.000001, FallOff);
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

        public virtual void Show()
        {
            IsDraw = true;
        }

        public virtual void Hide()
        {
            IsDraw = false;
        }

        public void Save()
        {

        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
