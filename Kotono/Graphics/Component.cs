using Kotono.Graphics.Objects;

namespace Kotono.Graphics
{
    internal class Component
    {
        private readonly Viewport _viewport;
        
        private readonly ImageManager _imageManager = new();

        private readonly int _background;
        
        internal Component(Rect dest) 
        { 
            _viewport = new Viewport(dest);

            _background = _imageManager.Create(new Image(KT.KotonoPath + "Assets/PerformanceWindow/background.png", dest));
        }

        internal void Update()
        {
            _imageManager.Update();
        }

        internal void UpdateShaders()
        {
            _imageManager.UpdateShaders();
        }

        internal void Draw()
        {
            _viewport.Use();

            _imageManager.Draw();
        }
    }
}
