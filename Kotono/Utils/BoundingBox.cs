namespace Kotono.Utils
{
    public struct BoundingBox
    {
        public Vector Min;

        public Vector Max;

        public BoundingBox()
        {
            Min = Vector.Zero;
            Max = Vector.Zero;
        }

        public BoundingBox(Vector min, Vector max) 
        { 
            Min = min;
            Max = max;
        }
    }
}
