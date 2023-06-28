using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class ObjectManager
    {
        private readonly ImageManager _imageManager = new();

        private readonly MeshManager _meshManager = new();

        private readonly HitboxManager _hitboxManager = new();

        private readonly PointLightManager _pointLightManager = new();

        private readonly SpotLightManager _spotLightManager = new();

        private readonly TriangleManager _triangleManager = new();

        private readonly Viewport _viewport = new(0, 0, 1280, 720);


        internal ObjectManager() { }

        internal void Init()
        {
            _imageManager.Init();
            _meshManager.Init();
            _hitboxManager.Init();
            _pointLightManager.Init();
            _spotLightManager.Init();
            _triangleManager.Init();
            _viewport.Init();
        }

        #region Mesh

        internal void CreateMesh(Mesh mesh)
        {
            _meshManager.Create(mesh);
        }

        internal void DeleteMesh(Mesh mesh)
        {
            _meshManager.Delete(mesh);
        }

        #endregion Mesh

        #region Hitbox

        internal void CreateHitbox(IHitbox hitbox)
        {
            _hitboxManager.Create(hitbox);
        }

        internal void DeleteHitbox(IHitbox hitbox)
        {
            _hitboxManager.Delete(hitbox);
        }

        internal List<IHitbox> GetAllHitboxes()
        {
            return _hitboxManager.GetAll();   
        }

        #endregion Hitbox

        #region PointLight

        internal void CreatePointLight(PointLight pointLight)
        {
            _pointLightManager.Create(pointLight);
        }

        internal void DeletePointLight(PointLight pointLight)
        {
            _pointLightManager.Delete(pointLight);
        }

        internal int GetPointLightsCount()
        {
            return _pointLightManager.GetCount();
        }

        internal PointLight GetFirstPointLight()
        {
            return _pointLightManager.GetFirst();
        }

        #endregion PointLight

        #region SpotLight

        internal void CreateSpotLight(SpotLight spotLight)
        {
            _spotLightManager.Create(spotLight);
        }

        internal void DeleteSpotLight(SpotLight spotLight)
        {
            _spotLightManager.Delete(spotLight);
        }

        internal int GetSpotLightsCount()
        {
            return _spotLightManager.GetCount();
        }

        #endregion SpotLight

        #region Image

        internal void CreateImage(Image image)
        {
            _imageManager.Create(image);
        }

        internal void DeleteImage(Image image)
        {
            _imageManager.Delete(image);
        }

        #endregion Image

        #region Triangle

        internal void CreateTriangle(Triangle triangle)
        {
            _triangleManager.Create(triangle);
        }

        internal void DeleteTriangle(Triangle triangle)
        {
            _triangleManager.Delete(triangle);
        }

        #endregion Triangle

        internal void Update()
        {
            _meshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
            _spotLightManager.Update();
            _imageManager.Update();
            _triangleManager.Update();

            if (Input.KeyboardState!.IsKeyDown(Keys.Up))
            {
                _viewport.Dest.Y += 100 * Time.DeltaS;
            }
            if (Input.KeyboardState!.IsKeyDown(Keys.Down))
            {
                _viewport.Dest.Y -= 100 * Time.DeltaS;
            }
            if (Input.KeyboardState!.IsKeyDown(Keys.Left))
            {
                _viewport.Dest.X -= 100 * Time.DeltaS;
            }
            if (Input.KeyboardState!.IsKeyDown(Keys.Right))
            {
                _viewport.Dest.X += 100 * Time.DeltaS;
            }

            if (Input.KeyboardState!.IsKeyDown(Keys.Minus))
            {
                _viewport.Dest.W -= 16 * Time.DeltaS * 5;
                _viewport.Dest.H -= 9 * Time.DeltaS * 5;
            }
            if (Input.KeyboardState!.IsKeyDown(Keys.Equal))
            {
                _viewport.Dest.W += 16 * Time.DeltaS * 5;
                _viewport.Dest.H += 9 * Time.DeltaS * 5;
            }
        }

        internal void UpdateShaders()
        {
            _pointLightManager.UpdateShaders();
            _spotLightManager.UpdateShaders();
            _imageManager.UpdateShaders();
        }

        internal void Draw()
        {
            _viewport.Use();

            _meshManager.Draw();
            _hitboxManager.Draw();
            _pointLightManager.Draw();
            _spotLightManager.Draw();
            _imageManager.Draw();
            _triangleManager.Draw();
        }
    }
}