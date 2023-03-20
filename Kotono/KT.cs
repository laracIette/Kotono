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

        public static void Update()
        {
            ObjectManager.Update();
        }

        public static void UpdateShaders()
        {
            ObjectManager.UpdateShaders();
        }

        public static void Draw()
        {
            ObjectManager.Draw();
        }
    }
}
