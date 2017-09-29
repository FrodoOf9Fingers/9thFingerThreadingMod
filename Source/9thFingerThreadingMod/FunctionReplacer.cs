using _9thFingerThreadingMod.Replacement_Functions;
using Harmony.ILCopying;
using System;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod
{
    static class FunctionReplacer
    {
        public static void ReplaceReachabilityFunctions()
        {
            Memory.WriteJump(Memory.GetMethodStart(typeof(Reachability).GetMethod("ClearCache")),
                    Memory.GetMethodStart(typeof(ReachabilityFunctionHolder).GetMethod("hijackClearCache")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(Reachability).GetMethod("CanReach", new Type[] { typeof(IntVec3), typeof(LocalTargetInfo), typeof(PathEndMode), typeof(TraverseParms) })),
                    Memory.GetMethodStart(typeof(ReachabilityFunctionHolder).GetMethod("hijackCanReach")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(Reachability).GetMethod("CanReachColony")),
                    Memory.GetMethodStart(typeof(ReachabilityFunctionHolder).GetMethod("hijackCanReachColony")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(Reachability).GetMethod("CanReachFactionBase")),
                    Memory.GetMethodStart(typeof(ReachabilityFunctionHolder).GetMethod("hijackCanReachFactionBase")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(Reachability).GetMethod("CanReachMapEdge")),
                    Memory.GetMethodStart(typeof(ReachabilityFunctionHolder).GetMethod("hijackCanReachMapEdge")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(Reachability).GetMethod("CanReachUnfogged")),
                    Memory.GetMethodStart(typeof(ReachabilityFunctionHolder).GetMethod("hijackCanReachUnfogged")));
        }
        public static void ReplacePathfinderFunctions()
        {
            Memory.WriteJump(Memory.GetMethodStart(typeof(PathFinder).GetMethod("FindPath", new Type[] { typeof(IntVec3), typeof(LocalTargetInfo), typeof(TraverseParms), typeof(PathEndMode) })),
                                Memory.GetMethodStart(typeof(PathFinderFunctionHolder).GetMethod("NewFindPath")));
        }
    }
}
