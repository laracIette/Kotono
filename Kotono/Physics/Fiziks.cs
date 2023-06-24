using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Mathematics;

namespace Kotono.Physics
{
    public enum CollisionState
    {
        None,
        BlockAll,
        BlockSelection
    }

    public sealed class Fiziks
    {
        private Fiziks() { }

        public static Vector Gravity { get; set; }

        public static void Init()
        {
            Gravity = new Vector(0f, -1f, 0f);
        }

        public static void Update(IMesh mesh) 
        {
            var collisionCenter = Vector.Zero;
            int n = 0;
            
            foreach (var vertex in mesh.Vertices)
            {
                if ((vertex.Y + mesh.Position.Y) <= 0)
                {
                    collisionCenter += vertex;
                    n++;
                }
            }

            if ((n > 0) && (n < 3))
            {
                collisionCenter /= n;
            }
        }

    }
}
