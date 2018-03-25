using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse.Sound;

namespace _9thFingerThreadingMod.Patches
{

    static class SustainerManagerPatch
    {
        internal static object listMutex = new object();
    }

    [HarmonyPatch(typeof(SustainerManager), "RegisterSustainer")]
    static class RegisterSustainerPatch
    {
        [HarmonyPrefix]
        public static bool prefix(SustainerManager __instance)
        {
            Blocker.Block(SustainerManagerPatch.listMutex);
            return true;

        }

        [HarmonyPostfix]
        public static void postfix(SustainerManager __instance)
        {
            Blocker.Unblock(SustainerManagerPatch.listMutex);
        }
    }

    [HarmonyPatch(typeof(SustainerManager), "DeregisterSustainer")]
    static class DeregisterSustainerPatch
    {
        [HarmonyPrefix]
        public static bool prefix(SustainerManager __instance)
        {
            Blocker.Block(SustainerManagerPatch.listMutex);
            return true;

        }

        [HarmonyPostfix]
        public static void postfix(SustainerManager __instance)
        {
            Blocker.Unblock(SustainerManagerPatch.listMutex);
        }
    }

    [HarmonyPatch(typeof(SustainerManager), "UpdateAllSustainerScopes")]
    static class UpdateAllSustainerScopesPatch
    {
        [HarmonyPrefix]
        public static bool prefix(SustainerManager __instance)
        {
            Blocker.Block(SustainerManagerPatch.listMutex);
            return true;

        }

        [HarmonyPostfix]
        public static void postfix(SustainerManager __instance)
        {
            Blocker.Unblock(SustainerManagerPatch.listMutex);
        }
    }
}
