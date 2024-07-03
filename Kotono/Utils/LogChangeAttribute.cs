using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Runtime.CompilerServices;

namespace Kotono.Utils
{
    [PSerializable]
    internal class LogChangeAttribute : OnMethodBoundaryAspect
    {
        private string _propertyName;

        private bool _isLogWhenSame;

        private object? _oldValue;

        internal LogChangeAttribute(string propertyName, bool isLogWhenSame)
            : base()
        {
            _propertyName = propertyName;
            _isLogWhenSame = isLogWhenSame;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _oldValue = GetValue(args.Instance);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var newValue = GetValue(args.Instance);

            if (_isLogWhenSame || (!_oldValue?.Equals(newValue) ?? false))
            {
                Logger.Log($"{_propertyName}: Old Value: {_oldValue}, New Value: {newValue}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private object? GetValue(object instance) => instance.GetType().GetProperty(_propertyName)?.GetValue(instance);
    }
}
