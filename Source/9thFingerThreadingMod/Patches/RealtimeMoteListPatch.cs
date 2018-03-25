using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    class RealtimeMoteListPatch
    {
        public static Object locker = new Object();

        [HarmonyPatch(typeof(RealtimeMoteList), "MoteSpawned", new Type[] {typeof(Mote)})]
        class MoteSpawnedPatch
        {
            [HarmonyPrefix]
            public static bool prefix(ref RealtimeMoteList __instance, Mote newMote)
            {
                lock (locker)
                {
                    __instance.allMotes.Add(newMote);
                }
                return false;
            }
        }

        [HarmonyPatch(typeof(RealtimeMoteList), "MoteDespawned", new Type[] { typeof(Mote) })]
        class MoteDespawnedPatch
        {
            [HarmonyPrefix]
            public static bool prefix(ref RealtimeMoteList __instance, Mote oldMote)
            {
                lock (locker)
                {
                    __instance.allMotes.Remove(oldMote);
                }
                return false;
            }
        }
    }
}
