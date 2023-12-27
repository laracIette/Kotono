using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class Viewport(Rect dest)
    {
        private readonly List<Viewport> _connections = [];

        public Rect Dest = dest;

        public Point Position
        {
            get => Dest.Position; 
            set => Dest.Position = value;
        }
        
        public Point Size
        {
            get => Dest.Size; 
            set => Dest.Size = value;
        }

        public float X
        {
            get => Dest.X;
            set => Dest.X = value;
        }

        public float Y
        {
            get => Dest.Y;
            set => Dest.Y = value;
        }

        public float W
        {
            get => Dest.W;
            set => Dest.W = value;
        }

        public float H
        {
            get => Dest.H;
            set => Dest.H = value;
        }

        public void Init()
        {

        }

        public void Use()
        {
            GL.Viewport((int)X, (int)(KT.Dest.H - Y - H), (int)W, (int)H);
        }
    }
}
