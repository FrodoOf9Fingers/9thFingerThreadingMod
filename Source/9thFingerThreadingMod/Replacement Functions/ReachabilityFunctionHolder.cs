using RimWorld;
using System;
using System.Reflection;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod.Replacement_Functions
{
    public static class ReachabilityFunctionHolder
    {
        public static void hijackClearCache(this Reachability oldReacher)
        {
            int ticket = -1;
            String index = "";
            try
            {
                FieldInfo mapField = typeof(Reachability).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
                Map map = (Map)mapField.GetValue(oldReacher);
                index = map.GetUniqueLoadID();

                var reacher = ReachabilityInstanceContrainer.GetInstance().requestReacher(index, ref ticket, map);
                reacher.ClearCache();
                ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
            }
            catch (Exception e)
            {
                if (ticket != -1 && !index.NullOrEmpty())
                    ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                throw e;
            }
        }

        public static bool hijackCanReach(this Reachability oldReacher, IntVec3 start, LocalTargetInfo dest, PathEndMode peMode, TraverseParms traverseParams)
        {
            int ticket = -1;
            String index = "";
            try
            {
                FieldInfo mapField = typeof(Reachability).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
                Map map = (Map)mapField.GetValue(oldReacher);
                index = map.GetUniqueLoadID();

                var reacher = ReachabilityInstanceContrainer.GetInstance().requestReacher(index, ref ticket, map);
                var result = reacher.CanReach(start, dest, peMode, traverseParams);
                ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                return result;
            }
            catch (Exception e)
            {
                if (ticket != -1 && !index.NullOrEmpty())
                    ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                throw e;
            }
        }

        public static bool hijackCanReachColony(this Reachability oldReacher, IntVec3 c)
        {
            int ticket = -1;
            String index = "";
            try
            {
                FieldInfo mapField = typeof(Reachability).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
                Map map = (Map)mapField.GetValue(oldReacher);
                index = map.GetUniqueLoadID();

                var reacher = ReachabilityInstanceContrainer.GetInstance().requestReacher(index, ref ticket, map);
                var result = reacher.CanReachColony(c);
                ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                return result;
            }
            catch (Exception e)
            {
                if (ticket != -1 && !index.NullOrEmpty())
                    ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                throw e;
            }
        }
        public static bool hijackCanReachFactionBase(this Reachability oldReacher, IntVec3 c, Faction factionBaseFaction)
        {
            int ticket = -1;
            String index = "";
            try
            {
                FieldInfo mapField = typeof(Reachability).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
                Map map = (Map)mapField.GetValue(oldReacher);
                index = map.GetUniqueLoadID();

                var reacher = ReachabilityInstanceContrainer.GetInstance().requestReacher(index, ref ticket, map);
                var result = reacher.CanReachFactionBase(c, factionBaseFaction);
                ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                return result;
            }
            catch (Exception e)
            {
                if (ticket != -1 && !index.NullOrEmpty())
                    ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                throw e;
            }
        }
        public static bool hijackCanReachMapEdge(this Reachability oldReacher, IntVec3 c, TraverseParms traverseParms)
        {
            int ticket = -1;
            String index = "";
            try
            {
                FieldInfo mapField = typeof(Reachability).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
                Map map = (Map)mapField.GetValue(oldReacher);
                index = map.GetUniqueLoadID();

                var reacher = ReachabilityInstanceContrainer.GetInstance().requestReacher(index, ref ticket, map);
                var result = reacher.CanReachMapEdge(c, traverseParms);
                ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                return result;
            }
            catch (Exception e)
            {
                if (ticket != -1 && !index.NullOrEmpty())
                    ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                throw e;
            }
        }
        public static bool hijackCanReachUnfogged(this Reachability oldReacher, IntVec3 c, TraverseParms traverseParms)
        {
            int ticket = -1;
            String index = "";
            try
            {
                FieldInfo mapField = typeof(Reachability).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
                Map map = (Map)mapField.GetValue(oldReacher);

                var reacher = ReachabilityInstanceContrainer.GetInstance().requestReacher(index, ref ticket, map);
                var result = reacher.CanReachUnfogged(c, traverseParms);
                ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                return result;
            }
            catch (Exception e)
            {
                if (ticket != -1 && !index.NullOrEmpty())
                    ReachabilityInstanceContrainer.GetInstance().CheckInReacher(index, ticket);
                throw e;
            }
        }
    }
}
