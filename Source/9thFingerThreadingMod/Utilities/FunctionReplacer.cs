using _9thFingerThreadingMod.Replacement_Functions;
using _9thFingerThreadingMod.Replacement_Objects;
using Harmony.ILCopying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod
{
    static class FunctionReplacer
    {

        public static void autoReplace(Type from, Type to, BindingFlags flags)
        {
            List<MethodBase> fromMethods;
            List<MethodBase> toMethods;

            if (flags != BindingFlags.Default)
            {
                fromMethods = from.GetMethods(flags).ToList<MethodBase>();
                toMethods = to.GetMethods(flags).ToList<MethodBase>();

                fromMethods.AddRange(from.GetConstructors(flags).ToList<MethodBase>());
                toMethods.AddRange(to.GetConstructors(flags).ToList<MethodBase>());
            }
            else
            {
                fromMethods = from.GetMethods().ToList<MethodBase>();
                toMethods = to.GetMethods().ToList<MethodBase>();

                fromMethods.AddRange(from.GetConstructors().ToList<MethodBase>());
                toMethods.AddRange(to.GetConstructors().ToList<MethodBase>());
            }

            foreach (MethodBase fromMethod in fromMethods)
            {
                List<ParameterInfo> setA = fromMethod.GetParameters().ToList();
                foreach (MethodBase toMethod in toMethods)
                {
                    List<ParameterInfo> setB = toMethod.GetParameters().ToList();
                    if (fromMethod.Name == toMethod.Name && compareParameterInfoArrays(setA, setB))
                    {
                        Memory.WriteJump(Memory.GetMethodStart(fromMethod), Memory.GetMethodStart(toMethod));
                    }
                }
            }
        }

        private static bool compareParameterInfoArrays(List<ParameterInfo> setA, List<ParameterInfo> setB)
        {
            foreach (ParameterInfo item in setA)
            {
                if (!setB.Contains(item, new CustomeParameterInfoComparer()))
                {
                    return false;
                }
            }
            return true;
        }

        private class CustomeParameterInfoComparer : IEqualityComparer<ParameterInfo>
        {
            bool IEqualityComparer<ParameterInfo>.Equals(ParameterInfo x, ParameterInfo y)
            {
                return x.ParameterType == y.ParameterType && 
                    x.Position == y.Position;
            }

            int IEqualityComparer<ParameterInfo>.GetHashCode(ParameterInfo obj)
            {
                return obj.GetHashCode();
            }
        }

        public static void replaceRegionTraverser()
        {
            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionTraverser).GetMethod("BreadthFirstTraverse", new Type[] { typeof(Region), typeof(RegionEntryPredicate), typeof(RegionProcessor), typeof(int), typeof(RegionType) })),
                    Memory.GetMethodStart(typeof(newRegionTraverser).GetMethod("BreadthFirstTraverse", new Type[] { typeof(Region), typeof(RegionEntryPredicate), typeof(RegionProcessor), typeof(int), typeof(RegionType) })));

            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionTraverser).GetMethod("BreadthFirstTraverse", new Type[] { typeof(IntVec3), typeof(Map), typeof(RegionEntryPredicate), typeof(RegionProcessor), typeof(int), typeof(RegionType) })),
                    Memory.GetMethodStart(typeof(newRegionTraverser).GetMethod("BreadthFirstTraverse", new Type[] { typeof(IntVec3), typeof(Map), typeof(RegionEntryPredicate), typeof(RegionProcessor), typeof(int), typeof(RegionType) })));

            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionTraverser).GetMethod("MarkRegionsBFS")),
                    Memory.GetMethodStart(typeof(newRegionTraverser).GetMethod("MarkRegionsBFS")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionTraverser).GetMethod("WithinRegions")),
                    Memory.GetMethodStart(typeof(newRegionTraverser).GetMethod("WithinRegions")));
            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionTraverser).GetMethod("FloodAndSetNewRegionIndex")),
                    Memory.GetMethodStart(typeof(newRegionTraverser).GetMethod("FloodAndSetNewRegionIndex")));
            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionTraverser).GetMethod("FloodAndSetRooms")),
                    Memory.GetMethodStart(typeof(newRegionTraverser).GetMethod("FloodAndSetRooms")));

        }


        public static void ReplaceRegionListers()
        {
            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionListersUpdater).GetMethod("DeregisterInRegions", new Type[] { typeof(Thing), typeof(Map) })),
                    Memory.GetMethodStart(typeof(RegionListersUpdaterFunctionHolder).GetMethod("hijackDeregisterInRegions")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionListersUpdater).GetMethod("RegisterInRegions", new Type[] { typeof(Thing), typeof(Map) })),
        Memory.GetMethodStart(typeof(RegionListersUpdaterFunctionHolder).GetMethod("hijackRegisterInRegions")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionListersUpdater).GetMethod("RegisterAllAt", new Type[] { typeof(IntVec3), typeof(Map), typeof(HashSet<Thing>) })),
        Memory.GetMethodStart(typeof(RegionListersUpdaterFunctionHolder).GetMethod("hijackRegisterAllAt")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(RegionListersUpdater).GetMethod("GetTouchableRegions", new Type[] { typeof(Thing), typeof(Map), typeof(List<Region>), typeof(bool) })),
        Memory.GetMethodStart(typeof(RegionListersUpdaterFunctionHolder).GetMethod("hijackGetTouchableRegions")));
        }

        public static void ReplaceListerThings()
        {
            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("EverListable", new Type[] { typeof(ThingDef), typeof(ListerThingsUse) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackEverListable")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("Remove", new Type[] { typeof(Thing) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackRemove")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("Add", new Type[] { typeof(Thing) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackAdd")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("Contains", new Type[] { typeof(Thing) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackContains")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("ThingsMatching", new Type[] { typeof(ThingRequest) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackThingsMatching")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("ThingsOfDef", new Type[] { typeof(ThingDef) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackThingsOfDef")));

            Memory.WriteJump(Memory.GetMethodStart(typeof(ListerThings).GetMethod("ThingsInGroup", new Type[] { typeof(ThingRequestGroup) })),
                                Memory.GetMethodStart(typeof(ListerThingsFunctionHolder).GetMethod("hijackThingsInGroup")));


        }

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
