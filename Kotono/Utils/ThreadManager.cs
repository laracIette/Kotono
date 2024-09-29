using Kotono.Utils.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Kotono.Utils
{
    internal sealed class ThreadManager : Object
    {
        private static readonly ThreadManager _instance = new();

        private ThreadManager() { }

        private static readonly HashSet<Thread> _threads = [];

        internal static void Start(ThreadStart threadStart)
        {
            ExceptionHelper.ThrowIf(_threads.Count >= short.MaxValue, "couldn't add the Thread, maximum number of threads reached");

            var thread = new Thread(threadStart);

            thread.Start();
            _threads.Add(thread);
        }

        public override void Update()
        {
            Thread[] threads = [.. _threads];

            foreach (var thread in threads)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    _threads.Remove(thread);
                }
            }
        }
    }
}
