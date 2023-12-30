using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Objects.Shapes;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    internal static class ObjectManager
    {
        private static readonly FrontMeshManager _frontMeshManager = new();

        private static readonly MeshManager _meshManager = new();

        private static readonly HitboxManager _hitboxManager = new();

        private static readonly PointLightManager _pointLightManager = new();

        private static readonly SpotLightManager _spotLightManager = new();

        private static readonly ShapeManager _shapeManager = new();

        private static readonly Object2DManager _object2DManager = new();

        internal static void Create(IDrawable drawable)
        {
            switch (drawable)
            {
                case FrontMesh frontMesh:
                    _frontMeshManager.Create(frontMesh);
                    break;

                case Mesh mesh:
                    _meshManager.Create(mesh);
                    break;

                case IHitbox hitbox:
                    _hitboxManager.Create(hitbox);
                    break;

                case PointLight pointLight:
                    _pointLightManager.Create(pointLight);
                    break;

                case SpotLight spotLight:
                    _spotLightManager.Create(spotLight);
                    break;

                case IShape shape:
                    _shapeManager.Create(shape);
                    break;

                case IObject2D object2D:
                    _object2DManager.Create(object2D);
                    break;

                default:
                    break;
            }
        }

        internal static void Delete(IDrawable drawable)
        {
            switch (drawable)
            {
                case FrontMesh frontMesh:
                    _frontMeshManager.Delete(frontMesh);
                    break;

                case Mesh mesh:
                    _meshManager.Delete(mesh);
                    break;

                case IHitbox hitbox:
                    _hitboxManager.Delete(hitbox);
                    break;

                case PointLight pointLight:
                    _pointLightManager.Delete(pointLight);
                    break;

                case SpotLight spotLight:
                    _spotLightManager.Delete(spotLight);
                    break;

                case IShape shape:
                    _shapeManager.Delete(shape);
                    break;

                case IObject2D object2D:
                    _object2DManager.Delete(object2D);
                    break;

                default:
                    break;
            }
        }

        internal static PointLight GetFirstPointLight()
        {
            return _pointLightManager.Drawables.First();
        }

        internal static List<PointLight> GetPointLights()
        {
            return _pointLightManager.Drawables;
        }

        internal static List<SpotLight> GetSpotLights()
        {
            return _spotLightManager.Drawables;
        }

        internal static void Update()
        {
            _frontMeshManager.Update();
            _meshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
            _spotLightManager.Update();
            _shapeManager.Update();
            _object2DManager.Update();
        }

        internal static void Draw()
        {
            ComponentManager.Window.Viewport.Use();

            _hitboxManager.Draw();
            _pointLightManager.Draw();
            _spotLightManager.Draw();
            _shapeManager.Draw();
            _meshManager.Draw();
            _frontMeshManager.Draw();
            _object2DManager.Draw();
        }

        internal static void Save()
        {
            _frontMeshManager.Save();
            _meshManager.Save();
            _hitboxManager.Save();
            _pointLightManager.Save();
            _spotLightManager.Save();
            _shapeManager.Save();
            _object2DManager.Save();
        }
    }
}