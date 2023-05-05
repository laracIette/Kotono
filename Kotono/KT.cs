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


namespace Kotono
{
    public sealed class KT
    {
        private KT() { }

        private static Vector2 _windowSize = new(0.0f);

        public const int MAX_POINT_LIGHTS = PointLightManager.MAX;

        public static string KotonoPath { get; set; } = "";

        public static string ProjectPath { get; set; } = "";

        private static ObjectManager ObjectManager { get; } = new();

        private static SoundManager SoundManager { get; } = new();

        private static CameraManager CameraManager { get; } = new();

        private static ShaderManager ShaderManager { get; } = new();

        private static Printer Printer { get; } = new();

        private static Performance.Window PerformanceWindow { get; } = new();

        public static float Width => _windowSize.X;

        public static float Height => _windowSize.Y;

        public static void Init()
        {
            ShaderManager.Init();
            Text.InitPaths();
        }

        public static void SetWindowSize(float width, float height)
        {
            SetCameraAspectRatio(0, width / height);
            _windowSize.X = width;
            _windowSize.Y = height;
        }

        public static int CreateImage(Image image)
            => ObjectManager.CreateImage(image);

        public static int CreateMesh(IMesh mesh)
            => ObjectManager.CreateMesh(mesh);

        public static int CreateHitbox(IHitbox hitbox)
            => ObjectManager.CreateHitbox(hitbox);

        public static int CreatePointLight(PointLight pointLight)
            => ObjectManager.CreatePointLight(pointLight);

        public static int CreateSpotLight(SpotLight spotLight)
            => ObjectManager.CreateSpotLight(spotLight);

        public static Vector3 GetMeshPosition(int index)
            => ObjectManager.GetMeshPosition(index);

        public static void SetMeshColor(int index, Vector3 color)
            => ObjectManager.SetMeshColor(index, color);

        public static void DeleteImage(int index)
            => ObjectManager.DeleteImage(index);

        public static void DeleteMesh(int index) 
            => ObjectManager.DeleteMesh(index);

        public static void DeleteHitbox(int index)
            => ObjectManager.DeleteHitbox(index);

        public static void DeletePointLight(int index)
            => ObjectManager.DeletePointLight(index);

        public static void DeleteSpotLight(int index)
            => ObjectManager.DeleteSpotLight(index);

        public static void GetImageRect(int index)
            => ObjectManager.GetImageRect(index);

        public static void SetImageX(int index, float x)
            => ObjectManager.SetImageX(index, x);
        
        public static void SetImageY(int index, float y)
            => ObjectManager.SetImageY(index, y);
        
        public static void SetImageW(int index, float w)
            => ObjectManager.SetImageW(index, w);
        
        public static void SetImageH(int index, float h)
            => ObjectManager.SetImageH(index, h);

        public static void TransformImage(int index, Rect transformation, double time)
            => ObjectManager.TransformImage(index, transformation, time);
        
        public static void TransformImageTo(int index, Rect dest, double time)
            => ObjectManager.TransformImageTo(index, dest, time);

        public static void ShowImage(int index)
            => ObjectManager.ShowImage(index);

        public static void HideImage(int index)
            => ObjectManager.HideImage(index);

        public static void SetHitBoxPosition(int index, Vector3 position)
            => ObjectManager.SetHitBoxPosition(index, position);

        public static void SetHitBoxAngle(int index, Vector3 angle)
            => ObjectManager.SetHitBoxAngle(index, angle);

        public static void SetHitBoxScale(int index, Vector3 scale)
            => ObjectManager.SetHitBoxScale(index, scale);

        public static void SetHitBoxColor(int index, Vector3 color)
            => ObjectManager.SetHitBoxColor(index, color);

        public static void AddHitboxCollision(int index, int hitboxIndex)
            => ObjectManager.AddHitboxCollision(index, hitboxIndex);

        public static void AddHitboxCollision(int index, int[] hitboxIndexes)
            => ObjectManager.AddHitboxCollision(index, hitboxIndexes);

        public static int[] GetAllHitboxes() 
            => ObjectManager.GetAllHitboxes();

        public static bool IsHitboxColliding(int index)
            => ObjectManager.IsHitboxColliding(index);

        public static int GetPointLightsCount()
            => ObjectManager.GetPointLightsCount();

        public static int GetSpotLightsCount()
            => ObjectManager.GetSpotLightsCount();

        public static int GetFirstPointLightIndex()
            => ObjectManager.GetFirstPointLightIndex();

        public static int CreateSound(string path)
            => SoundManager.Create(path);

        public static void DeleteSound(int index)
            => SoundManager.Delete(index);

        public static void PlaySound(int index)
            => SoundManager.Play(index);

        public static bool IsSoundPlaying(int index)
            => SoundManager.IsPlaying(index);

        public static void PauseSound(int index)
            => SoundManager.Pause(index);

        public static bool IsSoundPaused(int index)
            => SoundManager.IsPaused(index);

        public static void RewindSound(int index)
            => SoundManager.Rewind(index);

        public static void StopSound(int index)
            => SoundManager.Stop(index);

        public static bool IsSoundStopped(int index)
            => SoundManager.IsStopped(index);

        public static float GetSoundVolume(int index)
            => SoundManager.GetVolume(index);

        public static void SetSoundVolume(int index, float volume)
            => SoundManager.SetVolume(index, volume);

        public static float GetGeneralVolume()
            => SoundManager.GeneralVolume;

        public static void SetGeneralVolume(float volume)
            => SoundManager.GeneralVolume = volume;

        public static int CreateCamera(Camera camera)
            => CameraManager.Create(camera);

        public static void DeleteCamera(int index) 
            => CameraManager.Delete(index);

        public static Vector3 GetCameraPosition(int index)
            => CameraManager.GetPosition(index);

        public static Matrix4 GetCameraViewMatrix(int index)
            => CameraManager.GetViewMatrix(index);

        public static Matrix4 GetCameraProjectionMatrix(int index)
            => CameraManager.GetProjectionMatrix(index);
        
        public static Vector3 GetCameraFront(int index)
            => CameraManager.GetFront(index);

        public static Vector3 GetCameraRight(int index)
            => CameraManager.GetRight(index);

        public static Vector3 GetCameraUp(int index)
            => CameraManager.GetUp(index);

        public static void SetCameraAspectRatio(int index, float aspectRatio)
            => CameraManager.SetAspectRatio(index, aspectRatio);

        public static int GetShaderAttribLocation(ShaderType type, string attribName)
            => ShaderManager.GetAttribLocation(type, attribName);

        public static void SetShaderInt(ShaderType type, string name, int data)
            => ShaderManager.SetInt(type, name, data);

        public static void SetShaderFloat(ShaderType type, string name, float data)
            => ShaderManager.SetFloat(type, name, data);

        public static void SetShaderMatrix4(ShaderType type, string name, Matrix4 data)
            => ShaderManager.SetMatrix4(type, name, data);

        public static void SetShaderVector2(ShaderType type, string name, Vector2 data)
            => ShaderManager.SetVector2(type, name, data);

        public static void SetShaderVector3(ShaderType type, string name, Vector3 data)
            => ShaderManager.SetVector3(type, name, data);

        public static void SetShaderVector4(ShaderType type, string name, Vector4 data)
            => ShaderManager.SetVector4(type, name, data);

        public static void Print(string text)
            => Printer.Print(text);

        internal static void AddPerformanceWindowFrameTime(double frameTime)
            => PerformanceWindow.AddFrameTime(frameTime);

        internal static void AddPerformanceWindowUpdateTime(double updateTime)
            => PerformanceWindow.AddUpdateTime(updateTime);

        public static double GetPerformanceWindowFrameTime()
            => PerformanceWindow.FrameTime;

        public static double GetPerformanceWindowUpdateTime()
            => PerformanceWindow.UpdateTime;

        public static double GetPerformanceWindowFrameRate()
            => PerformanceWindow.FrameRate;

        public static double GetPerformanceWindowUpdateRate()
            => PerformanceWindow.UpdateRate;

        public static void Update()
        {
            Time.Update();
            PerformanceWindow.Update();
            ObjectManager.Update();
            CameraManager.Update();
            Printer.Update();
        }

        public static void RenderFrame()
        {
            UpdateShaders();
            ObjectManager.Draw();
        }

        private static void UpdateShaders()
        {
            ObjectManager.UpdateShaders();
            ShaderManager.Update();
        }

        public static void Exit()
        {
            SoundManager.Dispose();
        }
    }
}
