﻿using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;
using Math = Kotono.Utils.Math;

namespace Kotono.Physics
{
    internal static class Fizix
    {
        internal static Vector Gravity { get; set; } = new Vector(0.0f, -0.1f, 0.0f);

        internal static void Update(Mesh mesh)
        {
            var collisionCenter = Vector.Zero;
            int n = 0;

            foreach (var vertex in mesh.Model.Vertices)
            {
                if ((Vector.RotateAroundPoint(vertex, mesh.Model.Center, mesh.Rotation).Y + mesh.Location.Y) <= 0.0f)
                {
                    collisionCenter += vertex;
                    n++;
                }
            }

            if ((n > 0) && (n < 3))
            {
                collisionCenter /= n;
                
                var rot = Vector.Rad(mesh.Rotation);

                var up = new Vector
                {
                    X = Math.Cos(rot.X) * Math.Sin(rot.Y),
                    Y = Math.Sin(rot.X) * Math.Sin(rot.Y),
                    Z = Math.Cos(rot.Y)
                };

                var lookAtMatrix = Matrix4.LookAt((Vector3)mesh.Location, (Vector3)collisionCenter, (Vector3)up);
                mesh.Rotation = (Vector)Vector3.TransformPosition((Vector3)mesh.Rotation, lookAtMatrix);
            }
        }

        internal static void Update(Sphere sphere)
        {
            var collisionCenter = Vector.Zero;
            int n = 0;

            foreach (var collider in sphere.Collisions.FindAll(sphere.CollidesWith))
            {
                //collisionCenter += collider.Location;
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
                Vector relativeVelocity = sphere.LocationVelocity - 0.0f;
                float velocityAlongNormal = Vector.Dot(relativeVelocity, normal);

                // If spheres are moving towards each other, perform the bounce
                if (velocityAlongNormal < 0.0f)
                {
                    float impulse = -2.0f * velocityAlongNormal / (1.0f / sphere1Radius + 1.0f / sphere2Radius);
                    sphere.LocationVelocity -= normal * impulse / sphere1Radius;
                    //sphere2.Velocity += normal * impulse / sphere2Radius;
                }

            }
        }

        internal static void Update(Sphere left, Sphere right)
        {
            Vector delta = left.Location - right.Location;
            float distance = delta.Length;

            if (distance < left.Radius + right.Radius)
            {
                // Calculate the collision response
                Vector normal = delta.Normalized;
                Vector relativeVelocity = left.LocationVelocity - right.LocationVelocity;
                float velocityAlongNormal = Vector.Dot(relativeVelocity, normal);

                // If spheres are moving towards each other, perform the bounce
                if (velocityAlongNormal < 0.0f)
                {
                    float impulse = -2.0f * velocityAlongNormal / (1.0f / left.Radius + 1.0f / right.Radius);
                    left.LocationVelocity -= normal * impulse / left.Radius;
                    right.LocationVelocity += normal * impulse / right.Radius;
                }
            }
        }
    }
}
