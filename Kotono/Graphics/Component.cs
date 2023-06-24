using Kotono.Graphics.Objects;

namespace Kotono.Graphics
{
    internal class Component
    {
        private readonly Viewport _viewport;
        
        private readonly ImageManager _imageManager = new();

        private readonly Image _background;
        
        internal Component(Rect dest) 
        { 
            _viewport = new Viewport(dest);

            //_background = _imageManager.Create(new Image(KT.KotonoPath + "Assets/PerformanceWindow/background.png", dest * new Rect(0, 0, 1, 1)));
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
