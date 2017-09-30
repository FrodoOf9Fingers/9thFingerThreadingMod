using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace _9thFingerThreadingMod
{
    static class ExpiringObjectMapper
    {
        private static ConcurrentMap<long, WeakReference> weakReferences = new ConcurrentMap<long, WeakReference>();
        private static ConcurrentMap<long, Object> replacementReferences = new ConcurrentMap<long, object>();
        private static ObjectIDGenerator idgen = new ObjectIDGenerator();

        private static Thread thWaitForFullGC = new Thread(new ThreadStart(cleaner));

        public static void AddReference(Object weak, Object replacement)
        {
            long id = idgen.GetId(weak, out bool isnew);

            weakReferences.TryAdd(id, new WeakReference(weak));
            replacementReferences.TryAdd(id, replacement);
        }

        public static Object GetReplacement(Object weak)
        {
            long id = idgen.GetId(weak, out bool isnew);
            if (isnew)
                return null;
            if (replacementReferences.TryGetValue(id, out var value))
            {
                return value;
            }
            return null;
        }

        private static void cleaner()
        {
            List<long> idsToRemove = new List<long>();
            while (true)
            {
                Thread.Sleep(5000);
                lock (weakReferences)
                {
                    foreach (KeyValuePair<long, WeakReference> kv in weakReferences.enumerate())
                    {
                        if (!kv.Value.IsAlive)
                            idsToRemove.Add(kv.Key);
                    }
                    lock (replacementReferences)
                    {
                        foreach (long key in idsToRemove)
                        {
                            replacementReferences.TryRemove(key);
                            weakReferences.TryRemove(key);
                        }
                    }
                }
                idsToRemove.Clear();
            }
        }
    }
}
