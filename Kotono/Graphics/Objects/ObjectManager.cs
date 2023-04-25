using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Rects;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class ObjectManager
    {
        private ImageManager ImageManager { get; } = new();

        private MeshManager MeshManager { get; } = new();

        private HitboxManager HitboxManager { get; } = new();

        private PointLightManager PointLightManager { get; } = new();

        private SpotLightManager SpotLightManager { get; } = new();

        public ObjectManager() { }

        public int CreateImage(Image image)
            => ImageManager.Create(image);

        public int CreateMesh(IMesh mesh)
            => MeshManager.Create(mesh);
        
        public int CreateHitbox(IHitbox hitbox)
            => HitboxManager.Create(hitbox);

        public int CreatePointLight(PointLight pointLight)
            => PointLightManager.Create(pointLight);

        public int CreateSpotLight(SpotLight spotLight)
            => SpotLightManager.Create(spotLight);

        public Vector3 GetMeshPosition(int index)
            => MeshManager.GetPosition(index);

        public void SetMeshColor(int index, Vector3 color)
            => MeshManager.SetColor(index, color);

        public void DeleteImage(int index)
            => ImageManager.Delete(index);

        public void DeleteMesh(int index)
            => MeshManager.Delete(index);

        public void DeleteHitbox(int index)
            => HitboxManager.Delete(index);

        public void DeletePointLight(int index)
            => PointLightManager.Delete(index);

        public void DeleteSpotLight(int index)
            => SpotLightManager.Delete(index);

        public IRect GetImageRect(int index)
            => ImageManager.GetRect(index);

        public void SetHitBoxPosition(int index, Vector3 position)
            => HitboxManager.SetPosition(index, position);

        public void SetHitBoxAngle(int index, Vector3 angle)
            => HitboxManager.SetAngle(index, angle);

        public void SetHitBoxScale(int index, Vector3 scale)
            => HitboxManager.SetScale(index, scale);

        public void SetHitBoxColor(int index, Vector3 color)
            => HitboxManager.SetColor(index, color);
        

        public void AddHitboxCollision(int index, int hitboxIndex)
            => HitboxManager.AddCollision(index, hitboxIndex);

        public void AddHitboxCollision(int index, int[] hitboxIndexes)
            => HitboxManager.AddCollision(index, hitboxIndexes);

        public int[] GetAllHitboxes()
            => HitboxManager.GetAll();

        public bool IsHitboxColliding(int index) 
            => HitboxManager.IsColliding(index);

        public int GetPointLightsCount()
            => PointLightManager.GetCount();
        
        public int GetSpotLightsCount()
            => SpotLightManager.GetCount();

        public int GetFirstPointLightIndex()
            => PointLightManager.GetFirstIndex();

        public void Update()
        {
            ImageManager.Update();
            MeshManager.Update();
            HitboxManager.Update();
            PointLightManager.Update();
            SpotLightManager.Update();
        }

        public void UpdateShaders()
        {
            PointLightManager.UpdateShaders();
            SpotLightManager.UpdateShaders();
        }

        public void Draw()
        {
            ImageManager.Draw();
            MeshManager.Draw();
            HitboxManager.Draw();
            PointLightManager.Draw();
            SpotLightManager.Draw();
        }
    }
}