using Kotono.Audio;
using Kotono.Settings;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Utils;
using System;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using Math = Kotono.Utils.Math;

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
            
            new Cursor();

            _sound = new TestSound();

            _animation = new Animation(JsonParser.Parse<AnimationSettings>(Path.ASSETS + @"Animations\Counting\Counting.json"));
            
            CreateObjects();
        }

        protected override void Update()
        {
            _sound.Update();

            if (Keyboard.IsKeyPressed(Keys.Enter))
            {
                Mouse.CursorState = (CursorState)Math.Loop((float)Mouse.CursorState + 1.0f, 3.0f);
            }

            if (Keyboard.IsKeyPressed(Keys.T))
            {
                _animation.Switch();
            }
        }

        private static void CreateObjects()
        {
            new SpotLight();

            new Cube
            {
                IsGravity = true,
                IsFizix = false
            };

            for (int i = 0; i < 10; i++)
            {
                new RainbowPointLight();
            }

            new FlatTextureMesh()
            {
                Location = -Vector.Forward * 5
            };
        }
    }

}
