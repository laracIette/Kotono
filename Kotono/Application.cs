using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Objects.Texts;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Timing;
using System.Linq;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace Kotono
{
    internal sealed class Application : Window
    {
        private readonly Animation _animation;

        private readonly Timer _timer;

        internal Application(WindowSettings windowSettings)
            : base(windowSettings)
        {
            _ = new Cursor();

            _ = new Cubemap();

            _animation = new Animation(Path.FromAssets(@"Animations\Counting"))
            {
                RelativePosition = new Point(100.0f, 200.0f),
                RelativeSize = new Point(50.0f, 60.0f),
                Color = Color.Yellow,
                FrameRate = 10.0f,
                StartTime = 3.0f,
                Duration = 5.0f
            };

            _timer = new Timer
            {
                Timeout = (s, e) => Printer.PrintRainbow((int)e.Time, 0.01f),
            };

            CreateObjects();
        }

        private static void OnEnterKeyPressed()
            => Mouse.CursorState = (CursorState)Math.Loop((int)Mouse.CursorState + 1, 3);

        private void OnTKeyPressed()
            => _animation.Switch();

        private static void OnIKeyPressed()
            => Printer.PrintRainbow(Time.Now, 0.01f);

        private static void OnJKeyPressed()
        {
            foreach (var obj in ISelectable3D.Selected.Where(s => s != ISelectable3D.Active))
            {
                obj.Parent = ISelectable3D.Active;
            }
        }

        private static void OnKKeyPressed()
        {
            if (Keyboard.IsKeyPressed(Keys.K))
            {
                ISelectable3D.Selected.ForEach(s => s.Parent = null);
            }
        }

        protected override void Start()
        {
            _timer.Start(1.0f, true);
            _animation.Play();
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
            //var text = new TTFText
            //{
            //    FontSize = 15.0f,
            //    Size = new Point(200.0f, 150.0f)
            //};
            //text.AddText("Test", Point.Zero);

            _ = new TestSound();

            _ = new TestImage();

            _ = new Text
            {
                Value = "dhgudd",
                RelativePosition = new Point(150.0f, 200.0f),
                LettersSize = new Point(50.0f, 60.0f),
            };

            _ = new SpotLight();

            _ = new Cube
            {
                RelativeLocation = new Vector(0.0f, 0.0f, -5.0f),
                IsGravity = true,
            };

            _ = new Cube
            {
                RelativeLocation = new Vector(6.0f, 0.0f, 0.0f),
            };

            for (int i = 0; i < 10; i++)
            {
                _ = new PointLightMesh
                {
                    Parent = new RainbowPointLight(),
                };
            }
        }
    }
}
