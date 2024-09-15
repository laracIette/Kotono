using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;

namespace Kotono.Utils
{
    internal static class Intersection
    {
        /// <summary>
        /// Get whether the ray formed by rayOrigin and RayDirection intersects the Triangle. 
        /// </summary>
        /// <param name="rayOrigin"> The origin location of the ray. </param>
        /// <param name="rayDirection"> The direction of the ray. </param>
        /// <param name="triangle"> The Triangle to check intersection from. </param>
        /// <param name="intersectionLocation"> The location Vector at which the mouse intersects the mesh. </param>
        /// <param name="intersectionDistance"> The distance of the intersectionLocation from the Camera. </param>
        /// <returns> <see langword="true"/> if the ray interects the Triangle, else returns <see langword="false"/>. </returns>
        internal static bool IntersectRayTriangle(in Vector rayOrigin, in Vector rayDirection, in ModelTriangle triangle, in Transform transform, out Vector intersectionLocation, out float intersectionDistance)
        {
            intersectionLocation = Vector.Zero;
            intersectionDistance = 0.0f;

            var vertex1 = (Vector)Vector3.TransformPosition((Vector3)triangle[0], transform.Model);
            var vertex2 = (Vector)Vector3.TransformPosition((Vector3)triangle[1], transform.Model);
            var vertex3 = (Vector)Vector3.TransformPosition((Vector3)triangle[2], transform.Model);

            var triangleNormal = Vector.Cross(vertex2 - vertex1, vertex3 - vertex1);
            var rayToVertex1 = vertex1 - rayOrigin;

            float denominator = Vector.Dot(triangleNormal, rayDirection);
            if (Math.Abs(denominator) < float.Epsilon)
            {
                // Ray is parallel to the triangle's plane or triangle is degenerate
                return false;
            }

            float t = Vector.Dot(rayToVertex1, triangleNormal) / denominator;
            if (t < 0.0f)
            {
                // Triangle is behind the ray's origin
                return false;
            }

            intersectionLocation = rayOrigin + t * rayDirection;

            var v0 = vertex2 - vertex1;
            var v1 = vertex3 - vertex1;
            var v2 = intersectionLocation - vertex1;

            float dot00 = Vector.Dot(v0, v0);
            float dot01 = Vector.Dot(v0, v1);
            float dot02 = Vector.Dot(v0, v2);
            float dot11 = Vector.Dot(v1, v1);
            float dot12 = Vector.Dot(v1, v2);

            float invDenominator = 1.0f / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenominator;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenominator;

            if (u >= 0.0f && v >= 0.0f && (u + v) <= 1.0f)
            {
                // Ray intersects the triangle
                intersectionDistance = Vector.Distance(rayOrigin, intersectionLocation);
                return true;
            }

            return false;
        }
    }
}
