using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public static class ObjectManager
    {
        private static readonly MeshManager _meshManager = new();

        private static readonly FrontMeshManager _frontMeshManager = new();

        private static readonly HitboxManager _hitboxManager = new();

        private static readonly PointLightManager _pointLightManager = new();

        private static readonly SpotLightManager _spotLightManager = new();

        private static readonly TriangleManager _triangleManager = new();

        private static readonly Object2DManager _object2DManager = new();

        //private static readonly Viewport _viewport = new(0, 0, 1280, 720);

        #region Image

        public static Image CreateImage(Image image)
        {
            _object2DManager.Create(image);
            return image;
        }

        public static void DeleteImage(Image image)
        {
            _object2DManager.Delete(image);
        }

        #endregion Image

        #region Mesh

        public static Mesh CreateMesh(Mesh mesh)
        {
            _meshManager.Create(mesh);
            return mesh;
        }

        public static void DeleteMesh(Mesh mesh)
        {
            _meshManager.Delete(mesh);
        }

        #endregion Mesh

        #region FrontMesh

        public static FrontMesh CreateFrontMesh(FrontMesh frontMesh)
        {
            _frontMeshManager.Create(frontMesh);
            return frontMesh;
        }

        public static void DeleteFrontMesh(FrontMesh frontMesh)
        {
            _frontMeshManager.Delete(frontMesh);
        }

        #endregion FrontMesh

        #region Hitbox

        public static IHitbox CreateHitbox(IHitbox hitbox)
        {
            _hitboxManager.Create(hitbox);
            return hitbox;
        }

        public static void DeleteHitbox(IHitbox hitbox)
        {
            _hitboxManager.Delete(hitbox);
        }

        public static List<IHitbox> GetAllHitboxes()
        {
            return _hitboxManager.GetAll();
        }

        #endregion Hitbox

        #region PointLight

        public static PointLight CreatePointLight(PointLight pointLight)
        {
            _pointLightManager.Create(pointLight);
            return pointLight;
        }

        public static void DeletePointLight(PointLight pointLight)
        {
            _pointLightManager.Delete(pointLight);
        }

        public static PointLight GetFirstPointLight()
        {
            return _pointLightManager.GetFirst();
        }

        #endregion PointLight

        #region SpotLight

        public static SpotLight CreateSpotLight(SpotLight spotLight)
        {
            _spotLightManager.Create(spotLight);
            return spotLight;
        }

        public static void DeleteSpotLight(SpotLight spotLight)
        {
            _spotLightManager.Delete(spotLight);
        }

        public static int GetSpotLightsCount()
        {
            return _spotLightManager.GetCount();
        }

        #endregion SpotLight

        #region Triangle

        public static Triangle CreateTriangle(Triangle triangle)
        {
            _triangleManager.Create(triangle);
            return triangle;
        }

        public static void DeleteTriangle(Triangle triangle)
        {
            _triangleManager.Delete(triangle);
        }

        #endregion Triangle

        #region RoundedBox

        public static RoundedBox CreateRoundedBox(RoundedBox box)
        {
            _object2DManager.Create(box);
            return box;
        }

        public static void DeleteRoundedBox(RoundedBox box)
        {
            _object2DManager.Delete(box);
        }

        #endregion RoundedBox

        #region RoundedBorder

        public static RoundedBorder CreateRoundedBorder(RoundedBorder border)
        {
            _object2DManager.Create(border);
            return border;
        }

        public static void DeleteRoundedBorder(RoundedBorder border)
        {
            _object2DManager.Delete(border);
        }

        #endregion RoundedBorder

        public static void Init()
        {
            _meshManager.Init();
            _frontMeshManager.Init();
            _hitboxManager.Init();
            _pointLightManager.Init();
            _spotLightManager.Init();
            _triangleManager.Init();
            _object2DManager.Init();
            //_viewport.Init();
        }

        public static void Update()
        {
            _meshManager.Update();
            _frontMeshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
            _spotLightManager.Update();
            _triangleManager.Update();
            _object2DManager.Update();
        }

        public static void UpdateShaders()
        {
            _meshManager.UpdateShaders();
            _frontMeshManager.UpdateShaders();
            _hitboxManager.UpdateShaders();
            _pointLightManager.UpdateShaders();
            _spotLightManager.UpdateShaders();
            _triangleManager.UpdateShaders();
            _object2DManager.UpdateShaders();
        }

        public static void Draw()
        {
            KT.ActiveViewport.Use();

            _hitboxManager.Draw();
            _pointLightManager.Draw();
            _spotLightManager.Draw();
            _triangleManager.Draw();
            _meshManager.Draw();
            _frontMeshManager.Draw();
            _object2DManager.Draw();
        }

        public static void Save()
        {
            _meshManager.Save();
            _frontMeshManager.Save();
            _hitboxManager.Save();
            _pointLightManager.Save();
            _spotLightManager.Save();
            _triangleManager.Save();
            _object2DManager.Save();
        }
    }
}