using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches.Manual_Patches
{
    class ThingOwnerPatches
    {
        /*
        public static void patchAll(HarmonyInstance harmony)
        {

        }

        public static void patchRemove()
        {
            Type[] types = new Type[] { typeof(ThingOwner<Thing>), typeof(ThingOwner<ThingWithComps>), typeof(ThingOwner<Apparel>), typeof(ThingOwner<Pawn>)};
            foreach (Type t in types)
            {
                var original = t.GetMethod("TheMethod");
                var prefix = typeof(ThingOwnerPatches).GetMethod("SomeMethod");
                var postfix = typeof(ThingOwnerPatches).GetMethod("doNothing");
                harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
            }
        }

        public static void doNothing() {}

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
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            else if (__instance is ThingOwner<ThingWithComps>)
            {
                object obj = __instance;
                var mi = typeof(ThingOwner<ThingWithComps>).GetMethod("Remove", new Type[] { typeof(Thing) });
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            else if (__instance is ThingOwner<Apparel>)
            {
                object obj = __instance;
                var mi = typeof(ThingOwner<Apparel>).GetMethod("Remove", new Type[] { typeof(Thing) });
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            else if (__instance is ThingOwner<Pawn>)
            {
                object obj = __instance;
                var mi = typeof(ThingOwner<Pawn>).GetMethod("Remove", new Type[] { typeof(Thing) });
                return UpdateLoopBlockers.ObjectBlockInstance(mi, __params, __instance, ref __result);
            }
            FileLog.Log("ThingOwner<" + __instance.GetType().ToString() + ">.remove not accounted for.");
            return false;
        }*/
    }
}
