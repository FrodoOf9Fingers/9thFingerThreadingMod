using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod
{
    [HarmonyPatch(typeof(RegionTraverser))]
    class RegionTraverserPatch
    {
        [HarmonyTargetMethod]
        static MethodBase TargetMethod()
        {
            return (MethodBase)typeof(RegionTraverser).GetConstructor(new Type[] { });
        }

        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> increaseNumWorkers(IEnumerable<CodeInstruction> instr)
        {
            foreach (CodeInstruction ci in instr)
            {
                if(ci.opcode == OpCodes.Ldc_I4_8)
                {
                    FileLog.Log(ci.ToString());
                    ci.opcode = OpCodes.Ldc_I4;
                    ci.operand = 8 * ThreadingMod.NUM_THREADS_PER_MAP;
                    FileLog.Log(ci.ToString());
                }  
            }
            return instr;
        }
    }
}
