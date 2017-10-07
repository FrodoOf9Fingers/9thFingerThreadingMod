using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod.Replacement_Objects
{
    public static class newRegionListersUpdater
    {
        private static List<Region> tmpRegions = new List<Region>();

        public static void DeregisterInRegions(Thing thing, Map map)
        {
            ThingDef def = thing.def;
            if (!ListerThings.EverListable(def, ListerThingsUse.Region))
            {
                return;
            }
            newRegionListersUpdater.GetTouchableRegions(thing, map, newRegionListersUpdater.tmpRegions, true);
            for (int i = 0; i < newRegionListersUpdater.tmpRegions.Count; i++)
            {
                ListerThings listerThings = newRegionListersUpdater.tmpRegions[i].ListerThings;
                if (listerThings.Contains(thing))
                {
                    listerThings.Remove(thing);
                }
            }
            newRegionListersUpdater.tmpRegions.Clear();
        }

        public static void RegisterInRegions(Thing thing, Map map)
        {
            ThingDef def = thing.def;
            if (!ListerThings.EverListable(def, ListerThingsUse.Region))
            {
                return;
            }
            newRegionListersUpdater.GetTouchableRegions(thing, map, newRegionListersUpdater.tmpRegions, false);
            for (int i = 0; i < newRegionListersUpdater.tmpRegions.Count; i++)
            {
                ListerThings listerThings = newRegionListersUpdater.tmpRegions[i].ListerThings;
                if (!listerThings.Contains(thing))
                {
                    listerThings.Add(thing);
                }
            }
        }

        public static void RegisterAllAt(IntVec3 c, Map map, HashSet<Thing> processedThings = null)
        {
            List<Thing> thingList = c.GetThingList(map);
            int count = thingList.Count;
            for (int i = 0; i < count; i++)
            {
                Thing thing = thingList[i];
                if (processedThings == null || processedThings.Add(thing))
                {
                    newRegionListersUpdater.RegisterInRegions(thing, map);
                }
            }
        }

        public static void GetTouchableRegions(Thing thing, Map map, List<Region> outRegions, bool allowAdjacentEvenIfCantTouch = false)
        {
            outRegions.Clear();
            CellRect cellRect = thing.OccupiedRect();
            CellRect cellRect2 = cellRect;
            if (newRegionListersUpdater.CanRegisterInAdjacentRegions(thing))
            {
                cellRect2 = cellRect2.ExpandedBy(1);
            }
            CellRect.CellRectIterator iterator = cellRect2.GetIterator();
            while (!iterator.Done())
            {
                IntVec3 current = iterator.Current;
                if (current.InBounds(map))
                {
                    Region validRegionAt_NoRebuild = map.regionGrid.GetValidRegionAt_NoRebuild(current);
                    if (validRegionAt_NoRebuild != null && validRegionAt_NoRebuild.type.Passable() && !outRegions.Contains(validRegionAt_NoRebuild))
                    {
                        if (cellRect.Contains(current))
                        {
                            outRegions.Add(validRegionAt_NoRebuild);
                        }
                        else if (allowAdjacentEvenIfCantTouch || ReachabilityImmediate.CanReachImmediate(current, thing, map, PathEndMode.Touch, null))
                        {
                            outRegions.Add(validRegionAt_NoRebuild);
                        }
                    }
                }
                iterator.MoveNext();
            }
        }

        private static bool CanRegisterInAdjacentRegions(Thing thing)
        {
            return true;
        }
    }
}
