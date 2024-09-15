namespace Kotono.Utils.Coordinates
{
    internal interface ITransform
    {
        /// <summary> 
        /// The location of the <see cref="ITransform"/> relative to its parent. 
        /// </summary>
        public Vector RelativeLocation { get; set; }

        /// <summary> 
        /// The rotation of the <see cref="ITransform"/> relative to its parent. 
        /// </summary>
        public Rotator RelativeRotation { get; set; }

        /// <summary> 
        /// The scale of the <see cref="ITransform"/> relative to its parent. 
        /// </summary>
        public Vector RelativeScale { get; set; }

        /// <summary> 
        /// The location of the <see cref="ITransform"/>. 
        /// </summary>
        public Vector WorldLocation { get; set; }

        /// <summary> 
        /// The rotation of the <see cref="ITransform"/>. 
        /// </summary>
        public Rotator WorldRotation { get; set; }

        /// <summary> 
        /// The scale of the <see cref="ITransform"/>. 
        /// </summary>
        public Vector WorldScale { get; set; }

        /// <summary>
        /// The velocity of the <see cref="ITransform"/>'s relative location.
        /// </summary>
        public Vector RelativeLocationVelocity { get; set; }

        /// <summary>
        /// The velocity of the <see cref="ITransform"/>'s relative rotation.
        /// </summary>
        public Rotator RelativeRotationVelocity { get; set; }

        /// <summary>
        /// The velocity of the <see cref="ITransform"/>'s relative scale.
        /// </summary>
        public Vector RelativeScaleVelocity { get; set; }

        /// <summary>
        /// The velocity of the <see cref="ITransform"/>'s location.
        /// </summary>
        public Vector WorldLocationVelocity { get; set; }

        /// <summary>
        /// The velocity of the <see cref="ITransform"/>'s rotation.
        /// </summary>
        public Rotator WorldRotationVelocity { get; set; }

        /// <summary>
        /// The velocity of the <see cref="ITransform"/>'s scale.
        /// </summary>
        public Vector WorldScaleVelocity { get; set; }
    }
}
