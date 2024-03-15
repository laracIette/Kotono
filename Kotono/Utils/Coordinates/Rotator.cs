namespace Kotono.Utils.Coordinates
{
    public struct Rotator
    {
        /// <summary>
        /// The roll angle of the Rotator in degrees.
        /// </summary>
        public float Roll { get; set; }

        /// <summary>
        /// The pitch angle of the Rotator in degrees.
        /// </summary>
        public float Pitch { get; set; }

        /// <summary>
        /// The yaw angle of the Rotator in degrees.
        /// </summary>
        public float Yaw { get; set; }

        public readonly Rotator Radians => new Rotator(Math.Rad(Roll), Math.Rad(Pitch), Math.Rad(Yaw));

        public static Rotator Zero => new Rotator(0.0f, 0.0f, 0.0f);

        public Rotator(float roll = 0.0f, float pitch = 0.0f, float yaw = 0.0f)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        public Rotator() : this(0.0f, 0.0f, 0.0f) { }

        public Rotator(Rotator r) : this(r.Roll, r.Pitch, r.Yaw) { }

        public Rotator(float f) : this(f, f, f) { }

        public static Rotator FromRadians(float roll = 0.0f, float pitch = 0.0f, float yaw = 0.0f)
        {
            return new Rotator(Math.Deg(roll), Math.Deg(pitch), Math.Deg(yaw));
        }
    }
}
