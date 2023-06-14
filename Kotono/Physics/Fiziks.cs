using Kotono.Graphics.Objects.Meshes;

namespace Kotono.Physics
{
    public enum CollisionState
    {
        None,
        BlockAll,
        BlockSelection
    }

    internal sealed class Fiziks
    {

        private Fiziks() { }

        public static void Init()
        {
        }

        public static void Update() 
        { 
        }

        public static void SetCollisionState(IMesh mesh, CollisionState state)
        {
            mesh.Collision = state;
        }
    }
}
