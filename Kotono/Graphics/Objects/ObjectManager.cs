using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class ObjectManager
    {
        private MeshManager MeshManager { get; } = new();

        private HitboxManager HitboxManager { get; } = new();

        private PointLightManager PointLightManager { get; } = new();

        public ObjectManager() { }

        public int CreateMesh(IMesh mesh)
            => MeshManager.Create(mesh);
        
        public int CreateHitbox(IHitbox hitbox)
            => HitboxManager.Create(hitbox);

        public int CreatePointLight(PointLight pointLight)
            => PointLightManager.Create(pointLight);

        public IMesh GetMesh(int index)
            => MeshManager.Get(index);

        public IHitbox GetHitbox(int index)
            => HitboxManager.Get(index);

        public PointLight GetPointLight(int index)
            => PointLightManager.Get(index);

        public void DeleteMesh(int index)
        {
            MeshManager.Delete(index);
        }

        public void DeleteHitbox(int index)
        { 
            HitboxManager.Delete(index);
        }

        public void DeletePointLight(int index)
        {
            PointLightManager.Delete(index);
        }

        public void SetHitBoxPosition(int index, Vector3 position)
        {
            HitboxManager.SetPosition(index, position);
        }

        public void SetHitBoxAngle(int index, Vector3 angle)
        {
            HitboxManager.SetAngle(index, angle);
        }

        public void SetHitBoxScale(int index, Vector3 scale)
        {
            HitboxManager.SetScale(index, scale);
        }

        public void SetHitBoxColor(int index, Vector3 color)
        {
            HitboxManager.SetColor(index, color);
        }

        public int GetPointLightsCount()
            => PointLightManager.GetCount();

        public int GetFirstPointLightIndex()
            => PointLightManager.GetFirstIndex();

        public void Update()
        {
            MeshManager.Update();
            HitboxManager.Update();
            PointLightManager.Update();
        }

        public void UpdateShaders()
        {
            PointLightManager.UpdateShaders();
        }

        public void Draw()
        {
            MeshManager.Draw();
            HitboxManager.Draw();
            PointLightManager.Draw();
        }
    }
}