using Kotono.Utils.Exceptions;
using System.Collections.Generic;
using System.Threading;

namespace Kotono.Utils
{
    internal class ThreadManager : Object
    {
        private static readonly ThreadManager _instance = new();

        private ThreadManager() { }

        private static readonly List<Thread> _threads = [];

        internal static void Start(ThreadStart threadStart)
        {
            if (_threads.Count < 32767)
            {
                var thread = new Thread(threadStart);

                thread.Start();
                _threads.Add(thread);
            }
            else
            {
                throw new KotonoException("couldn't add the Thread, maximum number of threads reached");
            }
        }

        public override void Update()
        {
            for (int i = _threads.Count - 1; i >= 0; i--)
            {
                if (_threads[i].ThreadState == ThreadState.Stopped)
                {
                    _threads.RemoveAt(i);
                }
            }
        }
    }
}
