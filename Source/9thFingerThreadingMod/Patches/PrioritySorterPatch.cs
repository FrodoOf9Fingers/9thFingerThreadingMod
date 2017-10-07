using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse.AI;

namespace _9thFingerThreadingMod.Patches
{
    [HarmonyPatch(typeof(ThinkNode_PrioritySorter), "TryIssueJobPackage")]
    public static class PrioritySorterPatch
    {
        private static object block = new object();

        [HarmonyPrefix]
        public static bool prePatch()
        {
            Blocker.Block(block);
            return true;
        }

        [HarmonyPostfix]
        public static void postFix()
        {
            Blocker.Unblock(block);
        }
    }
}
