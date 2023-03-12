using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Kotono.Graphics.Objects;
using Kotono.Graphics;
using Random = Kotono.Utils.Random;
using Kotono.Utils;
using Kotono.Graphics.Objects.Lights;
using Camera = Kotono.Graphics.Camera;
using System.Runtime.InteropServices;

namespace Kotono
{
    public class Window : GameWindow
    {
        private readonly List<PointLight> _pointLights = new List<PointLight>();

        private Camera _camera;

        private SpotLight _spotLight;

        private int _numLights = 0;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            CreateLights();

            _camera = new Camera(Vector3.Zero, (float)Size.X / (float)Size.Y);

            CursorState = CursorState.Grabbed;
            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            UpdateShaders();

            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }

            InputManager.Update(KeyboardState, MouseState);

            Time.Update((float)e.Time);

            if (InputManager.KeyboardState.IsKeyDown(Keys.Escape))
            {
                base.Close();
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.F11) && !InputManager.KeyboardState.WasKeyDown(Keys.F11))
            {
                WindowState = (WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.Enter) && !InputManager.KeyboardState.WasKeyDown(Keys.Enter))
            {
                CursorState = (CursorState == CursorState.Normal) ? CursorState.Grabbed : CursorState.Normal;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.Up) && !InputManager.KeyboardState.WasKeyDown(Keys.Up))
            {
                if (_numLights < 20)
                {
                    _numLights++;
                }
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.Down) && !InputManager.KeyboardState.WasKeyDown(Keys.Down))
            {
                if (_numLights > 0)
                {
                    _numLights--;
                }
            }

            _camera.Move();

            _spotLight.Update();

            foreach (var mesh in ObjectManager.Meshes)
            {
                mesh.Update();
            }

            foreach (var pointLight in _pointLights)
            {
                pointLight.Update();
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = (float)Size.X / (float)Size.Y;
        }

        private void CreateLights()
        {
            _spotLight = new SpotLight(true);

            for (int i = 0; i < 20; i++)
            {
                var position = Random.Vector3(-20.0f, 20.0f);

                _pointLights.Add(new PointLight(position));
            }
        }

        private void UpdateShaders()
        {
            ShaderManager.LightingShader.SetInt("numLights", _numLights);

            ShaderManager.LightingShader.SetMatrix4("view", _camera.ViewMatrix);
            ShaderManager.LightingShader.SetMatrix4("projection", _camera.ProjectionMatrix);

            ShaderManager.LightingShader.SetVector3("viewPos", _camera.Position);

            ShaderManager.LightingShader.SetInt("material.diffuse", 0);
            ShaderManager.LightingShader.SetInt("material.specular", 1);
            ShaderManager.LightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            ShaderManager.LightingShader.SetFloat("material.shininess", 32.0f);

            ShaderManager.LightingShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            ShaderManager.LightingShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            ShaderManager.LightingShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            ShaderManager.LightingShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            for (int i = 0; i < _numLights; i++)
            {
                ShaderManager.LightingShader.SetVector3($"pointLights[{i}].position", _pointLights[i].Position);
                ShaderManager.LightingShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                ShaderManager.LightingShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                ShaderManager.LightingShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                ShaderManager.LightingShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                ShaderManager.LightingShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                ShaderManager.LightingShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }

            ShaderManager.LightingShader.SetVector3("spotLight.position", _camera.Position);
            ShaderManager.LightingShader.SetVector3("spotLight.direction", _camera.Front);
            ShaderManager.LightingShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            ShaderManager.LightingShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.LightingShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.LightingShader.SetFloat("spotLight.constant", 1.0f);
            ShaderManager.LightingShader.SetFloat("spotLight.linear", 0.09f);
            ShaderManager.LightingShader.SetFloat("spotLight.quadratic", 0.032f);
            ShaderManager.LightingShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.CutOffAngle)));
            ShaderManager.LightingShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.OuterCutOffAngle)));

            foreach (var mesh in ObjectManager.Meshes)
            {
                mesh.Draw();
            } 
        }

    }
}
