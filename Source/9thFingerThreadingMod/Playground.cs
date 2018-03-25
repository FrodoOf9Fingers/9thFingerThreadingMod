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
        [HarmonyPatch(typeof(Map), "ExposeData", new Type[] { })]
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
    }
}
