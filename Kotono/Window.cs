using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using Random = Kotono.Utils.Random;
using Camera = Kotono.Graphics.Camera;
using Assimp;

namespace Kotono
{
    public class Window : GameWindow
    {
        private Camera _camera;

        private SpotLight _spotLight;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            _spotLight = new SpotLight(true);

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

            if (InputManager.KeyboardState.IsKeyPressed(Keys.F11))
            {
                WindowState = (WindowState == WindowState.Fullscreen) ? 
                    WindowState.Normal : 
                    WindowState.Fullscreen;
            }

            if (InputManager.KeyboardState.IsKeyPressed(Keys.Enter))
            {
                CursorState = (CursorState == CursorState.Normal) ? 
                    CursorState.Grabbed : 
                    CursorState.Normal;
            }

            if (InputManager.KeyboardState.IsKeyPressed(Keys.Up))
            {
                if (ObjectManager.PointLights.Count < 20)
                {
                    ObjectManager.LoadPointLight(Random.Vector3(-20.0f, 20.0f)); ;
                }
            }

            if (InputManager.KeyboardState.IsKeyPressed(Keys.Down))
            {
                if (ObjectManager.PointLights.Count > 0)
                {
                    ObjectManager.RemovePointLight(0);

                    foreach (var pointLight in ObjectManager.PointLights)
                    {
                        pointLight.UpdateIndex();
                    }
                }
            }

            _camera.Move();

            _spotLight.Update();

            foreach (var mesh in ObjectManager.Meshes)
            {
                mesh.Update();
            }

            foreach (var pointLight in ObjectManager.PointLights)
            {
                pointLight.Update(ObjectManager.Meshes[pointLight.MeshIndex].Position);
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

        private void UpdateShaders()
        {
            ShaderManager.Lighting.SetInt("numLights", ObjectManager.PointLights.Count);

            ShaderManager.Lighting.SetMatrix4("view", _camera.ViewMatrix);
            ShaderManager.Lighting.SetMatrix4("projection", _camera.ProjectionMatrix);

            ShaderManager.Lighting.SetVector3("viewPos", _camera.Position);

            ShaderManager.Lighting.SetInt("material.diffuse", 0);
            ShaderManager.Lighting.SetInt("material.specular", 1);
            ShaderManager.Lighting.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            ShaderManager.Lighting.SetFloat("material.shininess", 32.0f);

            ShaderManager.Lighting.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            ShaderManager.Lighting.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            ShaderManager.Lighting.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            ShaderManager.Lighting.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            for (int i = 0; i < ObjectManager.PointLights.Count; i++)
            {
                ShaderManager.Lighting.SetVector3($"pointLights[{i}].position", ObjectManager.PointLights[i].Position);
                ShaderManager.Lighting.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                ShaderManager.Lighting.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                ShaderManager.Lighting.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                ShaderManager.Lighting.SetFloat($"pointLights[{i}].constant", 1.0f);
                ShaderManager.Lighting.SetFloat($"pointLights[{i}].linear", 0.09f);
                ShaderManager.Lighting.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }

            ShaderManager.Lighting.SetVector3("spotLight.position", _camera.Position);
            ShaderManager.Lighting.SetVector3("spotLight.direction", _camera.Front);
            ShaderManager.Lighting.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            ShaderManager.Lighting.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.Lighting.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.Lighting.SetFloat("spotLight.constant", 1.0f);
            ShaderManager.Lighting.SetFloat("spotLight.linear", 0.09f);
            ShaderManager.Lighting.SetFloat("spotLight.quadratic", 0.032f);
            ShaderManager.Lighting.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.CutOffAngle)));
            ShaderManager.Lighting.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.OuterCutOffAngle)));
            
            ShaderManager.PointLight.SetMatrix4("view", _camera.ViewMatrix);
            ShaderManager.PointLight.SetMatrix4("projection", _camera.ProjectionMatrix);

            foreach (var mesh in ObjectManager.Meshes)
            {
                mesh.Draw();
            } 
        }

    }
}
