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

            _animation = new Animation(JsonParser.Parse<AnimationSettings>(Path.ASSETS + @"Animations\Counting\Counting.json"));

            _timer = new Timer();
            _timer.Timeout += OnTimerTimeout;

            CreateObjects();

            Keyboard.SubscribeKeyPressed(OnEnterPressed, Keys.Enter);
            Keyboard.SubscribeKeyPressed(OnTPressed, Keys.T);
            Keyboard.SubscribeKeyPressed(OnIPressed, Keys.I);
            Keyboard.SubscribeKeyPressed(OnJPressed, Keys.J); 
            Keyboard.SubscribeKeyPressed(OnKPressed, Keys.K);
        }

        private void OnTimerTimeout(object? sender, TimeoutEventArgs e)
        {
            Printer.Print((int)e.Time, true);
        }

        private void OnEnterPressed(object? sender, TimedEventArgs e)
        {
            Mouse.CursorState = (CursorState)Math.Loop((float)Mouse.CursorState + 1.0f, 3.0f);
        }

        private void OnTPressed(object? sender, TimedEventArgs e)
        {
            _animation.Switch();
        }

        private void OnIPressed(object? sender, TimedEventArgs e)
        {
            Printer.Print(Time.Now, true);
        }

        private void OnJPressed(object? sender, TimedEventArgs e)
        {
            foreach (var obj in ISelectable.Selected3D.Where(s => s != ISelectable.Active))
            {
                obj.Parent = ISelectable.Active3D;
            }
        }

        private void OnKPressed(object? sender, TimedEventArgs e)
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
                RelativeLocation = new Vector(-3.0f, 0.0f, -3.0f)
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
