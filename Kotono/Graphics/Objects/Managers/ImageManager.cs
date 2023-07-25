namespace Kotono.Graphics.Objects.Managers
{
    public class ImageManager : DrawableManager<Image>
    {
        public ImageManager()
            : base() { }

        public override void Create(Image image)
        {
            // index of the first image of a superior layer
            int index = _drawables.FindIndex(i => i.Layer > image.Layer);
            
            // if every image has an inferior Layer
            if (index == -1) 
            {
                // add image at the end
                _drawables.Add(image);
            }
            else 
            {
                // insert before the first image of a superior layer 
                _drawables.Insert(index, image);
            }
        }
    }
}
