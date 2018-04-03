using _9thFingerThreadingMod.Utilities;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    class ListerThingsPatch
    {
        [HarmonyPatch(typeof(ListerThings), "get_AllThings")]
        class ListerThingsAllThingsPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "Remove", new Type[] { typeof(Thing) })]
        class ListerThingsRemove
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "Add", new Type[] { typeof(Thing) })]
        class ListerThingsAdd
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        /* Currently prevents proper startup of the game. I failed in diagnosising the issue.
         * 
         * [HarmonyPatch(typeof(ListerThings), "Contains", new Type[] { typeof(Thing) })]
        class ListerThingsContains
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }*/

        [HarmonyPatch(typeof(ListerThings), "ThingsMatching", new Type[] { typeof(ThingRequest) })]
        class ListerThingsMatching
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "ThingsOfDef", new Type[] { typeof(ThingDef) })]
        class ListerThingsThingsDefOf
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "ThingsInGroup", new Type[] { typeof(ThingRequestGroup) })]
        class ListerThingsThingsInGroup
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }
    }
}
