using Kotono.Audio;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;

namespace Kotono
{
    public sealed class KT
    {
        private KT() { }

        private static ObjectManager ObjectManager { get; } = new();

        private static AudioManager AudioManager { get; } = new();

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

        public static void Update()
        {
            ObjectManager.Update();
        }

        public static void UpdateShaders()
        {
            ObjectManager.UpdateShaders();

            ShaderManager.Lighting.SetInt("numLights", GetPointLightsCount());

            ShaderManager.Lighting.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.Lighting.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            ShaderManager.PointLight.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.PointLight.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            ShaderManager.Hitbox.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.Hitbox.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            ShaderManager.Lighting.SetVector3("viewPos", CameraManager.Main.Position);

            ShaderManager.Lighting.SetInt("material.diffuse", 0);
            ShaderManager.Lighting.SetInt("material.specular", 1);
            ShaderManager.Lighting.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            ShaderManager.Lighting.SetFloat("material.shininess", 32.0f);

            ShaderManager.Lighting.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            ShaderManager.Lighting.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            ShaderManager.Lighting.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            ShaderManager.Lighting.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));
            
            ShaderManager.Lighting.SetVector3("spotLight.position", CameraManager.Main.Position);
            ShaderManager.Lighting.SetVector3("spotLight.direction", CameraManager.Main.Front);
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
