using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod.Patches
{
    [HarmonyPatch(typeof(ThinkNode_PrioritySorter), "TryIssueJobPackage")]
    public static class PrioritySorterPatch
    {
        [HarmonyPrefix]
        public static bool prePatch(ThinkNode_PrioritySorter __instance, Pawn pawn, JobIssueParams jobParams, ref ThinkResult __result)
        {
            __result = TryIssueJobPackage(pawn, jobParams, __instance.subNodes, __instance.minPriority);
            return false;
        }

        private static ThinkResult TryIssueJobPackage(Pawn pawn, JobIssueParams jobParams, List<ThinkNode> subNodes, float minPriority)
        {
            List<ThinkNode> workingNodes = new List<ThinkNode>();
            int count = subNodes.Count;
            for (int i = 0; i < count; i++)
            {
                workingNodes.Insert(Rand.Range(0, workingNodes.Count - 1), subNodes[i]);
            }
            while (workingNodes.Count > 0)
            {
                float num = 0f;
                int num2 = -1;
                for (int j = 0; j < workingNodes.Count; j++)
                {
                    float num3 = 0f;
                    try
                    {
                        num3 = workingNodes[j].GetPriority(pawn);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Think node error under threading, priority sorter");
                    }
                    if (num3 > 0f && num3 >= minPriority)
                    {
                        if (num3 > num)
                        {
                            num = num3;
                            num2 = j;
                        }
                    }
                }
                if (num2 == -1)
                {
                    break;
                }
                ThinkResult result = ThinkResult.NoJob;
                try
                {
                    result = workingNodes[num2].TryIssueJobPackage(pawn, jobParams);
                }
                catch (Exception ex2)
                {
                    Log.Error("Think node error under threading, priority sorter");
                }
                if (result.IsValid)
                {
                    return result;
                }
                workingNodes.RemoveAt(num2);
            }
            return ThinkResult.NoJob;
        }
    }
}
