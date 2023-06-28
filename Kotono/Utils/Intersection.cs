using Kotono.Graphics.Objects;

namespace Kotono.Utils
{
    public static class Intersection
    {
        public static bool IntersectRayTriangle(Vector rayOrigin, Vector rayDirection, Triangle triangle, out Vector intersectionPoint)
        {
            intersectionPoint = Vector.Zero;

            const float epsilon = 0.000001f;

            Vector triangleNormal = Vector.Cross(triangle.Vertex2 - triangle.Vertex1, triangle.Vertex3 - triangle.Vertex1);
            Vector rayToVertex1 = triangle.Vertex1 - rayOrigin;
            
            float denominator = Vector.Dot(triangleNormal, rayDirection);
            if (Math.Abs(denominator) < epsilon)
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

            Vector v0 = triangle.Vertex2 - triangle.Vertex1;
            Vector v1 = triangle.Vertex3 - triangle.Vertex1;
            Vector v2 = intersectionPoint - triangle.Vertex1;

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
                return true;
            }

            return false;
        }
    }
}
