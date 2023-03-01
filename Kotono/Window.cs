using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using System.Drawing;

namespace Kotono
{
    public class Window : GameWindow
    {
        private readonly float[] _vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int _elementBufferObject;

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        private Image2D _texture;
        private Image2D _texture2;

        private const string rootPath = @"C:/Users/nicos/Documents/Visual Studio 2022/Projects/Kotono/Kotono/";

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.3f, 1.0f);
            
            GL.Enable(EnableCap.DepthTest);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            string vertPath = rootPath + "Graphics/Shaders/shader.vert";
            string fragPath = rootPath + "Graphics/Shaders/shader.frag";
            _shader = new Shader(vertPath, fragPath);
            _shader.Use();

            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            string imgPath = @"C:/Users/nicos/Documents/Visual Studio 2022/Projects/TestSharp/TestSharp/Assets/container.png";
            _texture = Image2D.LoadFromFile(imgPath, TextureUnit.Texture0);
            _texture.Use();

            imgPath = @"C:/Users/nicos/Documents/Visual Studio 2022/Projects/TestSharp/TestSharp/Assets/MaiGetTheFuckOut.png";
            _texture2 = Image2D.LoadFromFile(imgPath, TextureUnit.Texture1);
            _texture2.Use();

            _shader.SetInt("textures[0]", 0);
            _shader.SetInt("textures[1]", 1);

            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vertexArrayObject);

            _texture.Draw();
            _texture2.Draw();
            _shader.Use();

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            CheckFullscreen();
        }

        private void CheckFullscreen()
        {
            if (!KeyboardState.IsKeyDown(Keys.F11) || KeyboardState.WasKeyDown(Keys.F11)) return;

            WindowState = (WindowState == WindowState.Normal) ? WindowState.Fullscreen : WindowState.Normal;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
        }

    }
}