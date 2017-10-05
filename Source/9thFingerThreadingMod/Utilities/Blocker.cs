using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace _9thFingerThreadingMod
{
    static class Blocker
    {
        private static ObjectIDGenerator idGen = new ObjectIDGenerator();

        private static object _waiters = new ConcurrentDictionary<long, EventWaitHandle>();
        private static ConcurrentDictionary<long, EventWaitHandle> Waiters
        {
            get { return (ConcurrentDictionary<long, EventWaitHandle>)_waiters; }
        }


        public static void Block(Object o)
        {
            long id = idGen.GetId(o, out bool isFirst);

            if (!Waiters.ContainsKey(id))
                Waiters.TryAdd(id, new EventWaitHandle(true, EventResetMode.AutoReset));
            Waiters[id].WaitOne();
        }

        public static void Unblock(Object o)
        {
            long id = idGen.GetId(o, out bool isFirst);
            if (Waiters.ContainsKey(id))
                Waiters[id].Set();
        }

        public static void DumbFree()
        {
            foreach (KeyValuePair<long, EventWaitHandle> kv in Waiters)
            {
                //Mark EventWaitHandles that are not currently blocking for removal. High chance of re-instantiation
                if (kv.Value.WaitOne(0))
                {
                    Waiters.TryRemove(kv.Key, out EventWaitHandle notNeeded);
                }
            }
        }
    }
}
