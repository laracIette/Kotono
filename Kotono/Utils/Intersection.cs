using Kotono.Graphics.Objects.Shapes;
using OpenTK.Mathematics;

namespace Kotono.Utils
{
    public static class Intersection
    {
        public static bool IntersectRayTriangle(Vector rayOrigin, Vector rayDirection, Triangle triangle, out Vector intersectionPoint, out float distance)
        {
            intersectionPoint = Vector.Zero;
            distance = 0;

            var vertex1 = (Vector)Vector3.TransformPosition((Vector3)triangle.Vertices[0], triangle.Model);
            var vertex2 = (Vector)Vector3.TransformPosition((Vector3)triangle.Vertices[1], triangle.Model);
            var vertex3 = (Vector)Vector3.TransformPosition((Vector3)triangle.Vertices[2], triangle.Model);

            var triangleNormal = Vector.Cross(vertex2 - vertex1, vertex3 - vertex1);
            var rayToVertex1 = vertex1 - rayOrigin;

            float denominator = Vector.Dot(triangleNormal, rayDirection);
            if (Math.Abs(denominator) < float.Epsilon)
            {
                // Ray is parallel to the triangle's plane or triangle is degenerate
                return false;
            }

            float t = Vector.Dot(rayToVertex1, triangleNormal) / denominator;
            if (t < 0)
            {
                // Triangle is behind the ray's origin
                return false;
            }

            intersectionPoint = rayOrigin + rayDirection * t;

            var v0 = vertex2 - vertex1;
            var v1 = vertex3 - vertex1;
            var v2 = intersectionPoint - vertex1;

            float dot00 = Vector.Dot(v0, v0);
            float dot01 = Vector.Dot(v0, v1);
            float dot02 = Vector.Dot(v0, v2);
            float dot11 = Vector.Dot(v1, v1);
            float dot12 = Vector.Dot(v1, v2);

            float invDenominator = 1.0f / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenominator;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenominator;

            if (u >= 0 && v >= 0 && (u + v) <= 1)
            {
                // Ray intersects the triangle
                distance = Vector.Distance(rayOrigin, intersectionPoint);
                return true;
            }

            return false;
        }
    }
}
