using System;

namespace Kotono.Utils
{
    internal sealed class SingleExecutionAction(Action action)
    {
        private bool _hasBeenExecuted = false;

        private readonly Action _action = action;

        /// <summary>
        /// Invoke the action if it hasn't been before.
        /// </summary>
        internal void Execute()
        {
            if (!_hasBeenExecuted)
            {
                _hasBeenExecuted = true;
                _action();
            }
        }
    }
}
