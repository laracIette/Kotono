using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Objects.Shapes;

namespace Kotono.Graphics.Objects.Managers
{
    internal static class ObjectManager
    {
        private static readonly MeshManager _meshManager = new();

        private static readonly FrontMeshManager _frontMeshManager = new();

        private static readonly HitboxManager _hitboxManager = new();

        private static readonly PointLightManager _pointLightManager = new();

        private static readonly SpotLightManager _spotLightManager = new();

        private static readonly TriangleManager _triangleManager = new();

        private static readonly Object2DManager _object2DManager = new();

        //private static readonly Viewport _viewport = new(0, 0, 1280, 720);

        #region Object2D

        internal static void Create(IObject2D obj)
        {
            _object2DManager.Create(obj);
        }

        internal static void Delete(IObject2D obj)
        {
            _object2DManager.Delete(obj);
        }

        #endregion Object2D

        #region Mesh

        internal static void Create(Mesh mesh)
        {
            _meshManager.Create(mesh);
        }

        internal static void Delete(Mesh mesh)
        {
            _meshManager.Delete(mesh);
        }

        #endregion Mesh

        #region FrontMesh

        internal static void Create(FrontMesh frontMesh)
        {
            _frontMeshManager.Create(frontMesh);
        }

        internal static void Delete(FrontMesh frontMesh)
        {
            _frontMeshManager.Delete(frontMesh);
        }

        #endregion FrontMesh

        #region Hitbox

        internal static void Create(IHitbox hitbox)
        {
            _hitboxManager.Create(hitbox);
        }

        internal static void Delete(IHitbox hitbox)
        {
            _hitboxManager.Delete(hitbox);
        }

        #endregion Hitbox

        #region PointLight

        internal static void Create(PointLight pointLight)
        {
            _pointLightManager.Create(pointLight);
        }

        internal static void Delete(PointLight pointLight)
        {
            _pointLightManager.Delete(pointLight);
        }

        internal static PointLight GetFirstPointLight()
        {
            return _pointLightManager.GetFirst();
        }

        #endregion PointLight

        #region SpotLight

        internal static void Create(SpotLight spotLight)
        {
            _spotLightManager.Create(spotLight);
        }

        internal static void Delete(SpotLight spotLight)
        {
            _spotLightManager.Delete(spotLight);
        }

        #endregion SpotLight

        #region Triangle

        internal static void Create(Triangle triangle)
        {
            _triangleManager.Create(triangle);
        }

        internal static void Delete(Triangle triangle)
        {
            _triangleManager.Delete(triangle);
        }

        #endregion Triangle

        internal static void Init()
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

        internal static void Update()
        {
            _meshManager.Update();
            _frontMeshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
            _spotLightManager.Update();
            _triangleManager.Update();
            _object2DManager.Update();
        }

        internal static void UpdateShaders()
        {
            _meshManager.UpdateShaders();
            _frontMeshManager.UpdateShaders();
            _hitboxManager.UpdateShaders();
            _pointLightManager.UpdateShaders();
            _spotLightManager.UpdateShaders();
            _triangleManager.UpdateShaders();
            _object2DManager.UpdateShaders();
        }

        internal static void Draw()
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

        internal static void Save()
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