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
        private static ConcurrentMap<long, EventWaitHandle> waiters = new ConcurrentMap<long, EventWaitHandle>();

        public static void block(Object o)
        {
            long id = idGen.GetId(o, out bool isFirst);

            if (!waiters.ContainsKey(id))
                waiters.TryAdd(id, new EventWaitHandle(true, EventResetMode.AutoReset));
            Harmony.FileLog.Log("Start Wait on: " + id.ToString());
            waiters[id].WaitOne(2000);
            Harmony.FileLog.Log("End Wait on: " + id.ToString());
        }

        public static void unblock(Object o)
        {
            long id = idGen.GetId(o, out bool isFirst);
            if (waiters.ContainsKey(id))
                waiters[id].Set();
            Harmony.FileLog.Log("Returned lock: " + id.ToString());
        }

        public static void dumbFree()
        {
            List<long> idsToRemove = new List<long>();
            lock (waiters)
            {
                foreach(KeyValuePair<long, EventWaitHandle> kv in waiters.enumerate())
                {
                    //Mark EventWaitHandles that are not currently blocking for removal. High chance of re-instantiation
                    if (kv.Value.WaitOne(0))
                    {
                        waiters.TryRemove(kv.Key);
                    }
                }
            }
            foreach(long key in idsToRemove)
            {
                waiters.TryRemove(key);
            }
        }
    }
}
