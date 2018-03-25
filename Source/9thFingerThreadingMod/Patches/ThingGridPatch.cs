using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    class ThingGridPatch
    {
        public static object mutex = new object();


        [HarmonyPatch(typeof(ThingGrid), "RegisterInCell")]
        class ThingGridRegisterPatch
        {
            [HarmonyPrefix]
            public static bool prefix()
            {
                Blocker.Block(ThingGridPatch.mutex);
                return true;
            }

            [HarmonyPostfix]
            public static void postfix()
            {
                Blocker.Unblock(ThingGridPatch.mutex);
            }
        }

        [HarmonyPatch(typeof(ThingGrid), "DeregisterInCell")]
        class ThingGridDeregisterPatch
        {
            [HarmonyPrefix]
            public static bool prefix()
            {
                Blocker.Block(ThingGridPatch.mutex);
                return true;
            }

            [HarmonyPostfix]
            public static void postfix()
            {
                Blocker.Unblock(ThingGridPatch.mutex);
            }
        }


    }
}
