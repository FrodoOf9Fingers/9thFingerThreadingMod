using _9thFingerThreadingMod.Patches;
using _9thFingerThreadingMod.Replacement_Functions;
using Harmony;
using Harmony.ILCopying;
using HugsLib;
using RimWorld;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using Verse.AI;
using System.Runtime.CompilerServices;

namespace _9thFingerThreadingMod
{
    public class ThreadingMod : ModBase
    {
        public const int NUM_THREADS_PER_MAP = 8;
        public override string ModIdentifier => "threadingmod";
        protected override bool HarmonyAutoPatch { get { return false; } }
        HarmonyInstance harmony;
        public static int mainThreadId;

        ThreadingMod()
        {
            mainThreadId = (int)typeof(UnityData).GetField("mainThreadId", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            harmony = HarmonyInstance.Create("9thFingerThreadingMod");
            Prepare();
            doManualPatches();
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void DefsLoaded()
        {
            ConstructorInfo constructor = typeof(RegionTraverser).GetConstructor(BindingFlags.Static | BindingFlags.NonPublic, null, new Type[0], null);
            constructor.Invoke(null, null);
            var obj = typeof(RegionTraverser).GetField("freeWorkers", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
        }
        
        public bool Prepare()
        {
            Log.Message("Thread Mod Function Replacement Started");
            FunctionReplacer.ReplacePathfinderFunctions();
            FunctionReplacer.ReplaceReachabilityFunctions();
            FunctionReplacer.ReplaceListerThings();
            FunctionReplacer.ReplaceRegionListers();
            FunctionReplacer.replaceRegionTraverser();
            Memory.WriteJump(Memory.GetMethodStart(typeof(RCellFinder).GetMethod("RandomWanderDestFor")),
                    Memory.GetMethodStart(typeof(RCellFinderFuncionHolder).GetMethod("newRandomWanderDestFor")));
            return true;
        }

        public void doManualPatches()
        {

        }
    }
}
