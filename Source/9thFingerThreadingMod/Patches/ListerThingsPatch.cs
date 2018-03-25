using _9thFingerThreadingMod.Utilities;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                //FileLog.Log("get_AllThings");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "Remove", new Type[] { typeof(Thing) })]
        class ListerThingsRemove
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                //FileLog.Log("Remove");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "Add", new Type[] { typeof(Thing) })]
        class ListerThingsAdd
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                //FileLog.Log("Add");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "Contains", new Type[] { typeof(Thing) })]
        class ListerThingsContains
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                FileLog.Log("Contains State: ");
                try
                {
                    FileLog.Log(__methodId.ToString());
                }
                catch
                {
                    FileLog.Log("methodId Null!");
                }
                try
                {
                    FileLog.Log(__params[0].ToString());
                }
                catch
                {
                    FileLog.Log("__params Null!");
                }
                try
                {
                    FileLog.Log(__instance.ToString());
                }
                catch
                {
                    FileLog.Log("__instance Null!");
                }
                try
                {
                    FileLog.Log(__result.ToString());
                }
                catch
                {
                    FileLog.Log("__result Null!");
                }
                FileLog.Log("\n");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "ThingsMatching", new Type[] { typeof(ThingRequest) })]
        class ListerThingsMatching
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                //FileLog.Log("ThingsMatching");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "ThingsOfDef", new Type[] { typeof(ThingDef) })]
        class ListerThingsThingsDefOf
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                //FileLog.Log("ThingsOfDef");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(ListerThings), "ThingsInGroup", new Type[] { typeof(ThingRequestGroup) })]
        class ListerThingsThingsInGroup
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                //FileLog.Log("ThingsInGroup");
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }
    }
}
