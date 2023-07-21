using Kotono.Audio;
using Kotono.Engine;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Print;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Mathematics;
using System.Collections.Generic;
using Performance = Kotono.Graphics.Performance;
using ShaderType = Kotono.Graphics.ShaderType;
using Text = Kotono.Graphics.Objects.Text;

namespace Kotono
{
    public static class KT
    {
        public static string KotonoPath { get; internal set; } = "";

        public static string ProjectPath { get; internal set; } = "";

        private static readonly ComponentManager _componentManager = new();

        #region Viewport

        public static readonly Viewport _viewport = new();

        public static Viewport ActiveViewport => _viewport;

        #endregion Viewport

        #region WindowSize

        private static Rect _windowDest = Rect.Zero;

        public static Rect Dest => _windowDest;

        public static Point Position => _windowDest.Position;

        public static Point Size => _windowDest.Size;

        public static void SetWindowPosition(Point position)
        {
            _windowDest.Position = position;
        }

        public static void SetWindowSize(Point size)
        {
            _windowDest.Size = size;

            ActiveCamera.AspectRatio = size.X / size.Y;

            ActiveViewport.SetSize(size);
        }

        #endregion WindowSize

        #region ObjectManager

        private static readonly ObjectManager _objectManager = new();

        #region Image

        public static Image CreateImage(Image image)
        {
            _objectManager.CreateImage(image);
            return image;
        }

        public static void DeleteImage(Image image)
        {
            _objectManager.DeleteImage(image);
        }

        #endregion Image

        #region Mesh

        public static Mesh CreateMesh(Mesh mesh)
        {
            _objectManager.CreateMesh(mesh);
            return mesh;
        }

        public static void DeleteMesh(Mesh mesh)
        {
            _objectManager.DeleteMesh(mesh);
        }

        #endregion Mesh

        #region FrontMesh

        public static FrontMesh CreateFrontMesh(FrontMesh frontMesh)
        {
            _objectManager.CreateFrontMesh(frontMesh);
            return frontMesh;
        }

        public static void DeleteFrontMesh(FrontMesh frontMesh)
        {
            _objectManager.DeleteFrontMesh(frontMesh);
        }

        #endregion FrontMesh

        #region Hitbox

        public static IHitbox CreateHitbox(IHitbox hitbox)
        {
            _objectManager.CreateHitbox(hitbox);
            return hitbox;
        }

        public static void DeleteHitbox(IHitbox hitbox)
        {
            _objectManager.DeleteHitbox(hitbox);
        }

        public static List<IHitbox> GetAllHitboxes()
        {
            return _objectManager.GetAllHitboxes();
        }

        #endregion Hitbox

        #region PointLight

        public static PointLight CreatePointLight(PointLight pointLight)
        {
            _objectManager.CreatePointLight(pointLight);
            return pointLight;
        }

        public static void DeletePointLight(PointLight pointLight)
        {
            _objectManager.DeletePointLight(pointLight);
        }

        public static PointLight GetFirstPointLight()
        {
            return _objectManager.GetFirstPointLight();
        }

        #endregion PointLight

        #region SpotLight

        public static SpotLight CreateSpotLight(SpotLight spotLight)
        {
            _objectManager.CreateSpotLight(spotLight);
            return spotLight;
        }

        public static void DeleteSpotLight(SpotLight spotLight)
        {
            _objectManager.DeleteSpotLight(spotLight);
        }

        public static int GetSpotLightsCount()
        {
            return _objectManager.GetSpotLightsCount();
        }

        #endregion SpotLight

        #region Triangle

        public static Triangle CreateTriangle(Triangle triangle)
        {
            _objectManager.CreateTriangle(triangle);
            return triangle;
        }

        public static void DeleteTriangle(Triangle triangle)
        {
            _objectManager.DeleteTriangle(triangle);
        }

        #endregion Triangle

        #region RoundedBox

        public static RoundedBox CreateRoundedBox(RoundedBox box)
        {
            _objectManager.CreateRoundedBox(box);
            return box;
        }

        public static void DeleteRoundedBox(RoundedBox box)
        {
            _objectManager.DeleteRoundedBox(box);
        }

        #endregion RoundedBox

        #region RoundedBorder

        public static RoundedBox CreateRoundedBorder(RoundedBorder border)
        {
            _objectManager.CreateRoundedBorder(border);
            return border;
        }

        public static void DeleteRoundedBorder(RoundedBorder border)
        {
            _objectManager.DeleteRoundedBorder(border);
        }

        #endregion RoundedBorder

        #endregion ObjectManager

        #region SoundManager

        private static readonly SoundManager _soundManager = new();

        public static Sound CreateSound(string path)
        {
            return _soundManager.Create(path);
        }

        public static void DeleteSound(Sound sound)
        {
            _soundManager.Delete(sound);
        }

        #endregion SoundManager

        #region CameraManager

        private static readonly CameraManager _cameraManager = new();

        public static Camera ActiveCamera => GetCamera(0);

        public static Camera CreateCamera(Camera camera)
        {
            _cameraManager.Create(camera);
            return camera;
        }

        public static void DeleteCamera(Camera camera)
        {
            _cameraManager.Delete(camera);
        }

        public static Camera GetCamera(int index)
        {
            return _cameraManager.Get(index);
        }


        #endregion CameraManager

        #region ShaderManager

        private static readonly ShaderManager _shaderManager = new();

        public static int GetShaderAttribLocation(ShaderType type, string attribName)
        {
            return _shaderManager.GetAttribLocation(type, attribName);
        }

        public static void SetShaderBool(ShaderType type, string name, bool data)
        {
            _shaderManager.SetBool(type, name, data);
        }

        public static void SetShaderInt(ShaderType type, string name, int data)
        {
            _shaderManager.SetInt(type, name, data);
        }

        public static void SetShaderFloat(ShaderType type, string name, float data)
        {
            _shaderManager.SetFloat(type, name, data);
        }

        public static void SetShaderMatrix4(ShaderType type, string name, Matrix4 data)
        {
            _shaderManager.SetMatrix4(type, name, data);
        }

        public static void SetShaderVector(ShaderType type, string name, Vector data)
        {
            _shaderManager.SetVector(type, name, data);
        }

        public static void SetShaderColor(ShaderType type, string name, Color data)
        {
            _shaderManager.SetColor(type, name, data);
        }

        public static void SetShaderRect(ShaderType type, string name, Rect data)
        {
            _shaderManager.SetRect(type, name, data);
        }

        #endregion ShaderManager

        #region Printer

        private static readonly Printer _printer = new();

        public static void Print(object? obj, Color color)
        {
            if (obj != null)
            {
                _printer.Print(obj.ToString(), color);
            }
        }

        public static void Print(object? obj)
        {
            Print(obj, Color.White);
        }

        public static void Print()
        {
            Print("");
        }

        #endregion Printer

        #region PerformanceWindow

        private static readonly Performance.Window _performanceWindow = new();

        public static int MaxFrameRate { get; set; } = 60;

        public static void AddFrameTime(double frameTime)
        {
            _performanceWindow.AddFrameTime(frameTime);
        }

        public static void AddUpdateTime(double updateTime)
        {
            _performanceWindow.AddUpdateTime(updateTime);
        }

        public static double GetFrameTime()
        {
            return _performanceWindow.FrameTime;
        }

        public static double GetUpdateTime()
        {
            return _performanceWindow.UpdateTime;
        }

        public static double GetFrameRate()
        {
            return _performanceWindow.FrameRate;
        }

        public static double GetUpdateRate()
        {
            return _performanceWindow.UpdateRate;
        }

        #endregion PerformanceWindow

        #region Gizmo

        private readonly static Gizmo _gizmo = new();

        public static Gizmo Gizmo => _gizmo;

        #endregion Gizmo

        #region UserMode

        private static readonly Mode _mode = new();

        public static UserMode UserMode => _mode.UserMode;

        #endregion UserMode

        public static void Init()
        {
            _shaderManager.Init();
            SquareVertices.Init();
            _gizmo.Init();
            _objectManager.Init();
            Text.InitPaths();
            _printer.Init();
            _performanceWindow.Init();
            _componentManager.Init();
            Fizix.Init();
            _mode.Init();
        }

        public static void Update()
        {
            Time.Update();
            Mouse.Update();
            Keyboard.Update();
            _gizmo.Update();
            _objectManager.Update();
            _componentManager.Update();
            _cameraManager.Update();
            _printer.Update();
            _performanceWindow.Update();
            _mode.Update();
        }

        public static void RenderFrame()
        {
            UpdateShaders();
            _objectManager.Draw();
            _componentManager.Draw();
        }

        private static void UpdateShaders()
        {
            _objectManager.UpdateShaders();
            _componentManager.UpdateShaders();
            _shaderManager.Update();
        }

        public static void Save()
        {
            _objectManager.Save();
        }

        public static void Exit()
        {
            _soundManager.Dispose();
        }
    }
}
