using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Graphics.Objects
{
    internal class ObjectManager
    {
        private readonly ImageManager _imageManager = new();

        private readonly MeshManager _meshManager = new();

        private readonly HitboxManager _hitboxManager = new();

        private readonly PointLightManager _pointLightManager = new();

        private readonly SpotLightManager _spotLightManager = new();

        private readonly Viewport _viewport = new(0, 0, 1280, 720);


        internal ObjectManager() { }

        internal int CreateImage(Image image)
            => _imageManager.Create(image);

        internal int CreateMesh(IMesh mesh)
            => _meshManager.Create(mesh);
        
        internal int CreateHitbox(IHitbox hitbox)
            => _hitboxManager.Create(hitbox);

        internal int CreatePointLight(PointLight pointLight)
            => _pointLightManager.Create(pointLight);

        internal int CreateSpotLight(SpotLight spotLight)
            => _spotLightManager.Create(spotLight);

        internal Vector3 GetMeshPosition(int index)
            => _meshManager.GetPosition(index);

        internal void SetMeshColor(int index, Vector3 color)
            => _meshManager.SetColor(index, color);

        internal void DeleteImage(int index)
            => _imageManager.Delete(index);

        internal void DeleteMesh(int index)
            => _meshManager.Delete(index);

        internal void DeleteHitbox(int index)
            => _hitboxManager.Delete(index);

        internal void DeletePointLight(int index)
            => _pointLightManager.Delete(index);

        internal void DeleteSpotLight(int index)
            => _spotLightManager.Delete(index);

        internal Rect GetImageRect(int index)
            => _imageManager.GetRect(index);

        internal void SetImageX(int index, float x)
            => _imageManager.SetX(index, x);

        internal void SetImageY(int index, float y)
            => _imageManager.SetY(index, y);

        internal void SetImageW(int index, float w)
            => _imageManager.SetW(index, w);

        internal void SetImageH(int index, float h)
            => _imageManager.SetH(index, h);

        internal void TransformImage(int index, Rect transformation, double time)
            => _imageManager.Transform(index, transformation, time);
        
        internal void TransformImageTo(int index, Rect dest, double time)
            => _imageManager.TransformTo(index, dest, time);

        internal void ShowImage(int index)
            => _imageManager.Show(index);

        internal void HideImage(int index)
            => _imageManager.Hide(index);

        internal void SetHitBoxPosition(int index, Vector3 position)
            => _hitboxManager.SetPosition(index, position);

        internal void SetHitBoxAngle(int index, Vector3 angle)
            => _hitboxManager.SetAngle(index, angle);

        internal void SetHitBoxScale(int index, Vector3 scale)
            => _hitboxManager.SetScale(index, scale);

        internal void SetHitBoxColor(int index, Vector3 color)
            => _hitboxManager.SetColor(index, color);
        

        internal void AddHitboxCollision(int index, int hitboxIndex)
            => _hitboxManager.AddCollision(index, hitboxIndex);

        internal void AddHitboxCollision(int index, int[] hitboxIndexes)
            => _hitboxManager.AddCollision(index, hitboxIndexes);

        internal int[] GetAllHitboxes()
            => _hitboxManager.GetAll();

        internal bool IsHitboxColliding(int index) 
            => _hitboxManager.IsColliding(index);

        internal int GetPointLightsCount()
            => _pointLightManager.GetCount();
        
        internal int GetSpotLightsCount()
            => _spotLightManager.GetCount();

        internal int GetFirstPointLightIndex()
            => _pointLightManager.GetFirstIndex();

        internal void Update()
        {
            _meshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
            _spotLightManager.Update();
            _imageManager.Update();

            if (InputManager.KeyboardState!.IsKeyDown(Keys.Up))
            {
                _viewport.Y += 100 * Time.DeltaS;
            }
            if (InputManager.KeyboardState!.IsKeyDown(Keys.Down))
            {
                _viewport.Y -= 100 * Time.DeltaS;
            }
            if (InputManager.KeyboardState!.IsKeyDown(Keys.Left))
            {
                _viewport.X -= 100 * Time.DeltaS;
            }
            if (InputManager.KeyboardState!.IsKeyDown(Keys.Right))
            {
                _viewport.X += 100 * Time.DeltaS;
            }

            if (InputManager.KeyboardState!.IsKeyDown(Keys.Minus))
            {
                _viewport.W -= 16 * Time.DeltaS * 5;
                _viewport.H -= 9 * Time.DeltaS * 5;
            }
            if (InputManager.KeyboardState!.IsKeyDown(Keys.Equal))
            {
                _viewport.W += 16 * Time.DeltaS * 5;
                _viewport.H += 9 * Time.DeltaS * 5;
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
        }
    }
}