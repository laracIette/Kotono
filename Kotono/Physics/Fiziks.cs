using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Mathematics;
using Math = Kotono.Utils.Math;

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
            Gravity = new Vector(0, -.1, 0);
        }

        public static void Update(Mesh mesh) 
        {
            var collisionCenter = Vector.Zero;
            int n = 0;

            foreach (var vertex in mesh.Vertices)
            {
                if ((Vector.RotateAroundPoint(vertex, mesh.Center, mesh.Rotation).Y + mesh.Location.Y) <= 0)
                {
                    collisionCenter += vertex;
                    n++;
                }
            }

            if ((n > 0) && (n < 3))
            {
                collisionCenter /= n;

                float roll = Math.Rad(mesh.Rotation.X);
                float pitch = Math.Rad(mesh.Rotation.Y);
                float yaw = Math.Rad(mesh.Rotation.Z);
                MathHelper.RadiansToDegrees(roll);
                var up = new Vector
                {
                    X = Math.Cos(roll) * Math.Sin(pitch),
                    Y = Math.Sin(roll) * Math.Sin(pitch),
                    Z = Math.Cos(pitch)
                };

                var lookAtMatrix = Matrix4.LookAt((Vector3)mesh.Location, (Vector3)collisionCenter, (Vector3)up);
                mesh.Rotation = (Vector)Vector3.TransformPosition((Vector3)mesh.Rotation, lookAtMatrix);
            }
        }

    }
}
