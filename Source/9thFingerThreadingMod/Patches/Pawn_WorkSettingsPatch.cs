using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using _9thFingerThreadingMod.Utilities;
using Harmony;

namespace _9thFingerThreadingMod.Patches
{
    [HarmonyPatch(typeof(Pawn_WorkSettings), "CacheWorkGiversInOrder")]
    static class Pawn_WorkSettingsPatch
    {
        [HarmonyPrefix]
        static bool CacheWorkGiversInOrder(ref Pawn_WorkSettings __instance)
        {
            List<WorkTypeDef> wtsByPrio = new List<WorkTypeDef>();
            List<WorkGiver> workGiversInOrderEmerg = ((List<WorkGiver>) __instance.GetField("workGiversInOrderEmerg"));
            List<WorkGiver> workGiversInOrderNormal = ((List<WorkGiver>) __instance.GetField("workGiversInOrderNormal"));

            List<WorkTypeDef> allDefsListForReading = DefDatabase<WorkTypeDef>.AllDefsListForReading;

            int num = 999;
            for (int i = 0; i < allDefsListForReading.Count; i++)
            {
                WorkTypeDef workTypeDef = allDefsListForReading[i];
                int priority = __instance.GetPriority(workTypeDef);
                if (priority > 0)
                {
                    if (priority < num)
                    {
                        if (workTypeDef.workGiversByPriority.Any((WorkGiverDef wg) => !wg.emergency))
                        {
                            num = priority;
                        }
                    }
                    wtsByPrio.Add(workTypeDef);
                }
            }
            var tmp = __instance;
            wtsByPrio.InsertionSort(delegate (WorkTypeDef a, WorkTypeDef b)
            {
                float value = (float)(a.naturalPriority + (4 - tmp.GetPriority(a)) * 100000);
                return ((float)(b.naturalPriority + (4 - tmp.GetPriority(b)) * 100000)).CompareTo(value);
            });
            workGiversInOrderEmerg.Clear();
            for (int j = 0; j < wtsByPrio.Count; j++)
            {
                WorkTypeDef workTypeDef2 = wtsByPrio[j];
                for (int k = 0; k < workTypeDef2.workGiversByPriority.Count; k++)
                {
                    WorkGiver worker = workTypeDef2.workGiversByPriority[k].Worker;
                    if (worker.def.emergency && __instance.GetPriority(worker.def.workType) <= num)
                    {
                        workGiversInOrderEmerg.Add(worker);
                    }
                }
            }
            workGiversInOrderNormal.Clear();
            for (int l = 0; l < wtsByPrio.Count; l++)
            {
                WorkTypeDef workTypeDef3 = wtsByPrio[l];
                for (int m = 0; m < workTypeDef3.workGiversByPriority.Count; m++)
                {
                    WorkGiver worker2 = workTypeDef3.workGiversByPriority[m].Worker;
                    if (!worker2.def.emergency || __instance.GetPriority(worker2.def.workType) > num)
                    {
                        workGiversInOrderNormal.Add(worker2);
                    }
                }
            }
            __instance.SetField("workGiversDirty", false);
            return false;
        }
    }
}
