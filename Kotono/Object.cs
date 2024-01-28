using Kotono.File;

namespace Kotono
{
    internal abstract class Object : IObject
    {
        protected ObjectSettings _settings;

        internal Object(ObjectSettings settings) 
        { 
            _settings = settings;
        }

        public virtual void Update() { }
    }
}
