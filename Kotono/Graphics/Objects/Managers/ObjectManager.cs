using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    internal static class ObjectManager
    {
        private static readonly FrontMeshManager _frontMeshManager = new();

        private static readonly Object2DManager _object2DManager = new();
        
        private static readonly Object3DManager _object3DManager = new();

        internal static PointLight[] PointLights => _object3DManager.Drawables.OfType<PointLight>().ToArray();

        internal static SpotLight[] SpotLights => _object3DManager.Drawables.OfType<SpotLight>().ToArray();

        internal static void Create(Drawable drawable)
        {
            switch (drawable)
            {
                case FrontMesh frontMesh:
                    _frontMeshManager.Create(frontMesh);
                    break;

                case PointLight pointLight:
                    if (PointLights.Length >= PointLight.MAX_COUNT)
                    {
                        KT.Log($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
                    }
                    else
                    {
                        _object3DManager.Create(pointLight);
                    }
                    break;

                case SpotLight spotLight:
                    if (SpotLights.Length >= SpotLight.MAX_COUNT)
                    {
                        KT.Log($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
                    }
                    else
                    {
                        _object3DManager.Create(spotLight);
                    }
                    break;

                case Object2D object2D:
                    _object2DManager.Create(object2D);
                    break;

                case Object3D object3D:
                    _object3DManager.Create(object3D);
                    break;

                default:
                    break;
            }
        }

        internal static void Delete(Drawable drawable)
        {
            switch (drawable)
            {
                case FrontMesh frontMesh:
                    _frontMeshManager.Delete(frontMesh);
                    break;

                case Object2D object2D:
                    _object2DManager.Delete(object2D);
                    break;

                case Object3D object3D:
                    _object3DManager.Delete(object3D);
                    break;

                default:
                    break;
            }
        }

        internal static void UpdateObject2DLayer(Object2D object2D)
        {
            _object2DManager.UpdateLayer(object2D);
        }

        internal static void Update()
        {
            _frontMeshManager.Update();
            _object3DManager.Update();
            _object2DManager.Update();
        }

        internal static void Draw3D()
        {
            _object3DManager.Draw();
        }

        internal static void DrawFront()
        {
            _frontMeshManager.Draw();
        }

        internal static void Draw2D() 
        {
            _object2DManager.Draw();
        }

        internal static void Save()
        {
            _frontMeshManager.Save();
            _object3DManager.Save();
            _object2DManager.Save();
        }

        internal static void Dispose()
        {
            _frontMeshManager.Dispose();
            _object3DManager.Dispose();
            _object2DManager.Dispose();
        }
    }
}