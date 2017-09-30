using _9thFingerThreadingMod.Replacement_Functions;
using Harmony;
using Harmony.ILCopying;
using HugsLib;
using RimWorld;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace _9thFingerThreadingMod
{
    public class ThreadingMod : ModBase
    {
        public const int NUM_THREADS_PER_MAP = 8;
        public override string ModIdentifier => "threadingmod";
        ThreadingMod()
        {
            ThreadingMod.Prepare();
            Log.Message("Initialized Threading Mod");
        }

        public static bool Prepare()
        {
            Log.Message("Thread Mod Function Replacement Started");
            FunctionReplacer.ReplacePathfinderFunctions();
            FunctionReplacer.ReplaceReachabilityFunctions();
            FunctionReplacer.ReplaceListerThings();
            Memory.WriteJump(Memory.GetMethodStart(typeof(RCellFinder).GetMethod("RandomWanderDestFor")),
                    Memory.GetMethodStart(typeof(RCellFinderFuncionHolder).GetMethod("newRandomWanderDestFor")));

            Log.Message("Thread Mod Function Replacement Complete");
            return true;
        }

        public static List<CodeInstruction> MethodToCodeInstructions(MethodBase method, ILGenerator generator)
        {
            FileLog.Reset();
            List<CodeInstruction> codeInstructions = new List<CodeInstruction>();
            foreach (ILInstruction Ili in FixedMethodBodyReader.GetInstructions(method, generator))
            {
                CodeInstruction ci = Ili.GetCodeInstruction();
                // Log.Message(ci.ToString());
                codeInstructions.Add(ci);
            }
            return codeInstructions;
        }
    }
}
