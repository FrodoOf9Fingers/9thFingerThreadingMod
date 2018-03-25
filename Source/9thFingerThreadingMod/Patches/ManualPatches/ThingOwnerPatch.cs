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
        //This 'ThingOwner' object, in particular, may be the breaking point for this project. Enumerators, etc... Though errors might be ignorable and supressable?

        /*[HarmonyPatch(typeof(ThingOwner), "Remove")]
        class RemovePatch1
        {
            [HarmonyPrefix]
            public static bool prefix(object[] __params, object __instance, ref object __result)
            {
                FileLog.Log("Entered ThingOwner");
                if (__instance is ThingOwner<Thing>)
                    FileLog.Log("Is Thing");
                if (__instance is ThingOwner<ThingWithComps>)
                    FileLog.Log("Is ThingWithComps");
                if (__instance is ThingOwner<Apparel>)
                    FileLog.Log("Is Apparel");
                if (__instance is ThingOwner<Pawn>)
                    FileLog.Log("Is Pawn");
                FileLog.Log("Continuing ThingOwner");

                if (__instance is ThingOwner<Thing>)
                {
                    object obj = __instance;
                    var mi = typeof(ThingOwner<Thing>).GetMethod("Remove", new Type[] { typeof(Thing) });
                    return SelfPassingBlocker.ObjectBlockInstance(mi, __params, __instance, ref __result);
                }
                else if (__instance is ThingOwner<ThingWithComps>)
                {
                    object obj = __instance;
                    var mi = typeof(ThingOwner<ThingWithComps>).GetMethod("Remove", new Type[] { typeof(Thing) });
                    return SelfPassingBlocker.ObjectBlockInstance(mi, __params, __instance, ref __result);
                }
                else if (__instance is ThingOwner<Apparel>)
                {
                    object obj = __instance;
                    var mi = typeof(ThingOwner<Apparel>).GetMethod("Remove", new Type[] { typeof(Thing) });
                    return SelfPassingBlocker.ObjectBlockInstance(mi, __params, __instance, ref __result);
                }
                else if(__instance is ThingOwner<Pawn>)
                {
                    object obj = __instance;
                    var mi = typeof(ThingOwner<Pawn>).GetMethod("Remove", new Type[] { typeof(Thing) });
                    return SelfPassingBlocker.ObjectBlockInstance(mi, __params, __instance, ref __result);
                }
                FileLog.Log("ThingOwner<" + __instance.GetType().ToString() + ">.remove not accounted for.");
                return false;
            }
        }

        //public static bool prefix(int __methodId, object[] __params, Object __instance)
        //{
        //    return SelfPassingBlocker.ObjectBlockInstance(__methodId, __params, __instance);
        //}

        /*[HarmonyPatch(typeof(ThingOwner), "TryAdd", new Type[] { typeof(Thing), typeof(int), typeof(bool) })]
        class TryAddPatch1
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref RealtimeMoteList __instance)
            {
                return SelfPassingBlocker.SameThreadOnlyPrepatch(__methodId, __params, __instance, true);
            }
        }

        [HarmonyPatch(typeof(ThingOwner), "TryAdd", new Type[] { typeof(Thing), typeof(bool) })]
        class TryAddPatch2
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref RealtimeMoteList __instance)
            {
                return SelfPassingBlocker.SameThreadOnlyPrepatch(__methodId, __params, __instance, true);
            }
        }

        [HarmonyPatch(typeof(ThingOwner), "TryAddRangeOrTransfer", new Type[] { typeof(IEnumerable<Thing>), typeof(bool), typeof(bool) })]
        class TryAddRangeOrTransferPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref RealtimeMoteList __instance)
            {
                return SelfPassingBlocker.SameThreadOnlyPrepatch(__methodId, __params, __instance, true);
            }
        }

        [HarmonyPatch(typeof(ThingOwner), "RemoveAll", new Type[] { typeof(Predicate<Pawn>) })]
        class RemoveAllPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref RealtimeMoteList __instance)
            {
                return SelfPassingBlocker.SameThreadOnlyPrepatch(__methodId, __params, __instance, true);
            }
        }

        [HarmonyPatch(typeof(ThingOwner), "Remove", new Type[] { typeof(Thing) })]
        class RemovePatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, ref RealtimeMoteList __instance)
            {
                return SelfPassingBlocker.SameThreadOnlyPrepatch(__methodId, __params, __instance, true);
            }
        }*/
    }
}
