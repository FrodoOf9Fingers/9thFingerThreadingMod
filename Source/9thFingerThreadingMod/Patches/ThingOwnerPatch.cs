using _9thFingerThreadingMod.Utilities;
using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    class ThingOwnerPatch
    {
        //This 'ThingOwner' object, in particular, may be the breaking point for this project. 
        //Enumerators, etc... Though errors might be ignorable and supressable?

        /*public static bool personalize(String methodName, object __instance, object[] __params, ref object __result, Type[] methodTypes)
        {
            if (__instance is ThingOwner<Thing>)
            {
                var mi = typeof(ThingOwner<Thing>).GetMethod(methodName, methodTypes);
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            else if (__instance is ThingOwner<ThingWithComps>)
            {
                var mi = typeof(ThingOwner<ThingWithComps>).GetMethod(methodName, methodTypes);
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            else if (__instance is ThingOwner<Apparel>)
            {
                var mi = typeof(ThingOwner<Apparel>).GetMethod(methodName, methodTypes);
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            else if (__instance is ThingOwner<Pawn>)
            {
                var mi = typeof(ThingOwner<Pawn>).GetMethod(methodName, methodTypes);
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            FileLog.Log("ThingOwner<" + __instance.GetType().ToString() + ">.remove not accounted for.");
            return true;
        }

        [HarmonyPatch(typeof(ThingOwner<Thing>), "TryAdd", new Type[] { typeof(Thing), typeof(int), typeof(bool) })]
        class TryAddPatch1
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref object __instance, ref object __result)
            {
                return personalize("TryAdd", __instance, __params, ref __result, new Type[] { typeof(Thing), typeof(int), typeof(bool) });
            }
        }

        [HarmonyPatch(typeof(ThingOwner<Thing>), "TryAdd", new Type[] { typeof(Thing), typeof(bool) })]
        class TryAddPatch2
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref object __instance, ref object __result)
            {
                return personalize("TryAdd", __instance, __params, ref __result, new Type[] { typeof(Thing), typeof(bool) });
            }
        }

        [HarmonyPatch(typeof(ThingOwner<Thing>), "TryAddRangeOrTransfer", new Type[] { typeof(IEnumerable<Thing>), typeof(bool), typeof(bool) })]
        class TryAddRangeOrTransferPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref object __instance)
            {
                object isNull = null;
                return personalize("TryAddRangeOrTransfer", __instance, __params, ref isNull, new Type[] { typeof(IEnumerable<Thing>), typeof(bool), typeof(bool) });
            }
        }

        [HarmonyPatch(typeof(ThingOwner<Pawn>), "RemoveAll", new Type[] { typeof(Predicate<Pawn>) })]
        class RemoveAllPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref object __instance, ref object __result)
            {
                return personalize("RemoveAll", __instance, __params, ref __result, new Type[] { typeof(Predicate<Pawn>) });
            }
        }

        [HarmonyPatch(typeof(ThingOwner<Thing>), "Remove", new Type[] { typeof(Thing) })]
        class RemovePatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref object __instance, ref object __result)
            {
                return personalize("Remove", __instance, __params, ref __result, new Type[] { typeof(Thing) });
            }
        }*/
    }
}
