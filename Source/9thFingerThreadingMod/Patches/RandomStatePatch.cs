using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    /*class RandomStatePatch
    {
        public static object mutex = new object();
    }

    [HarmonyPatch(typeof(Rand), "PopState", new Type[] { })]
    class RandomStatePopPatch
    { 
        [HarmonyPrefix]
        public static bool prefix()
        {
            Blocker.Block(RandomStatePatch.mutex);
            return true;
        }

        [HarmonyPostfix]
        public static void postfix()
        {
            Blocker.Unblock(RandomStatePatch.mutex);
        }
    }

    [HarmonyPatch(typeof(Rand), "PushState", new Type[] { })]
    class RandomStatePushPatch
    {
        [HarmonyPrefix]
        public static bool prefix()
        {
            Blocker.Block(RandomStatePatch.mutex);
            return true;
        }

        [HarmonyPostfix]
        public static void postfix()
        {
            Blocker.Unblock(RandomStatePatch.mutex);
        }
    }*/
}

