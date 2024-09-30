using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
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
                IsLoop = true,
            };

            _timer = new Timer
            {
                Timeout = (s, e) => Printer.PrintRainbow((int)e.Time, 0.01f),
                TargetDuration = 1.0f,  
                IsLoop = true,
            };

            CreateObjects();
        }

        private static void OnEnterKeyPressed()
            => Mouse.CursorState = (CursorState)Math.Loop((int)Mouse.CursorState + 1, 3);

        private void OnTKeyPressed()
            => _animation.Switch();

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
            _timer.Start();
            ExecuteAction.Delay(_animation.Play, 3.0f);
        }

        protected override void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.Enter)) OnEnterKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.T)) OnTKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.J)) OnJKeyPressed();
            if (Keyboard.IsKeyPressed(Keys.K)) OnKKeyPressed();
        }

        private static void CreateObjects()
        {
            _ = new TestSound();

            _ = new TestImage();

            _ = new SpotLight
            {
                Parent = Camera.Active,
                Diffuse = Color.Red,
                Intensity = 1.0f,
                IsOn = false,
            };

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
                    Parent = new RainbowPointLight
                    {
                        Intensity = 1.0f,
                    },
                    RelativeLocation = Vector.Zero,
                };
            }
        }
    }
}
