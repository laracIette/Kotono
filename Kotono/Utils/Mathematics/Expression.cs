using System;
using System.Collections.Generic;

namespace Kotono.Utils.Mathematics
{
    internal class Expression
    {
        private float _currentResult = 0.0f;

        private readonly List<Delegate> _steps = [];

        internal Expression(string input) // "6^2 * (2 + 2)"
        {
            const string OPERANDS_0 = "^";
            const string OPERANDS_1 = "*/";
            const string OPERANDS_2 = "+-";

            input = input.Keep("0123456789*/+-()^x");
        }

        private void Add(float other)
        {
            _currentResult += other;
        }

        internal float GetResult(float value)
        {
            return _currentResult;
        }
    }
}
