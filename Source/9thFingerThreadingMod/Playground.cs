using Harmony;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Verse;
using Verse.Sound;

namespace _9thFingerThreadingMod
{
    static class Playground
    {
        /*[HarmonyPatch(typeof(Map), "ExposeData", new Type[] { })]
        class RandomStatePopPatch
        {
            [HarmonyPrefix]
            public static bool prefix(Map __instance)
            {
                FileLog.Log("Exposing Map");
                FileLog.Log(Scribe.EnterNode("things").ToString());
                FileLog.Log(__instance.listerThings.AllThings.Count.ToString());
                return true;
            }
        }

        [HarmonyPatch(typeof(DeepProfiler), "Start")]
        class test3
        {
            [HarmonyPrefix]
            public static bool prefix(String label)
            {
                Log.Message("Profiler Debug: " + label);
                return true;
            }
        }

        [HarmonyPatch(typeof(DeepProfiler), "End")]
        class test4
        {
            [HarmonyPrefix]
            public static bool prefix()
            {
                Log.Message("Profiler end");
                return true;
            }
        }

        /*[HarmonyPatch(typeof(RegionAndRoomUpdater), "RebuildAllRegionsAndRooms")]
        class test6
        {
            static int testing = 0;
            [HarmonyPrefix]
            public static void prefix()
            {
                    testing++;
                    FileLog.Log("Rebuild All Regions " + testing.ToString() + " " + Thread.CurrentThread.ManagedThreadId.ToString());
                if (testing > 1)
                    throw new Exception();
            }
        }*/
    }
}
