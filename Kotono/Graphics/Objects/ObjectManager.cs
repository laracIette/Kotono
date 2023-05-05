using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal class ObjectManager
    {
        private ImageManager ImageManager { get; } = new();

        private MeshManager MeshManager { get; } = new();

        private HitboxManager HitboxManager { get; } = new();

        private PointLightManager PointLightManager { get; } = new();

        private SpotLightManager SpotLightManager { get; } = new();

        internal ObjectManager() { }

        internal int CreateImage(Image image)
            => ImageManager.Create(image);

        internal int CreateMesh(IMesh mesh)
            => MeshManager.Create(mesh);
        
        internal int CreateHitbox(IHitbox hitbox)
            => HitboxManager.Create(hitbox);

        internal int CreatePointLight(PointLight pointLight)
            => PointLightManager.Create(pointLight);

        internal int CreateSpotLight(SpotLight spotLight)
            => SpotLightManager.Create(spotLight);

        internal Vector3 GetMeshPosition(int index)
            => MeshManager.GetPosition(index);

        internal void SetMeshColor(int index, Vector3 color)
            => MeshManager.SetColor(index, color);

        internal void DeleteImage(int index)
            => ImageManager.Delete(index);

        internal void DeleteMesh(int index)
            => MeshManager.Delete(index);

        internal void DeleteHitbox(int index)
            => HitboxManager.Delete(index);

        internal void DeletePointLight(int index)
            => PointLightManager.Delete(index);

        internal void DeleteSpotLight(int index)
            => SpotLightManager.Delete(index);

        internal Rect GetImageRect(int index)
            => ImageManager.GetRect(index);

        internal void SetImageX(int index, float x)
            => ImageManager.SetX(index, x);

        internal void SetImageY(int index, float y)
            => ImageManager.SetY(index, y);

        internal void SetImageW(int index, float w)
            => ImageManager.SetW(index, w);

        internal void SetImageH(int index, float h)
            => ImageManager.SetH(index, h);

        internal void TransformImage(int index, Rect transformation, double time)
            => ImageManager.Transform(index, transformation, time);
        
        internal void TransformImageTo(int index, Rect dest, double time)
            => ImageManager.TransformTo(index, dest, time);

        internal void ShowImage(int index)
            => ImageManager.Show(index);

        internal void HideImage(int index)
            => ImageManager.Hide(index);

        internal void SetHitBoxPosition(int index, Vector3 position)
            => HitboxManager.SetPosition(index, position);

        internal void SetHitBoxAngle(int index, Vector3 angle)
            => HitboxManager.SetAngle(index, angle);

        internal void SetHitBoxScale(int index, Vector3 scale)
            => HitboxManager.SetScale(index, scale);

        internal void SetHitBoxColor(int index, Vector3 color)
            => HitboxManager.SetColor(index, color);
        

        internal void AddHitboxCollision(int index, int hitboxIndex)
            => HitboxManager.AddCollision(index, hitboxIndex);

        internal void AddHitboxCollision(int index, int[] hitboxIndexes)
            => HitboxManager.AddCollision(index, hitboxIndexes);

        internal int[] GetAllHitboxes()
            => HitboxManager.GetAll();

        internal bool IsHitboxColliding(int index) 
            => HitboxManager.IsColliding(index);

        internal int GetPointLightsCount()
            => PointLightManager.GetCount();
        
        internal int GetSpotLightsCount()
            => SpotLightManager.GetCount();

        internal int GetFirstPointLightIndex()
            => PointLightManager.GetFirstIndex();

        internal void Update()
        {
            ImageManager.Update();
            MeshManager.Update();
            HitboxManager.Update();
            PointLightManager.Update();
            SpotLightManager.Update();
        }

        internal void UpdateShaders()
        {
            PointLightManager.UpdateShaders();
            SpotLightManager.UpdateShaders();
        }

        internal void Draw()
        {
            MeshManager.Draw();
            HitboxManager.Draw();
            PointLightManager.Draw();
            SpotLightManager.Draw();
            ImageManager.Draw();
        }
    }
}