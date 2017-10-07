using _9thFingerThreadingMod.Replacement_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Replacement_Functions
{
    class RegionListersUpdaterFunctionHolder
    {
        private static object block = new object();

        public static void hijackDeregisterInRegions(Thing thing, Map map)
        {
            Blocker.Block(block);
            try
            {
                newRegionListersUpdater.DeregisterInRegions(thing, map);
            }
            catch (Exception e)
            {
                Blocker.Unblock(block);
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
            Blocker.Unblock(block);
        }

        public static void hijackRegisterInRegions(Thing thing, Map map)
        {
            try
            {
                Blocker.Block(block);
                newRegionListersUpdater.RegisterInRegions(thing, map);
            }
            catch (Exception e)
            {
                Blocker.Unblock(block);
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
            Blocker.Unblock(block);
        }

        public static void hijackRegisterAllAt(IntVec3 c, Map map, HashSet<Thing> processedThings = null)
        {
            Blocker.Block(block);
            try
            {
                newRegionListersUpdater.RegisterAllAt(c, map, processedThings);
            }
            catch (Exception e)
            {
                Blocker.Unblock(block);
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
            Blocker.Unblock(block);
        }

        public static void hijackGetTouchableRegions(Thing thing, Map map, List<Region> outRegions, bool allowAdjacentEvenIfCantTouch = false)
        {
            Blocker.Block(block);
            try
            {
                newRegionListersUpdater.GetTouchableRegions(thing, map, outRegions, allowAdjacentEvenIfCantTouch);
            }
            catch (Exception e)
            {
                Blocker.Unblock(block);
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
            Blocker.Unblock(block);
        }
    }
}
