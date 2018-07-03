using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iChanServer
{
    //これであってる?
    class TaskQueue
    {
        private readonly Queue<Action> _queue;
        private readonly AutoResetEvent _autoResetEvent;
        private readonly CancellationTokenSource _tokenSource;
        public TaskQueue()
        {
            _queue = new Queue<Action>();
            _autoResetEvent = new AutoResetEvent(false);
            _tokenSource = new CancellationTokenSource();
        }

        private void Run(CancellationToken token)
        {
            while (true)
            {
                _autoResetEvent.WaitOne();
                Action[] copy;
                lock (_queue)
                {
                    int loopCount = _queue.Count;
                    copy = new Action[loopCount];
                    for (int i = 0; i < loopCount; i++)
                    {
                        copy[i] = _queue.Dequeue();
                    }
                }

                foreach (var action in copy)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    action();
                }
            }
        }

        internal void Start()
        {
            Task.Run(() => Run(_tokenSource.Token));
        }

        internal void Stop()
        {
            _tokenSource.Cancel();
        }

        internal void Enqueue(Action action)
        {
            lock (_queue)
            {
                _queue.Enqueue(action);
            }

            _autoResetEvent.Set();
        }
    }
}
