using Kotono.Utils.Exceptions;
using System;
using System.Collections.Generic;

namespace Kotono.Utils
{
    internal sealed class ExecuteAction : Object
    {
        private sealed record class DelayedAction(Action Action, float Time);

        private readonly List<DelayedAction> _delayedActions = [];

        private static readonly Lazy<ExecuteAction> _instance = new(() => new());

        private static ExecuteAction Instance => _instance.Value;

        private ExecuteAction() { }

        internal static void Delay(Action action, float delay)
        {
            ExceptionHelper.ThrowIf(delay < 0.0f, $"delay '{delay}' should not be negative");

            Instance._delayedActions.Add(new DelayedAction(action, Time.Now + delay));
        }

        public override void Update()
        {
            DelayedAction[] delayedActions = [.. _delayedActions];

            foreach (var delayedAction in delayedActions)
            {
                if (Time.Now >= delayedAction.Time)
                {
                    delayedAction.Action();
                    _delayedActions.Remove(delayedAction);
                }
            }
        }
    }
}
