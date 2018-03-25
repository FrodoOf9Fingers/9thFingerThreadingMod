using Harmony;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Verse;

namespace _9thFingerThreadingMod.Utilities
{
    static class UpdateLoopBlockers
    {

        private static object _waiters = new ConcurrentDictionary<object, WhoWaiter>();
        private static ConcurrentDictionary<object, WhoWaiter> Waiters
        {
            get { return (ConcurrentDictionary<object, WhoWaiter>)_waiters; }
        }

        private class WhoWaiter
        {
            private int who = -1;
            public void setWho(int newWho) { who = newWho; }
            public int getWho() { return who; }


            public EventWaitHandle waiter = new EventWaitHandle(true, EventResetMode.AutoReset);
        }

        private readonly static object defaultReference = new object();


        public static bool forceMainThreadPrePatch(int __methodId, ref object __result, System.Object[] __params)
        {
            // Check if this thread is the main thread, if it is, bypass. Else, call to main thread.
            if (Thread.CurrentThread.ManagedThreadId == ThreadingMod.mainThreadId)
                return true;

            if (__params == null || __params.Length == 0)
                __params = null;

            Job job = new Job(delegate { return (System.Object)HarmonyInstance.getMethodBody(__methodId).Invoke(null, __params); });
            TickThreadPatch.mainThreadJobs.Enqueue(job);

            while (!job.isDone)
            { }
            if (job.exception != null)
                FileLog.Log(job.exception.ToString());
            __result = job.result;

            return false;
        }

        public static bool forceMainThreadPrePatch(int __methodId)
        {
            // Check if this thread is the main thread, if it is, bypass. Else, call to main thread.
            if (Thread.CurrentThread.ManagedThreadId == ThreadingMod.mainThreadId)
                return true;

            Job job = new Job(delegate { return (System.Object)HarmonyInstance.getMethodBody(__methodId).Invoke(null, null); });
            TickThreadPatch.mainThreadJobs.Enqueue(job);

            while (!job.isDone)
            { }
            if (job.exception != null)
                FileLog.Log(job.exception.ToString());

            return false;
        }

        public static bool ObjectBlockInstance(int __methodId, object[] __params, object __instance, bool isGeneric = false)
        {
            object obj = null;
            return ObjectBlockInstance(HarmonyInstance.getMethodBody(__methodId), __params, __instance, ref obj, isGeneric);
        }

        public static bool ObjectBlockInstance(MethodBase method, object[] __params, object __instance, bool isGeneric = false)
        {
            object obj = null;
            return ObjectBlockInstance(method, __params, __instance, ref obj, isGeneric);
        }

        public static bool ObjectBlockInstance(int __methodId, object[] __params, object __instance, ref object result, bool isGeneric = false)
        {
            return ObjectBlockInstance(HarmonyInstance.getMethodBody(__methodId), __params, __instance, ref result, isGeneric);
        }

        public static bool ObjectBlockInstance(MethodBase method, object[] __params, object __instance, ref object result, bool isGeneric = false)
        {
            if (!Waiters.ContainsKey(__instance))
                Waiters.TryAdd(__instance, new WhoWaiter());

            //If the thread entering is order 2, allow to pass
            if (Thread.CurrentThread.ManagedThreadId == Waiters[__instance].getWho())
                return true;

            //If the thread entering is order 1, wait for object, then proceed to order 2.  
            Waiters[__instance].waiter.WaitOne();
            Waiters[__instance].setWho(Thread.CurrentThread.ManagedThreadId);

            MethodInfo mi;
            if (isGeneric)
            {
                mi = ((MethodInfo) method).MakeGenericMethod(__instance.GetType().GetGenericArguments());
            }
            else
            {
                mi = (MethodInfo) method;
            }
            if (mi.ReturnType != typeof(void))
                result = mi.Invoke(__instance, __params);
            else
                mi.Invoke(__instance, __params);

            Waiters[__instance].setWho(-1);
            Waiters[__instance].waiter.Set();
            return false;
        }
    }
}
