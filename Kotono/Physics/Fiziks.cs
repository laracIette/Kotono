using Kotono.Graphics.Objects.Meshes;

namespace Kotono.Physics
{
    internal sealed class Fiziks
    {
        internal enum CollisionState
        {
            None,
            BlockAll,
            BlockSelection
        }

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
