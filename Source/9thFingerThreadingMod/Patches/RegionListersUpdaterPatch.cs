using _9thFingerThreadingMod.Utilities;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    static class RegionListersUpdaterPatch
    {
        public static readonly object locker = new object();

        [HarmonyPatch(typeof(RegionListersUpdater), "GetTouchableRegions")]
        class RegionListersUpdaterGetTouchableRegions
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, locker);
            }
        }

        [HarmonyPatch(typeof(RegionListersUpdater), "RegisterInRegions")]
        class RegionListersUpdaterRegisterInRegions
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, locker);
            }
        }

        [HarmonyPatch(typeof(RegionListersUpdater), "DeregisterInRegions")]
        class RegionListersUpdaterDeregisterInRegions
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, locker);
            }
        }

        [HarmonyPatch(typeof(RegionListersUpdater), "RegisterAllAt")]
        class RegionListersUpdaterRegisterAllAt
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params)
            {
                return UpdateLoopBlockers.ObjectBlockInstance(__methodId, __params, locker);
            }
        }
    }
}
