using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System;

namespace Kotono.Engine.UserInterface.Components
{
    /// <summary>
    /// Represents the <see cref="WindowComponent"/> that contains a <see cref="Scene"/>.
    /// </summary>
    internal sealed class SceneComponent : WindowComponent
    {
        private static readonly Lazy<SceneComponent> _instance = new(static () => new()
        {
            RelativeSize = new Point(1280.0f, 720.0f),
            RelativePosition = Point.Zero,
            Anchor = Anchor.TopLeft,
            BackgroundColor = Color.Transparent,
        });

        internal static SceneComponent Instance => _instance.Value;
    }
}
