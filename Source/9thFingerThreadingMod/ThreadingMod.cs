using _9thFingerThreadingMod.Replacement_Functions;
using Harmony;
using Harmony.ILCopying;
using HugsLib;
using RimWorld;
using System;
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

        public override void DefsLoaded()
        {
            ConstructorInfo constructor = typeof(RegionTraverser).GetConstructor(BindingFlags.Static | BindingFlags.NonPublic, null, new Type[0], null);
            constructor.Invoke(null, null);

            var obj = typeof(RegionTraverser).GetField("freeWorkers", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            FileLog.Log("Num items: " + obj.GetType().GetProperty("Count").GetValue(obj, null).ToString());
            FileLog.Log("num workers: " + RegionTraverser.NumWorkers.ToString());
        }

        public static bool Prepare()
        {
            Log.Message("Thread Mod Function Replacement Started");
            FunctionReplacer.ReplacePathfinderFunctions();
            FunctionReplacer.ReplaceReachabilityFunctions();
            FunctionReplacer.ReplaceListerThings();
            FunctionReplacer.ReplaceRegionListers();
            FunctionReplacer.replaceRegionTraverser();
            Memory.WriteJump(Memory.GetMethodStart(typeof(RCellFinder).GetMethod("RandomWanderDestFor")),
                    Memory.GetMethodStart(typeof(RCellFinderFuncionHolder).GetMethod("newRandomWanderDestFor")));

            Log.Message("Thread Mod Function Replacement Complete");
            return true;
        }
    }
}
