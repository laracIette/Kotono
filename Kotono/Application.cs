﻿using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Buttons;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Settings;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Linq;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace Kotono
{
    internal class Application : Window
    {
        private readonly TestSound _sound;

        private readonly Animation _animation;

        private readonly Timer _timer;

        internal Application(WindowSettings windowSettings)
            : base(windowSettings)
        {
            _ = new Cursor();

            _sound = new TestSound();

            _animation = new Animation(JsonParser.Parse<AnimationSettings>(Path.ASSETS + @"Animations\Counting\Counting.json"));

            CreateObjects();

            _ = new TextButtonList(
                new TextButtonListSettings
                {
                    Color = Color.DarkSlateGray,
                    Texts =
                    [
                        "Hey",
                        "Yo",
                        "BOoOOooOo"
                    ],
                    Dest = new Rect(150.0f, 150.0f, 200.0f, 50.0f),
                    CornerSize = 15.0f
                }
            );

            _timer = new Timer();
            _timer.Timeout += OnTimerTimeout;
            _timer.Start(1.0f, true);

            var parent = new Cube()
            {
                WorldLocation = new Vector(-3.0f, 0.0f, -3.0f)
            };
            var child = new Cube()
            {
                RelativeLocation = new Vector(6.0f, 0.0f, 0.0f)
            };
        }

        private void OnTimerTimeout(object? sender, TimeoutEventArgs e)
        {
            Printer.Print((int)e.Time, true);
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

            if (Keyboard.IsKeyDown(Keys.I))
            {
                Printer.Print(Time.Now, true);
            }

            if (Keyboard.IsKeyPressed(Keys.J))
            {
                foreach (var obj in ISelectable.Selected3D.Where(s => s != ISelectable.Active))
                {
                    obj.Parent = ISelectable.Active3D;
                }
            }
            if (Keyboard.IsKeyPressed(Keys.K))
            {
                foreach (var obj in ISelectable.Selected3D)
                {
                    obj.Parent = null;
                }
            }
        }

        private static void CreateObjects()
        {
            _ = new SpotLight();

            _ = new Cube
            {
                IsGravity = true,
                IsFizix = false,
            };

            for (int i = 0; i < 10; i++)
            {
                _ = new RainbowPointLight();
            }

            _ = new FlatTextureMesh()
            {
                RelativeLocation = -Vector.Forward * 5
            };
        }
    }

}
