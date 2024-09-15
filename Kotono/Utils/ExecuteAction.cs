using Kotono.Utils.Exceptions;
using System;
using System.Collections.Generic;

namespace Kotono.Utils
{
    internal sealed class ExecuteAction : Object
    {
        private sealed record class DelayedAction(Action Action, float Time);

        private static readonly ExecuteAction _instance = new();

        private readonly List<DelayedAction> _actionDelays = [];

        private ExecuteAction() { }

        internal static void Delay(Action action, float delay = 0.0f)
        {
            if (delay < 0.0f)
            {
                throw new KotonoException($"delay \"{delay}\" should not be negative");
            }
            else
            {
                _instance._actionDelays.Add(new DelayedAction(action, Time.Now + delay));
            }
        }

        public override void Update()
        {
            for (int i = _actionDelays.Count - 1; i >= 0; i--)
            {
                if (Time.Now >= _actionDelays[i].Time)
                {
                    _actionDelays[i].Action();
                    _actionDelays.RemoveAt(i);
                }
            }
        }
    }
}
