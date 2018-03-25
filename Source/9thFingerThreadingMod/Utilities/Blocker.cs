using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace _9thFingerThreadingMod
{
    static class Blocker
    {
        public static ObjectIDGenerator idGen = new ObjectIDGenerator();

        private static object _waiters = new ConcurrentDictionary<long, whoWaiter>();
        private static ConcurrentDictionary<long, whoWaiter> Waiters
        {
            get { return (ConcurrentDictionary<long, whoWaiter>)_waiters; }
        }

        private struct whoWaiter
        {
            public EventWaitHandle waiter;
            public int who;
            public whoWaiter(EventWaitHandle waiter, int who)
            {
                this.waiter = waiter;
                this.who = who;
            }
        }

        public static void Block(Object o)
        {
            long id = idGen.GetId(o, out bool isFirst);

            if (!Waiters.ContainsKey(id))
                Waiters.TryAdd(id, new whoWaiter(new EventWaitHandle(true, EventResetMode.AutoReset), Thread.CurrentThread.ManagedThreadId));
            if (Waiters[id].who != Thread.CurrentThread.ManagedThreadId)
                Waiters[id].waiter.WaitOne(50); //Would be great if we could catch exceptional states with a HarmonyPostFix or a new HarmonyCatcher
        }

        public static void Unblock(Object o)
        {
            long id = idGen.GetId(o, out bool isFirst);
            if (Waiters.ContainsKey(id))
                Waiters[id].waiter.Set();
        }

        public static void DumbFree()
        {
            foreach (KeyValuePair<long, whoWaiter> kv in Waiters)
            {
                //Mark EventWaitHandles that are not currently blocking for removal. High chance of re-instantiation
                if (kv.Value.waiter.WaitOne(0))
                {
                    Waiters.TryRemove(kv.Key, out whoWaiter notNeeded);
                }
            }
        }
    }
}
