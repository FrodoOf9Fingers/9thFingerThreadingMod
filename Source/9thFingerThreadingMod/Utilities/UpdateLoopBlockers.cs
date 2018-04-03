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


        public static bool forceMainThreadPrePatch(int __methodId, ref object __result, System.Object[] __params, Object target = null)
        {
            // Check if this thread is the main thread, if it is, bypass. Else, call to main thread.
            if (Thread.CurrentThread.ManagedThreadId == ThreadingMod.mainThreadId)
                return true;

            if (__params == null || __params.Length == 0)
                __params = null;

            Job job = new Job(delegate { return (System.Object)HarmonyInstance.getMethodBody(__methodId).Invoke(target, __params); });
            TickThreadPatch.mainThreadJobs.Enqueue(job);

            while (!job.isDone)
            { }
            if (job.exception != null)
                FileLog.Log(job.exception.ToString());
            __result = job.result;

            return false;
        }

        public static bool forceMainThreadPrePatch(int __methodId, System.Object[] __params, Object target = null)
        {
            // Check if this thread is the main thread, if it is, bypass. Else, call to main thread.
            if (Thread.CurrentThread.ManagedThreadId == ThreadingMod.mainThreadId)
                return true;

            Job job = new Job(delegate { return (System.Object)HarmonyInstance.getMethodBody(__methodId).Invoke(target, null); });
            TickThreadPatch.mainThreadJobs.Enqueue(job);

            while (!job.isDone)
            { }
            if (job.exception != null)
                FileLog.Log(job.exception.ToString());

            return false;
        }

        public static bool ObjectBlockInstance(int __methodId, object[] __params, object locker)
        {
            object obj = null;
            return ObjectBlockInstance(HarmonyInstance.getMethodBody(__methodId), __params, locker, ref obj);
        }

        public static bool ObjectBlockInstance(MethodBase method, object[] __params, object locker)
        {
            object obj = null;
            return ObjectBlockInstance(method, __params, locker, ref obj);
        }

        public static bool ObjectBlockInstance(int __methodId, object[] __params, object locker, ref object result)
        {
            return ObjectBlockInstance(HarmonyInstance.getMethodBody(__methodId), __params, locker, ref result);
        }

        public static bool ObjectBlockInstance(MethodBase method, object[] __params, object locker, ref object result)
        {
            if (!Waiters.ContainsKey(locker))
                Waiters.TryAdd(locker, new WhoWaiter());

            //If the thread entering is order 2, allow to pass
            if (Thread.CurrentThread.ManagedThreadId == Waiters[locker].getWho())
                return true;

            //If the thread entering is order 1, wait for object, then proceed to order 2.  
            Waiters[locker].waiter.WaitOne();
            Waiters[locker].setWho(Thread.CurrentThread.ManagedThreadId);

            MethodInfo mi = (MethodInfo) method;
            object instance = null;
            if (!mi.IsStatic)
                instance = locker;

            if (mi.ReturnType != typeof(void))
                result = mi.Invoke(instance, __params);
            else
                mi.Invoke(instance, __params);

            Waiters[locker].setWho(-1);
            Waiters[locker].waiter.Set();
            return false;
        }
    }
}
