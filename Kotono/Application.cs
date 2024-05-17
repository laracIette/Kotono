using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Buttons;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Input;
using Kotono.Settings;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Timing;
using System.Linq;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace Kotono
{
    internal class Application : Window
    {
        private readonly Animation _animation;

        private readonly Timer _timer;

        internal Application(WindowSettings windowSettings)
            : base(windowSettings)
        {
            _ = new Cursor();

            _ = new TestSound();

            _ = new TestImage();

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
                    Rect = new Rect(150.0f, 150.0f, 200.0f, 50.0f),
                    CornerSize = 15.0f
                }
            );

            _animation = new Animation(JsonParser.Parse<AnimationSettings>(Path.ASSETS + @"Animations\Counting\Counting.json"));

            _timer = new Timer();
            _timer.Timeout += OnTimerTimeout;

            CreateObjects();
        }

        private void OnTimerTimeout(object? sender, TimeoutEventArgs e)
        {
            Printer.Print((int)e.Time, true);
        }

        private static void OnEnterKeyPressed()
        {
            Mouse.CursorState = (CursorState)Math.Loop((float)Mouse.CursorState + 1.0f, 3.0f);
        }

        private void OnTKeyPressed()
        {
            _animation.Switch();
        }

        private static void OnIKeyPressed()
        {
            Printer.Print(Time.Now, true);
        }

        private static void OnJKeyPressed()
        {
            foreach (var obj in ISelectable.Selected3D.Where(s => s != ISelectable.Active))
            {
                obj.Parent = ISelectable.Active3D;
            }
        }

        private static void OnKKeyPressed()
        {
            if (Keyboard.IsKeyPressed(Keys.K))
            {
                foreach (var obj in ISelectable.Selected3D)
                {
                    obj.Parent = null;
                }
            }
        }

        protected override void Start()
        {
            _timer.Start(1.0f, true);
        }

        protected override void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.Enter)) OnEnterKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.T)) OnTKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.I)) OnIKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.J)) OnJKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.K)) OnKKeyPressed();
        }

        private static void CreateObjects()
        {
            _ = new SpotLight();

            _ = new Cube
            {
                IsGravity = true,
                IsFizix = false,
            };

            _ = new Cube()
            {
                RelativeLocation = new Vector(0.0f, 0.0f, -5.0f)
            };

            _ = new Cube()
            {
                RelativeLocation = new Vector(6.0f, 0.0f, 0.0f)
            };

            for (int i = 0; i < 10; i++)
            {
                _ = new PointLightMesh
                {
                    Parent = new RainbowPointLight(),
                    RelativeLocation = Vector.Zero
                };
            }
        }
    }

}
