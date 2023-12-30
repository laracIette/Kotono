using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Mathematics;
using System.Linq;
using Math = Kotono.Utils.Math;

namespace Kotono.Physics
{
    public sealed class Fizix
    {
        private Fizix() { }

        public static Vector Gravity { get; set; }

        public static void Init()
        {
            Gravity = new Vector(0.0f, -0.1f, 0.0f);
        }

        public static void Update(Mesh mesh)
        {
            var collisionCenter = Vector.Zero;
            int n = 0;

            foreach (var vertex in mesh.Vertices)
            {
                if ((Vector.RotateAroundPoint(vertex, mesh.Center, mesh.Rotation).Y + mesh.Location.Y) <= 0.0f)
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

        public static void Update(Sphere sphere)
        {
            var collisionCenter = Vector.Zero;
            int n = 0;

            foreach (var collider in sphere.Collisions.Where(sphere.CollidesWith))
            {
                collisionCenter += collider.Location;
                n++;
            }

            if (n > 0)
            {
                collisionCenter /= n;

                //Vector direction = -Vector.Abs((collisionCenter - sphere.Location).Normalized);

                //sphere.Velocity *= direction;

                Vector delta = sphere.Location - collisionCenter;
                float distance = delta.Length;

                float sphere1Radius = 1.0f;
                float sphere2Radius = 10.0f;

                // Calculate the collision response
                Vector normal = delta.Normalized;
                Vector relativeVelocity = sphere.Velocity - 0.0f;
                float velocityAlongNormal = Vector.Dot(relativeVelocity, normal);

                // If spheres are moving towards each other, perform the bounce
                if (velocityAlongNormal < 0.0f)
                {
                    float impulse = -2.0f * velocityAlongNormal / (1.0f / sphere1Radius + 1.0f / sphere2Radius);
                    sphere.Velocity -= normal * impulse / sphere1Radius;
                    //sphere2.Velocity += normal * impulse / sphere2Radius;
                }

            }
        }

        public static void Update(Sphere left, Sphere right)
        {
            Vector delta = left.Location - right.Location;
            float distance = delta.Length;

            if (distance < left.Radius + right.Radius)
            {
                // Calculate the collision response
                Vector normal = delta.Normalized;
                Vector relativeVelocity = left.Velocity - right.Velocity;
                float velocityAlongNormal = Vector.Dot(relativeVelocity, normal);

                // If spheres are moving towards each other, perform the bounce
                if (velocityAlongNormal < 0.0f)
                {
                    float impulse = -2.0f * velocityAlongNormal / (1.0f / left.Radius + 1.0f / right.Radius);
                    left.Velocity -= normal * impulse / left.Radius;
                    right.Velocity += normal * impulse / right.Radius;
                }
            }
        }
    }
}
