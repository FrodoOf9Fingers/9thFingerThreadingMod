using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod
{
   /* [HarmonyPatch(typeof(RegionTraverser), "BreadthFirstTraverse", new Type[] { typeof(Region), typeof(RegionEntryPredicate), typeof(RegionProcessor), typeof(int), typeof(RegionType)})]
    class debugPatch
    {
        [HarmonyPrefix]
        public static bool prefix()
        {
            var obj = typeof(RegionTraverser).GetField("freeWorkers", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            FileLog.Log("Num items: " + obj.GetType().GetProperty("Count").GetValue(obj, null).ToString() +
            " Num workers: " + RegionTraverser.NumWorkers.ToString());
            
            return true;
        }
    }*/
}
