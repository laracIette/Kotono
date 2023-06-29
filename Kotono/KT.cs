using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Print;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Mathematics;
using System.Collections.Generic;
using Performance = Kotono.Graphics.Performance;
using ShaderType = Kotono.Graphics.ShaderType;
using Text = Kotono.Graphics.Objects.Text;

namespace Kotono
{
    public sealed class KT
    {
        private KT() { }

        public const int MAX_POINT_LIGHTS = PointLightManager.MAX;

        public static string KotonoPath { get; internal set; } = "";

        public static string ProjectPath { get; internal set; } = "";


        private static readonly ComponentManager _componentManager = new();


        public static readonly Viewport CurrentViewport = new();

        #region WindowSize

        private static Vector2 _windowSize = new(0.0f);

        public static float Width => _windowSize.X;

        public static float Height => _windowSize.Y;

        public static void SetWindowSize(float width, float height)
        {
            _windowSize.X = width;
            _windowSize.Y = height;

            GetCamera(0).AspectRatio = 1280f / 720f;
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

        public static Image CreateImage(string path, Rect dest, Vector color)
        {
            return CreateImage(new Image(path, dest, color));
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

        public static int GetPointLightsCount()
        {
            return _objectManager.GetPointLightsCount();
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

        #endregion ObjectManager

        #region SoundManager

        private static readonly SoundManager _soundManager = new();

        public static int CreateSound(string path)
            => _soundManager.Create(path);

        public static void DeleteSound(int index)
            => _soundManager.Delete(index);

        public static void PlaySound(int index)
            => _soundManager.Play(index);

        public static bool IsSoundPlaying(int index)
            => _soundManager.IsPlaying(index);

        public static void PauseSound(int index)
            => _soundManager.Pause(index);

        public static bool IsSoundPaused(int index)
            => _soundManager.IsPaused(index);

        public static void RewindSound(int index)
            => _soundManager.Rewind(index);

        public static void StopSound(int index)
            => _soundManager.Stop(index);

        public static bool IsSoundStopped(int index)
            => _soundManager.IsStopped(index);

        public static float GetSoundVolume(int index)
            => _soundManager.GetVolume(index);

        public static void SetSoundVolume(int index, float volume)
            => _soundManager.SetVolume(index, volume);

        public static float GetGeneralVolume()
            => _soundManager.GeneralVolume;

        public static void SetGeneralVolume(float volume)
            => _soundManager.GeneralVolume = volume;

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

        #endregion ShaderManager

        #region Printer

        private static readonly Printer _printer = new();

        public static void Print(object? obj)
        {
            if (obj != null)
            {
                _printer.Print(obj.ToString());
            }
        }

        public static void Print()
        {
            Print("");
        }

        #endregion Printer

        #region PerformanceWindow

        private static readonly Performance.Window _performanceWindow = new();

        public static int MaxFrameRate { get; set; } = 60;

        internal static void AddFrameTime(double frameTime)
            => _performanceWindow.AddFrameTime(frameTime);

        internal static void AddUpdateTime(double updateTime)
            => _performanceWindow.AddUpdateTime(updateTime);

        public static double GetFrameTime()
            => _performanceWindow.FrameTime;

        public static double GetUpdateTime()
            => _performanceWindow.UpdateTime;

        public static double GetFrameRate()
            => _performanceWindow.FrameRate;

        public static double GetUpdateRate()
            => _performanceWindow.UpdateRate;

        #endregion PerformanceWindow

        #region Gizmo

        private readonly static Gizmo _gizmo = new();

        public static Gizmo Gizmo => _gizmo;

        #endregion Gizmo

        public static void Init()
        {
            _shaderManager.Init();
            _gizmo.Init();
            _objectManager.Init();
            Text.InitPaths();
            _printer.Init();
            _performanceWindow.Init();
            _componentManager.Init();
            Fiziks.Init();
        }

        public static void Update()
        {
            Time.Update();
            _gizmo.Update();
            _objectManager.Update();
            _componentManager.Update();
            _cameraManager.Update();
            _printer.Update();
            _performanceWindow.Update();
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

        public static void Exit()
        {
            _soundManager.Dispose();
        }
    }
}
