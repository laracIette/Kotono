using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public class ObjectManager
    {
        private readonly ImageManager _imageManager = new();

        private readonly MeshManager _meshManager = new();

        private readonly FrontMeshManager _frontMeshManager = new();

        private readonly HitboxManager _hitboxManager = new();

        private readonly PointLightManager _pointLightManager = new();

        private readonly SpotLightManager _spotLightManager = new();

        private readonly TriangleManager _triangleManager = new();

        private readonly RoundedBoxManager _roundedBoxManager = new();
        
        private readonly RoundedBorderManager _roundedBorderManager = new();

        //private readonly Viewport _viewport = new(0, 0, 1280, 720);


        public ObjectManager() { }

        public void Init()
        {
            _imageManager.Init();
            _meshManager.Init();
            _frontMeshManager.Init();
            _hitboxManager.Init();
            _pointLightManager.Init();
            _spotLightManager.Init();
            _triangleManager.Init();
            _roundedBoxManager.Init();
            _roundedBorderManager.Init();
            //_viewport.Init();
        }

        #region Image

        public void CreateImage(Image image)
        {
            _imageManager.Create(image);
        }

        public void DeleteImage(Image image)
        {
            _imageManager.Delete(image);
        }

        #endregion Image

        #region Mesh

        public void CreateMesh(Mesh mesh)
        {
            _meshManager.Create(mesh);
        }

        public void DeleteMesh(Mesh mesh)
        {
            _meshManager.Delete(mesh);
        }

        #endregion Mesh

        #region FrontMesh

        public void CreateFrontMesh(FrontMesh frontMesh)
        {
            _frontMeshManager.Create(frontMesh);
        }

        public void DeleteFrontMesh(FrontMesh frontMesh)
        {
            _frontMeshManager.Delete(frontMesh);
        }

        #endregion FrontMesh

        #region Hitbox

        public void CreateHitbox(IHitbox hitbox)
        {
            _hitboxManager.Create(hitbox);
        }

        public void DeleteHitbox(IHitbox hitbox)
        {
            _hitboxManager.Delete(hitbox);
        }

        public List<IHitbox> GetAllHitboxes()
        {
            return _hitboxManager.GetAll();
        }

        #endregion Hitbox

        #region PointLight

        public void CreatePointLight(PointLight pointLight)
        {
            _pointLightManager.Create(pointLight);
        }

        public void DeletePointLight(PointLight pointLight)
        {
            _pointLightManager.Delete(pointLight);
        }

        public PointLight GetFirstPointLight()
        {
            return _pointLightManager.GetFirst();
        }

        #endregion PointLight

        #region SpotLight

        public void CreateSpotLight(SpotLight spotLight)
        {
            _spotLightManager.Create(spotLight);
        }

        public void DeleteSpotLight(SpotLight spotLight)
        {
            _spotLightManager.Delete(spotLight);
        }

        public int GetSpotLightsCount()
        {
            return _spotLightManager.GetCount();
        }

        #endregion SpotLight

        #region Triangle

        public void CreateTriangle(Triangle triangle)
        {
            _triangleManager.Create(triangle);
        }

        public void DeleteTriangle(Triangle triangle)
        {
            _triangleManager.Delete(triangle);
        }

        #endregion Triangle

        #region RoundedBox

        public void CreateRoundedBox(RoundedBox box)
        {
            _roundedBoxManager.Create(box);
        }

        public void DeleteRoundedBox(RoundedBox box)
        {
            _roundedBoxManager.Delete(box);
        }

        #endregion RoundedBox

        #region RoundedBorder

        public void CreateRoundedBorder(RoundedBorder border)
        {
            _roundedBorderManager.Create(border);
        }

        public void DeleteRoundedBorder(RoundedBorder border)
        {
            _roundedBorderManager.Delete(border);
        }

        #endregion RoundedBorder

        public void Update()
        {
            _imageManager.Update();
            _meshManager.Update();
            _frontMeshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
            _spotLightManager.Update();
            _triangleManager.Update();
            _roundedBoxManager.Update();
            _roundedBorderManager.Update();
        }

        public void UpdateShaders()
        {
            _imageManager.UpdateShaders();
            _meshManager.UpdateShaders();
            _frontMeshManager.UpdateShaders();
            _hitboxManager.UpdateShaders();
            _pointLightManager.UpdateShaders();
            _spotLightManager.UpdateShaders();
            _triangleManager.UpdateShaders();
            _roundedBoxManager.UpdateShaders();
            _roundedBorderManager.UpdateShaders();
        }

        public void Draw()
        {
            KT.ActiveViewport.Use();

            _hitboxManager.Draw();
            _pointLightManager.Draw();
            _spotLightManager.Draw();
            _triangleManager.Draw();
            _meshManager.Draw();
            _frontMeshManager.Draw();
            _roundedBoxManager.Draw();
            _roundedBorderManager.Draw();
            _imageManager.Draw();
        }

        public void Save()
        {
            _imageManager.Save();
            _meshManager.Save();
            _frontMeshManager.Save();
            _hitboxManager.Save();
            _pointLightManager.Save();
            _spotLightManager.Save();
            _triangleManager.Save();
            _roundedBoxManager.Save();
            _roundedBorderManager.Save();
        }
    }
}