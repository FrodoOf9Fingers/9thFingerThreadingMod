using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace _9thFingerThreadingMod
{
    static class ExpiringObjectMapper
    {
        private static object _weakReferences = new ConcurrentDictionary<long, WeakReference>();
        private static ConcurrentDictionary<long, WeakReference> weakReferences
        {
            get { return (ConcurrentDictionary<long, WeakReference>)_weakReferences; }
        }

        private static object _replacementReferences = new ConcurrentDictionary<long, object>();
        private static ConcurrentDictionary<long, object> replacementReferences
        {
            get { return (ConcurrentDictionary<long, object>)_replacementReferences; }
        }


        private static ObjectIDGenerator idgen = new ObjectIDGenerator();

        private static Thread thWaitForFullGC = new Thread(new ThreadStart(RoutineCleaning));




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




        private static void RoutineCleaning()
        {
            List<long> idsToRemove = new List<long>();
            while (true)
            {
                Thread.Sleep(5000);

                foreach (KeyValuePair<long, WeakReference> kv in weakReferences)
                {
                    if (!kv.Value.IsAlive)
                        idsToRemove.Add(kv.Key);
                }

                foreach (long key in idsToRemove)
                {
                    replacementReferences.TryRemove(key, out object trash1);
                    weakReferences.TryRemove(key, out WeakReference trash2);
                }

                idsToRemove.Clear();
            }
        }
    }
}
