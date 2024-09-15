using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using System.Linq;

namespace Kotono.Engine.UserInterface.AddMenu
{
    internal sealed class SubMenu : Object2D
    {
        private readonly Text[] _options;

        private readonly Point _letterSize = new(20.0f, 24.0f);

        public override Anchor Anchor
        {
            get => base.Anchor;
            set
            {
                base.Anchor = value;
                for (int i = 0; i < _options.Length; i++)
                {
                    _options[i].RelativePosition = GetTextPosition(i, _letterSize.Y);   
                    _options[i].Anchor = value;
                }
            }
        }

        internal SubMenu(string[] options)
        {
            _options = [.. options.Select((o, i) => new Text
            {
                RelativeSize = _letterSize,
                Layer = 3,
                Source = o,
                Spacing = 0.6f,
                Parent = this
            })];
        }

        private Point GetTextPosition(int index, float spacing)
        {
            return Anchor switch
            {
                Anchor.TopLeft => new Point(0.0f, index * spacing),
                Anchor.TopRight => new Point(0.0f, index * spacing),
                Anchor.BottomLeft => new Point(0.0f, -index * spacing),
                Anchor.BottomRight => new Point(0.0f, -index * spacing),
                _ => throw new SwitchException(typeof(Anchor), Anchor)
            };
        }
    }
}
