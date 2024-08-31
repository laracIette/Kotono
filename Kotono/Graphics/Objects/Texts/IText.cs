using Kotono.Utils;

namespace Kotono.Graphics.Objects.Texts
{
    internal interface IText : IObject2D
    {
        /// <summary>
        /// The source of the <see cref="IText"/>'s Value.
        /// </summary>
        public object? Source { get; set; }

        /// <summary>
        /// The value of the <see cref="IText"/>.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// The spacing between the <see cref="IText"/>'s letters.
        /// </summary>
        public float Spacing { get; set; }

        /// <summary>
        /// The anchor of the <see cref="IText"/>.
        /// </summary>
        public Anchor Anchor { get; set; }
    }
}
