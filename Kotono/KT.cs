using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Print;
using Kotono.Utils;
using OpenTK.Mathematics;
using Performance = Kotono.Graphics.Performance;
using ShaderType = Kotono.Graphics.ShaderType;
using Text = Kotono.Graphics.Objects.Text;

namespace Kotono
{
    public sealed class KT
    {
        private KT() { }

        public const int MAX_POINT_LIGHTS = PointLightManager.MAX;

        #region Path

        public static string KotonoPath { get; set; } = "";

        public static string ProjectPath { get; set; } = "";

        #endregion Path

        private static readonly ComponentManager _componentManager = new();

        #region WindowSize

        private static Vector2 _windowSize = new(0.0f);
        
        public static float Width => _windowSize.X;

        public static float Height => _windowSize.Y;

        public static void SetWindowSize(float width, float height)
        {
            _windowSize.X = width;
            _windowSize.Y = height;

            SetCameraAspectRatio(0, 1280f / 720f);
        }

        #endregion WindowSize

        #region CurrentViewport

        private static readonly Viewport _currentViewport = new();

        public static float CurrentViewportWidth
        {
            get => _currentViewport.W;
            set => _currentViewport.W = value;
        }
        
        public static float CurrentViewportHeight
        {
            get => _currentViewport.H;
            set => _currentViewport.H = value;
        }

        #endregion CurrentViewport

        #region ObjectManager
        
        private static readonly ObjectManager _objectManager = new();

        #region Image

        public static int CreateImage(Image image)
            => _objectManager.CreateImage(image);

        public static void DeleteImage(int index)
            => _objectManager.DeleteImage(index);

        public static void GetImageRect(int index)
            => _objectManager.GetImageRect(index);

        public static void SetImageX(int index, float x)
            => _objectManager.SetImageX(index, x);

        public static void SetImageY(int index, float y)
            => _objectManager.SetImageY(index, y);

        public static void SetImageW(int index, float w)
            => _objectManager.SetImageW(index, w);

        public static void SetImageH(int index, float h)
            => _objectManager.SetImageH(index, h);

        public static void TransformImage(int index, Rect transformation, double time)
            => _objectManager.TransformImage(index, transformation, time);

        public static void TransformImageTo(int index, Rect dest, double time)
            => _objectManager.TransformImageTo(index, dest, time);

        public static void ShowImage(int index)
            => _objectManager.ShowImage(index);

        public static void HideImage(int index)
            => _objectManager.HideImage(index);

        #endregion Image

        #region Mesh

        public static int CreateMesh(IMesh mesh)
            => _objectManager.CreateMesh(mesh);

        public static void DeleteMesh(int index)
            => _objectManager.DeleteMesh(index);

        public static Vector3 GetMeshPosition(int index)
            => _objectManager.GetMeshPosition(index);

        public static void SetMeshColor(int index, Vector3 color)
            => _objectManager.SetMeshColor(index, color);

        #endregion Mesh

        #region Hitbox

        public static int CreateHitbox(IHitbox hitbox)
            => _objectManager.CreateHitbox(hitbox);

        public static void DeleteHitbox(int index)
            => _objectManager.DeleteHitbox(index);

        public static void SetHitBoxPosition(int index, Vector3 position)
            => _objectManager.SetHitBoxPosition(index, position);

        public static void SetHitBoxAngle(int index, Vector3 angle)
            => _objectManager.SetHitBoxAngle(index, angle);

        public static void SetHitBoxScale(int index, Vector3 scale)
            => _objectManager.SetHitBoxScale(index, scale);

        public static void SetHitBoxColor(int index, Vector3 color)
            => _objectManager.SetHitBoxColor(index, color);

        public static void AddHitboxCollision(int index, int hitboxIndex)
            => _objectManager.AddHitboxCollision(index, hitboxIndex);

        public static void AddHitboxCollision(int index, int[] hitboxIndexes)
            => _objectManager.AddHitboxCollision(index, hitboxIndexes);

        public static int[] GetAllHitboxes()
            => _objectManager.GetAllHitboxes();

        public static bool IsHitboxColliding(int index)
            => _objectManager.IsHitboxColliding(index);

        #endregion Hitbox

        #region PointLight

        public static int CreatePointLight(PointLight pointLight)
            => _objectManager.CreatePointLight(pointLight);

        public static void DeletePointLight(int index)
            => _objectManager.DeletePointLight(index);

        public static int GetPointLightsCount()
            => _objectManager.GetPointLightsCount();

        public static int GetFirstPointLightIndex()
            => _objectManager.GetFirstPointLightIndex();

        #endregion PointLight

        #region SpotLight

        public static int CreateSpotLight(SpotLight spotLight)
            => _objectManager.CreateSpotLight(spotLight);

        public static void DeleteSpotLight(int index)
            => _objectManager.DeleteSpotLight(index);

        public static int GetSpotLightsCount()
            => _objectManager.GetSpotLightsCount();
       
        #endregion SpotLight

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

        public static int CreateCamera(Camera camera)
            => _cameraManager.Create(camera);

        public static void DeleteCamera(int index) 
            => _cameraManager.Delete(index);

        public static Vector3 GetCameraPosition(int index)
            => _cameraManager.GetPosition(index);

        public static Matrix4 GetCameraViewMatrix(int index)
            => _cameraManager.GetViewMatrix(index);

        public static Matrix4 GetCameraProjectionMatrix(int index)
            => _cameraManager.GetProjectionMatrix(index);
        
        public static Vector3 GetCameraFront(int index)
            => _cameraManager.GetFront(index);

        public static Vector3 GetCameraRight(int index)
            => _cameraManager.GetRight(index);

        public static Vector3 GetCameraUp(int index)
            => _cameraManager.GetUp(index);

        public static void SetCameraAspectRatio(int index, float aspectRatio)
            => _cameraManager.SetAspectRatio(index, aspectRatio);

        #endregion CameraManager

        #region ShaderManager
        
        private static readonly ShaderManager _shaderManager = new();

        public static int GetShaderAttribLocation(ShaderType type, string attribName)
            => _shaderManager.GetAttribLocation(type, attribName);

        public static void SetShaderInt(ShaderType type, string name, int data)
            => _shaderManager.SetInt(type, name, data);

        public static void SetShaderFloat(ShaderType type, string name, float data)
            => _shaderManager.SetFloat(type, name, data);

        public static void SetShaderMatrix4(ShaderType type, string name, Matrix4 data)
            => _shaderManager.SetMatrix4(type, name, data);

        public static void SetShaderVector2(ShaderType type, string name, Vector2 data)
            => _shaderManager.SetVector2(type, name, data);

        public static void SetShaderVector3(ShaderType type, string name, Vector3 data)
            => _shaderManager.SetVector3(type, name, data);

        public static void SetShaderVector4(ShaderType type, string name, Vector4 data)
            => _shaderManager.SetVector4(type, name, data);

        #endregion ShaderManager

        #region Printer
        
        private static readonly Printer _printer = new();

        public static void Print(string text)
            => _printer.Print(text);

        #endregion Printer

        #region PerformanceWindow
        
        private static readonly Performance.Window _performanceWindow = new();

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


        public static void Init()
        {
            _shaderManager.Init();
            Text.InitPaths();
            _performanceWindow.Init();
            _componentManager.Init();
        }

        public static void Update()
        {
            Time.Update();
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
