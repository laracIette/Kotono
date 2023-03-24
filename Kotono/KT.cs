using Assimp;
using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Mathematics;
using Camera = Kotono.Graphics.Camera;

namespace Kotono
{
    public sealed class KT
    {
        private KT() { }

        private static ObjectManager ObjectManager { get; } = new();

        private static AudioManager AudioManager { get; } = new();

        private static CameraManager CameraManager { get; } = new();

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

        public static void RemoveMesh(int index)
        {
            ObjectManager.RemoveMesh(index);
        }

        public static void RemoveHitbox(int index)
        {
            ObjectManager.RemoveHitbox(index);
        }

        public static void RemovePointLight(int index)
            => ObjectManager.RemovePointLight(index);

        public static void SetHitBoxPosition(int index, Vector3 position)
        {
            ObjectManager.SetHitBoxPosition(index, position);
        }

        public static void SetHitBoxAngle(int index, Vector3 angle)
        {
            ObjectManager.SetHitBoxAngle(index, angle);
        }

        public static void SetHitBoxScale(int index, Vector3 scale)
        {
            ObjectManager.SetHitBoxScale(index, scale);
        }

        public static void SetHitBoxColor(int index, Vector3 color)
        {
            ObjectManager.SetHitBoxColor(index, color);
        }

        public static int GetPointLightsCount()
            => ObjectManager.GetPointLightsCount();

        public static int GetFirstPointLightIndex()
            => ObjectManager.GetFirstPointLightIndex();

        public static int CreateSound(string path)
            => AudioManager.Create(path);

        public static void PlaySound(int source)
        {
            AudioManager.Play(source);
        }

        public static void PauseSound(int source)
        {
            AudioManager.Pause(source);
        }

        public static void RewindSound(int source)
        {
            AudioManager.Rewind(source);
        }

        public static void StopSound(int source)
        {
            AudioManager.Stop(source);
        }

        public static void DeleteSound(int source)
        {
            AudioManager.Delete(source);
        }

        public static int CreateCamera(Camera camera)
            => CameraManager.Create(camera);

        public static Vector3 GetCameraPosition(int index)
            => CameraManager.GetPosition(index);

        public static Matrix4 GetCameraViewMatrix(int index)
            => CameraManager.GetViewMatrix(index);

        public static Matrix4 GetCameraProjectionMatrix(int index)
            => CameraManager.GetProjectionMatrix(index);

        public static Vector3 GetCameraFront(int index)
            => CameraManager.GetFront(index);

        public static void SetCameraAspectRatio(int index, float aspectRatio)
        {
            CameraManager.SetAspectRatio(index, aspectRatio);
        }

        public static void Update()
        {
            Time.Update();
            ObjectManager.Update();
            CameraManager.Update();
        }

        public static void UpdateShaders()
        {
            ObjectManager.UpdateShaders();

            ShaderManager.Lighting.SetInt("numLights", GetPointLightsCount());

            ShaderManager.Lighting.SetMatrix4("view", GetCameraViewMatrix(0));
            ShaderManager.Lighting.SetMatrix4("projection", GetCameraProjectionMatrix(0));

            ShaderManager.PointLight.SetMatrix4("view", GetCameraViewMatrix(0));
            ShaderManager.PointLight.SetMatrix4("projection", GetCameraProjectionMatrix(0));

            ShaderManager.Hitbox.SetMatrix4("view", GetCameraViewMatrix(0));
            ShaderManager.Hitbox.SetMatrix4("projection", GetCameraProjectionMatrix(0));

            ShaderManager.Lighting.SetVector3("viewPos", GetCameraPosition(0));

            ShaderManager.Lighting.SetInt("material.diffuse", 0);
            ShaderManager.Lighting.SetInt("material.specular", 1);
            ShaderManager.Lighting.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            ShaderManager.Lighting.SetFloat("material.shininess", 32.0f);

            ShaderManager.Lighting.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            ShaderManager.Lighting.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            ShaderManager.Lighting.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            ShaderManager.Lighting.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));
            
            ShaderManager.Lighting.SetVector3("spotLight.position", GetCameraPosition(0));
            ShaderManager.Lighting.SetVector3("spotLight.direction", GetCameraFront(0));
            ShaderManager.Lighting.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            ShaderManager.Lighting.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.Lighting.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            ShaderManager.Lighting.SetFloat("spotLight.constant", 1.0f);
            ShaderManager.Lighting.SetFloat("spotLight.linear", 0.09f);
            ShaderManager.Lighting.SetFloat("spotLight.quadratic", 0.032f);
        }

        public static void RenderFrame()
        {
            UpdateShaders();
            ObjectManager.Draw();
        }

        public static void Exit()
        {
            AudioManager.Dispose();
        }
    }
}
