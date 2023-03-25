﻿using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Mathematics;

namespace Kotono
{
    public sealed class KT
    {
        private KT() { }

        public const int MAX_POINT_LIGHTS = PointLightManager.MAX;

        private static ObjectManager ObjectManager { get; } = new();

        private static AudioManager AudioManager { get; } = new();

        private static CameraManager CameraManager { get; } = new();

        private static ShaderManager ShaderManager { get; } = new();

        public static int CreateMesh(IMesh mesh)
            => ObjectManager.CreateMesh(mesh);

        public static int CreateHitbox(IHitbox hitbox)
            => ObjectManager.CreateHitbox(hitbox);

        public static int CreatePointLight(PointLight pointLight)
            => ObjectManager.CreatePointLight(pointLight);

        public static IMesh GetMesh(int index)
            => ObjectManager.GetMesh(index);

        public static IHitbox GetHitbox(int index)
            => ObjectManager.GetHitbox(index);

        public static PointLight GetPointLight(int index)
            => ObjectManager.GetPointLight(index);

        public static void DeleteMesh(int index) 
            => ObjectManager.DeleteMesh(index);

        public static void DeleteHitbox(int index)
            => ObjectManager.DeleteHitbox(index);

        public static void DeletePointLight(int index)
            => ObjectManager.DeletePointLight(index);

        public static void SetHitBoxPosition(int index, Vector3 position)
            => ObjectManager.SetHitBoxPosition(index, position);

        public static void SetHitBoxAngle(int index, Vector3 angle)
            => ObjectManager.SetHitBoxAngle(index, angle);

        public static void SetHitBoxScale(int index, Vector3 scale)
            => ObjectManager.SetHitBoxScale(index, scale);

        public static void SetHitBoxColor(int index, Vector3 color)
            => ObjectManager.SetHitBoxColor(index, color);

        public static int GetPointLightsCount()
            => ObjectManager.GetPointLightsCount();

        public static int GetFirstPointLightIndex()
            => ObjectManager.GetFirstPointLightIndex();

        public static int CreateSound(string path)
            => AudioManager.Create(path);

        public static void PlaySound(int source)
            => AudioManager.Play(source);

        public static bool IsSoundPlaying(int source)
            => AudioManager.IsPlaying(source);

        public static void PauseSound(int source)
            => AudioManager.Pause(source);

        public static bool IsSoundPaused(int source)
            => AudioManager.IsPaused(source);

        public static void RewindSound(int source)
            => AudioManager.Rewind(source);

        public static void StopSound(int source)
            => AudioManager.Stop(source);

        public static bool IsSoundStopped(int source)
            => AudioManager.IsStopped(source);

        public static void DeleteSound(int source)
            => AudioManager.Delete(source);

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

        public static void SetCameraAspectRatio(int index, float aspectRatio)
            => CameraManager.SetAspectRatio(index, aspectRatio);

        /*
        public static int CreateShader(Shader shader)
            => ShaderManager.Create(shader);

        public static void DeleteShader(int index)
            => ShaderManager.Delete(index);
        */

        public static int GetShaderAttribLocation(ShaderType type, string attribName)
            => ShaderManager.GetAttribLocation(type, attribName);

        public static void SetShaderInt(ShaderType type, string name, int data)
            => ShaderManager.SetInt(type, name, data);

        public static void SetShaderFloat(ShaderType type, string name, float data)
            => ShaderManager.SetFloat(type, name, data);

        public static void SetShaderMatrix4(ShaderType type, string name, Matrix4 data)
            => ShaderManager.SetMatrix4(type, name, data);

        public static void SetShaderVector3(ShaderType type, string name, Vector3 data)
            => ShaderManager.SetVector3(type, name, data);

        public static void SetShaderVector4(ShaderType type, string name, Vector4 data)
            => ShaderManager.SetVector4(type, name, data);

        public static void Update()
        {
            Time.Update();
            ObjectManager.Update();
            CameraManager.Update();
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
            AudioManager.Dispose();
        }
    }
}
