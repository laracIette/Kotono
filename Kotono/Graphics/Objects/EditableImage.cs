using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public class EditableImage : Image
    {
        public EditableImage(string path, Rect dest, Color color, int layer)
            : base(path, dest, color, layer)
        {

        }

        public override void Update()
        {
            base.Update();

            //KT.Print(IsMouseOn());
        }
    }
}
