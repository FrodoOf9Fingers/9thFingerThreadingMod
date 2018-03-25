using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    class RootPlayPatch
    {
        [HarmonyPatch(typeof(Root_Play), "Start")]
        public static class StartPatch
        {
            [HarmonyPrefix]
            public static void prePatch()
            {
                ReachabilityInstanceContrainer.GetInstance().refreshReachers();
                PathFinderInstanceContainer.GetInstance().refreshFinders();
            }
        }
    }
}
