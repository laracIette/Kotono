using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Component
    {
        private readonly Viewport _viewport;
        
        private readonly ImageManager _imageManager = new();

        private readonly Image _background;
        
        public Component(Rect dest) 
        { 
            _viewport = new Viewport(dest);

            //_background = _imageManager.Create(new Image(KT.KotonoPath + "Assets/PerformanceWindow/background.png", dest * new Rect(0, 0, 1, 1)));
        }

        public void Update()
        {
            _imageManager.Update();
        }

        public void UpdateShaders()
        {
            _imageManager.UpdateShaders();
        }

        public void Draw()
        {
            _viewport.Use(); 

            _imageManager.Draw();
        }
    }
}
