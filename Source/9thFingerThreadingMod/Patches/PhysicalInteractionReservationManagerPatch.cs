using _9thFingerThreadingMod.Utilities;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    class PhysicalInteractionReservationManagerPatch
    {
        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "Reserve")]
        class ReservePatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "Release")]
        class ReleasePatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        /*[HarmonyPatch(typeof(PhysicalInteractionReservationManager), "IsReservedBy")]
        class IsReservedByPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "IsReserved")]
        class IsReservedPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "FirstReserverOf")]
        class FirstReserverOfPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "FirstReservationFor")]
        class FirstReservationForPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance, ref object __result)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance, ref __result);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "ReleaseAllForTarget")]
        class ReleaseAllForTargetPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "ReleaseClaimedBy")]
        class ReleaseClaimedByPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }

        [HarmonyPatch(typeof(PhysicalInteractionReservationManager), "ReleaseAllClaimedBy")]
        class ReleaseAllClaimedByPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, __instance);
            }
        }*/
    }
}
