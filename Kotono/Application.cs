using Kotono.Audio;
using Kotono.File;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Objects.Settings;
using Kotono.Input;
using Kotono.Utils;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace Kotono
{
    internal class Application : Window
    {
        private readonly TestSound _sound;

        private readonly Animation _animation;

        internal Application(WindowSettings windowSettings)
            : base(windowSettings)
        {
            SoundManager.GeneralVolume = 1.0f;

            _sound = new TestSound();

            new Cursor();

            CreateObjects();

            var settings = Settings.Parse<AnimationSettings>(Path.ASSETS + @"Animations\Counting\Counting.ktf");

            _animation = new Animation(settings);

            new FlatTextureMesh();
        }

        protected override void Update()
        {
            _sound.Update();

            if (Keyboard.IsKeyPressed(Keys.Enter))
            {
                Mouse.CursorState = (CursorState)Math.Loop((int)Mouse.CursorState + 1.0f, 3.0f);
            }

            if (Keyboard.IsKeyPressed(Keys.T))
            {
                if (_animation.IsPlaying)
                {
                    _animation.Pause();
                }
                else
                {
                    _animation.Play();
                }
            }
        }

        private static void CreateObjects()
        {
            new SpotLight();

            new Cube
            {
                IsGravity = true,
                IsFizix = true
            };

            for (int i = 0; i < 10; i++)
            {
                new RainbowPointLight();
            }
        }
    }

}
