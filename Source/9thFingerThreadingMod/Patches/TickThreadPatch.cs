using _9thFingerThreadingMod.Utilities;
using Harmony;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Verse;

namespace _9thFingerThreadingMod
{
    [HarmonyPatch(typeof(TickList), "Tick")]
    static class TickThreadPatch
    {
        static bool loopDone = false;
        //Rimworld crashes if I use a Queue
        public static DirtyConccurentQueue<Job> mainThreadJobs = new DirtyConccurentQueue<Job>();


        [HarmonyPrefix]
        public static bool Tick(TickList __instance)
        { 
            TickDetour(__instance);
            return false;
        }

        public static void dumpThreadStacks()
        {
            
        }

        public static void TickDetour(TickList __instance)
        {
            try
            {
                FieldInfo registerThings = typeof(TickList).GetField("thingsToRegister", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo deregisterThings = typeof(TickList).GetField("thingsToDeregister", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo listsThings = typeof(TickList).GetField("thingLists", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo tickt = typeof(TickList).GetField("tickType", BindingFlags.NonPublic | BindingFlags.Instance);

                MethodInfo bucket = typeof(TickList).GetMethod("BucketOf", BindingFlags.NonPublic | BindingFlags.Instance);


                for (int i = 0; i < ((List<Thing>)registerThings.GetValue(__instance)).Count; i++)
                {
                    ((List<Thing>)bucket.Invoke(__instance, new object[] { ((List<Thing>)registerThings.GetValue(__instance))[i] }))
                        .Add(((List<Thing>)registerThings.GetValue(__instance))[i]);
                }
            ((List<Thing>)registerThings.GetValue(__instance)).Clear();
                for (int j = 0; j < ((List<Thing>)deregisterThings.GetValue(__instance)).Count; j++)
                {
                    ((List<Thing>)bucket.Invoke(__instance, new object[] { ((List<Thing>)deregisterThings.GetValue(__instance))[j] }))
                        .Remove(((List<Thing>)deregisterThings.GetValue(__instance))[j]);
                }
            ((List<Thing>)deregisterThings.GetValue(__instance)).Clear();
                if (DebugSettings.fastEcology)
                {
                    Find.World.tileTemperatures.ClearCaches();
                    for (int k = 0; k < ((List<List<Thing>>)listsThings.GetValue(__instance)).Count; k++)
                    {
                        List<Thing> list = ((List<List<Thing>>)listsThings.GetValue(__instance))[k];
                        for (int l = 0; l < list.Count; l++)
                        {
                            if (list[l].def.category == ThingCategory.Plant)
                            {
                                list[l].TickLong();
                            }
                        }
                    }
                }
                List<Thing> list2 = ((List<List<Thing>>)listsThings.GetValue(__instance))[Find.TickManager.TicksGame % TickInterval(((TickerType)tickt.GetValue(__instance)))];

                //ThreadPool.QueueUserWorkItem(new WaitCallback(mainUpdateLoop), new WorkSet(list2, tickt, __instance));
                //var t2 = new Thread(() => mainUpdateLoop(new WorkSet(list2, tickt, __instance)));
                //t2.Start();

                Thread t = new Thread(new ParameterizedThreadStart(mainUpdateLoop));
                t.Start(new WorkSet(list2, tickt, __instance));

                TimeSpan timeout = TimeSpan.FromSeconds(30);
                DateTime start_time = DateTime.Now;

                while (!loopDone)
                {
                    if (mainThreadJobs.Count != 0)
                    {
                        Job job;
                        job = mainThreadJobs.Dequeue();
                        if (job != null)
                            job.doJob();
                    }
                    if ((DateTime.Now - start_time) > timeout)
                    {
                        t.Abort();
                        throw new Exception("Threads needed to be aborted");
                        break;
                    }
                }
                loopDone = false;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        private class WorkSet
        {
            public List<Thing> list2;
            public FieldInfo tickt;
            public TickList __instance;

            public WorkSet(List<Thing> list2, FieldInfo tickt, TickList instance)
            {
                this.list2 = list2;
                this.tickt = tickt;
                __instance = instance;
            }
        }

        private static void mainUpdateLoop(Object workSet)
        {
            WorkSet set = (WorkSet)workSet;

            List<Thing> list2 = set.list2;
            FieldInfo tickt = set.tickt;
            TickList __instance = set.__instance;

            Parallel.For(0, list2.Count, new ParallelOptions { MaxDegreeOfParallelism = ThreadingMod.NUM_THREADS_PER_MAP}, m =>
            {
                if (!list2[m].Destroyed)
                {
                    try
                    {
                        switch (((TickerType)tickt.GetValue(__instance)))
                        {
                            case TickerType.Normal:
                                list2[m].Tick();
                                break;
                            case TickerType.Rare:
                                list2[m].TickRare();
                                break;
                            case TickerType.Long:
                                list2[m].TickLong();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Prefs.DevMode)
                        {
                            Log.Error("Exception ticking " + list2[m].ToString() + ": " + ex.ToString());
                        }
                    }
                }
            });

            loopDone = true;
        }

        private static int TickInterval(TickerType type)
        {
            switch (type)
            {
                case TickerType.Normal:
                    return 1;
                case TickerType.Rare:
                    return 250;
                case TickerType.Long:
                    return 2000;
                default:
                    return -1;
            }
        }
    }
}
