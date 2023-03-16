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
using Kotono.Graphics.Objects.Hitboxes;

namespace Kotono
{
    public class Window : GameWindow
    {
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


            CameraManager.Main.Position = Vector3.Zero;
            CameraManager.Main.AspectRatio = (float)Size.X / (float)Size.Y;

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

            Time.Update();

            if (InputManager.KeyboardState.IsKeyDown(InputManager.Escape))
            {
                base.Close();
            }

            if (InputManager.KeyboardState.IsKeyPressed(InputManager.Fullscreen))
            {
                WindowState = (WindowState == WindowState.Fullscreen) ?
                    WindowState.Normal :
                    WindowState.Fullscreen;
            }

            if (InputManager.KeyboardState.IsKeyPressed(InputManager.GrabMouse))
            {
                CursorState = (CursorState == CursorState.Normal) ?
                    CursorState.Grabbed :
                    CursorState.Normal;
            }

            _spotLight.Update();

            foreach (var mesh in ObjectManager.Meshes)
            {
                mesh.Update();
            }

            foreach (var pointLight in ObjectManager.PointLights)
            {
                pointLight.Update();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            CameraManager.Main.AspectRatio = (float)Size.X / (float)Size.Y;
        }

        private void UpdateShaders()
        {
            ShaderManager.Lighting.SetInt("numLights", ObjectManager.PointLights.Count);

            ShaderManager.Lighting.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.Lighting.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            ShaderManager.Lighting.SetVector3("viewPos", CameraManager.Main.Position);

            ShaderManager.Lighting.SetInt("material.diffuse", 0);
            ShaderManager.Lighting.SetInt("material.specular", 1);
            ShaderManager.Lighting.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            ShaderManager.Lighting.SetFloat("material.shininess", 32.0f);

            ShaderManager.Lighting.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            ShaderManager.Lighting.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            ShaderManager.Lighting.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            ShaderManager.Lighting.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            foreach (var pointLight in ObjectManager.PointLights)
            {
                pointLight.UpdateShaders();
            }

            ShaderManager.Lighting.SetVector3("spotLight.position", CameraManager.Main.Position);
            ShaderManager.Lighting.SetVector3("spotLight.direction", CameraManager.Main.Front);
            ShaderManager.Lighting.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            ShaderManager.Lighting.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.Lighting.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.Lighting.SetFloat("spotLight.constant", 1.0f);
            ShaderManager.Lighting.SetFloat("spotLight.linear", 0.09f);
            ShaderManager.Lighting.SetFloat("spotLight.quadratic", 0.032f);
            ShaderManager.Lighting.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.CutOffAngle)));
            ShaderManager.Lighting.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.OuterCutOffAngle)));

            ShaderManager.PointLight.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.PointLight.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            foreach (var mesh in ObjectManager.Meshes)
            {
                mesh.Draw();
            } 

            foreach (var pointLight in ObjectManager.PointLights)
            {
                pointLight.Draw();
            }
        }

    }
}
