using System;

namespace Kotono.Graphics.Objects
{
    public interface IDrawable : IDisposable
    {
        public bool IsDraw { get; }

        public void Init();

        public void Update();

        public void UpdateShaders();

        public void Draw();

        public void Save();

        public void Show();

        public void Hide();
    }
}
