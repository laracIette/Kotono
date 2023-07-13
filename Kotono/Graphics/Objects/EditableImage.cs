using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public class EditableImage : Image
    {
        public EditableImage(string path, Rect dest, Color color)
            : base(path, dest, color)
        {

        }

        public override void Update()
        {
            base.Update();

            //KT.Print(IsMouseOn());
        }
    }
}
