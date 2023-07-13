using System;

namespace Kotono.Tests
{
    internal class MultiType
    {
        private object? _value = null;

        internal object Value
        {
            get
            {
                return _value ?? throw new Exception($"error: _value must not be null");
            }
            set
            {
                _value = value;
            }
        }

        public MultiType(object value)
        {
            Value = value;
        }

        public static implicit operator string(MultiType multiType)
        {
            return Convert.ToString(multiType.Value) ?? throw new Exception("error: return value of type string must not be null");
        }

        public static implicit operator float(MultiType multiType)
        {
            return Convert.ToSingle(multiType.Value);
        }

        public static implicit operator double(MultiType multiType)
        {
            return Convert.ToDouble(multiType.Value);
        }

        public static implicit operator int(MultiType multiType)
        {
            return Convert.ToInt32(multiType.Value);
        }
    }
}
