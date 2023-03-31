using Kotono.Graphics;
using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using ShaderType = Kotono.Graphics.ShaderType;

namespace Kotono
{
    public class Window : GameWindow
    {
        private readonly SpotLight _spotLight = new();

        private int _frameColorTexture;

        private int _frameDepthTexture;

        private int _frameBufferObject;

        int[] _pingpongFBO;

        int[] _pingpongBuffer;

        int[] _colorBuffers;

        private readonly float[] _frameVertices =
        {
            // positions   // texCoords
            -1.0f,  1.0f,  0.0f, 1.0f,
            -1.0f, -1.0f,  0.0f, 0.0f,
             1.0f, -1.0f,  1.0f, 0.0f,

            -1.0f,  1.0f,  0.0f, 1.0f,
             1.0f, -1.0f,  1.0f, 0.0f,
             1.0f,  1.0f,  1.0f, 1.0f
        };

        private int _frameVertexArrayObject;

        private int _frameVertexBufferObject;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            KT.CreateCamera(new Camera());

            KT.SetCameraAspectRatio(0, (float)Size.X / (float)Size.Y);

            CreateFrameQuad();
            CreateFrameBuffer();

            InputManager.Update(KeyboardState, MouseState);
        }

        protected new void Load()
        {
            CursorState = CursorState.Grabbed;
            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            if (!IsFocused) return;


            // clear frame buffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferObject);
            
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            
            // update shaders
            KT.SetShaderFloat(ShaderType.Lighting, "spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.CutOffAngle)));
            KT.SetShaderFloat(ShaderType.Lighting, "spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(_spotLight.OuterCutOffAngle)));

            KT.RenderFrame();


            // unbind frame buffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);


            int horizontal = 1;
            bool first_iteration = true;
            int amount = 10;

            KT.UseShader(ShaderType.Bloom);

            for (int i = 0; i < amount; i++)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _pingpongFBO[horizontal]);
                KT.SetShaderInt(ShaderType.Bloom, "horizontal", horizontal);
                GL.BindTexture(TextureTarget.Texture2D, first_iteration ? _colorBuffers[1] : _pingpongBuffer[(horizontal == 0) ? 1 : 0]);
                
                RenderFrameQuad();

                horizontal = (horizontal == 0) ? 1 : 0;
                if (first_iteration)
                {
                    first_iteration = false;
                }
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);


            //GL.Disable(EnableCap.DepthTest);
            //GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            //GL.Clear(ClearBufferMask.ColorBufferBit);


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            KT.UseShader(ShaderType.Frame);
            
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _colorBuffers[0]);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, _pingpongBuffer[(horizontal == 0) ? 1 : 0]);

            KT.SetShaderInt(ShaderType.Frame, "bloom", 1);
            KT.SetShaderFloat(ShaderType.Frame, "exposure", 1.0f);

            RenderFrameQuad();

            base.SwapBuffers();
        }

        private void RenderFrameQuad()
        {
            GL.BindVertexArray(_frameVertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            InputManager.Update(KeyboardState, MouseState);

            KT.Update();

            if (InputManager.KeyboardState!.IsKeyDown(InputManager.Escape))
            {
                base.Close();
            }

            if (InputManager.KeyboardState.IsKeyPressed(InputManager.Fullscreen))
            {
                WindowState = (WindowState == WindowState.Normal) ?
                    WindowState.Fullscreen :
                    WindowState.Normal;
            }

            if (InputManager.KeyboardState.IsKeyPressed(InputManager.GrabMouse))
            {
                CursorState = (CursorState == CursorState.Normal) ?
                    CursorState.Grabbed :
                    CursorState.Normal;
            }

            _spotLight.Update();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            KT.SetCameraAspectRatio(0, (float)Size.X / (float)Size.Y);
        }

        protected override void OnUnload()
        {
            KT.Exit();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            base.OnUnload();
        }

        private void CreateFrameQuad()
        {
            _frameVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_frameVertexArrayObject);

            _frameVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _frameVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _frameVertices.Length, _frameVertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        }

        private void CreateFrameBuffer()
        {
            // create frame buffer
            _frameBufferObject = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferObject);

            _colorBuffers = new int[2];
            GL.GenTextures(2, _colorBuffers);

            for (int i = 0; i < 2; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, _colorBuffers[i]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba16f, Size.X, Size.Y, 0, PixelFormat.Rgba, PixelType.Float, 0);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                
                // attach texture to framebuffer
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0 + i, TextureTarget.Texture2D, _colorBuffers[i], 0);
            }

            var attachments = new DrawBuffersEnum[] { DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1 };
            GL.DrawBuffers(2, attachments);

            // check for errors
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception("Error creating the Frame Buffer Object.");
            }


            _pingpongFBO = new int[2];
            GL.GenFramebuffers(2, _pingpongFBO);

            _pingpongBuffer = new int[2];
            GL.GenTextures(2, _pingpongBuffer);

            for (int i = 0; i < 2; i++)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _pingpongFBO[i]);
                GL.BindTexture(TextureTarget.Texture2D, _pingpongBuffer[i]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba16f, Size.X, Size.Y, 0, PixelFormat.Rgba, PixelType.Float, 0);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _pingpongBuffer[i], 0);
                
                // check for errors
                if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                {
                    throw new Exception("Error creating the Frame Buffer Object.");
                }
            }

        }
    }
}
