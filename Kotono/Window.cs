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
using Kotono.Graphics.Objects.Meshes;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using Camera = Kotono.Graphics.Camera;
using System.Runtime.InteropServices;

namespace Kotono
{
    public class Window : GameWindow
    {
        private readonly List<PointLight> _pointLights = new List<PointLight>();

        private Camera _camera;

        private SpotLight _spotLight;

        private float _deltaTime;

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

            if (InputManager.KeyboardState.IsKeyDown(Keys.Escape))
            {
                base.Close();
            }

            _deltaTime = (float)e.Time;

            MoveCamera();

            if (InputManager.KeyboardState.IsKeyDown(Keys.F11) && !InputManager.KeyboardState.WasKeyDown(Keys.F11))
            {
                WindowState = (WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.Enter) && !InputManager.KeyboardState.WasKeyDown(Keys.Enter))
            {
                CursorState = (CursorState == CursorState.Normal) ? CursorState.Grabbed : CursorState.Normal;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.F) && !InputManager.KeyboardState.WasKeyDown(Keys.F))
            {
                _spotLight.Switch();
            }

            _spotLight.Update(_deltaTime);

            foreach (var model in ObjectManager._meshes)
            {
                model.Update(_deltaTime, ObjectManager._meshes.Where(c => (Vector3.Distance(c.Position, model.Position) <= 2.0f) && (c != model)));
            }

            foreach (var pointLight in _pointLights)
            {
                pointLight.Update(_deltaTime);
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

        private void CreateLights() // TODO
        {
            _spotLight = new SpotLight(true);

            for (int i = 0; i < 20; i++)
            {
                var position = Random.Vector3(-20.0f, 20.0f);

                _pointLights.Add(new PointLight(position));
            }

            GL.GenBuffers(1, out int lightBuffer);
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, lightBuffer);
            GL.BufferData(BufferTarget.ShaderStorageBuffer, Marshal.SizeOf(typeof(LightBuffer)) * 256, IntPtr.Zero, BufferUsageHint.DynamicDraw);

            int bindingPoint = 0; // choose a binding point index
            int lightBufferIndex = GL.GetProgramResourceIndex(ShaderManager.LightingShader.Handle, ProgramInterface.ShaderStorageBlock, "LightBuffer");
            GL.ShaderStorageBlockBinding(ShaderManager.LightingShader.Handle, lightBufferIndex, bindingPoint);
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, bindingPoint, lightBuffer);
        }

        private void UpdateShaders()
        {
            LightBuffer lightBufferData = new LightBuffer
            {
                Lights = _pointLights.ToArray(),
                NumLights = _pointLights.Count
            };

            IntPtr bufferPointer = GL.MapBufferRange(BufferTarget.ShaderStorageBuffer, IntPtr.Zero, Marshal.SizeOf(typeof(LightBuffer)) * 256, BufferAccessMask.MapWriteBit | BufferAccessMask.MapInvalidateBufferBit);
            Marshal.StructureToPtr(lightBufferData, bufferPointer, false);
            GL.UnmapBuffer(BufferTarget.ShaderStorageBuffer);

            ShaderManager.LightingShader.Use();

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

            for (int i = 0; i < _pointLights.Count; i++)
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

            foreach (var mesh in ObjectManager._meshes)
            {
                mesh.Draw();
            } 
        }

        private void MoveCamera()
        {
            float cameraSpeed = 1.5f;
            float sensitivity = 0.2f;

            if (InputManager.KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                cameraSpeed *= 2.0f;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * _deltaTime; // Forward
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * _deltaTime; // Backwards
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * _deltaTime; // Left
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * _deltaTime; // Right
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * _deltaTime; // Up
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * _deltaTime; // Down
            }

            _camera.Yaw += InputManager.MouseState.Delta.X * sensitivity;
            _camera.Pitch -= InputManager.MouseState.Delta.Y * sensitivity;
        }

    }
}
