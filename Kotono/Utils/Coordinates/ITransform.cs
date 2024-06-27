namespace Kotono.Utils.Coordinates
{
    internal interface ITransform
    {
        /// <summary> 
        /// The location of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeLocation { get; set; }

        /// <summary> 
        /// The rotation of the Transform relative to its parent. 
        /// </summary>
        public Rotator RelativeRotation { get; set; }

        /// <summary> 
        /// The scale of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeScale { get; set; }

        /// <summary> 
        /// The location of the Transform. 
        /// </summary>
        public Vector WorldLocation { get; set; }

        /// <summary> 
        /// The rotation of the Transform. 
        /// </summary>
        public Rotator WorldRotation { get; set; }

        /// <summary> 
        /// The scale of the Transform. 
        /// </summary>
        public Vector WorldScale { get; set; }

        /// <summary>
        /// The velocity of the Transform's relative location.
        /// </summary>
        public Vector RelativeLocationVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's relative rotation.
        /// </summary>
        public Rotator RelativeRotationVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's relative scale.
        /// </summary>
        public Vector RelativeScaleVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's location.
        /// </summary>
        public Vector WorldLocationVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's rotation.
        /// </summary>
        public Rotator WorldRotationVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's scale.
        /// </summary>
        public Vector WorldScaleVelocity { get; set; }
    }
}
