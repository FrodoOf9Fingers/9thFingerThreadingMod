using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _9thFingerThreadingMod.Utilities
{
    class DirtyConccurentQueue<T>
    {
        private Queue<T> queue = new Queue<T>();
        private readonly object _syncObject = new object();
        public int Count { get { return queue.Count; } }

        public void Enqueue(T obj)
        {
            lock (_syncObject)
            {
                queue.Enqueue(obj);
            }
        }

        public T Dequeue()
        {
            lock (_syncObject)
            {
                return queue.Dequeue();
            }
        }
    }
}
