using _9thFingerThreadingMod.Replacement_Objects;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Replacement_Functions
{
    static class ListerThingsFunctionHolder
    {

        private static newListerThings newInstance(ListerThings old)
        {
            newListerThings obj = new newListerThings(old.use);
            ExpiringObjectMapper.AddReference(old, obj);
            return obj;
        }

        public static bool hijackEverListable(ThingDef def, ListerThingsUse use)
        {
            try
            {
                return newListerThings.EverListable(def, use);
            }
            catch (Exception e)
            {
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static List<Thing> hijackAllThings(this ListerThings oldListerThings)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    var retval = newObj.AllThings;
                    return retval;
                }
            }
            catch (Exception e)
            {
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static void hijackRemove(this ListerThings oldListerThings, Thing t)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    newObj.Remove(t);
                }
            }
            catch (Exception e)
            {
                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static void hijackAdd(this ListerThings oldListerThings, Thing t)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    newObj.Add(t);
                }
            }
            catch (Exception e)
            {

                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static bool hijackContains(this ListerThings oldListerThings, Thing t)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    var retval = newObj.Contains(t);
                    return retval;
                }
            }
            catch (Exception e)
            {

                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static List<Thing> hijackThingsMatching(this ListerThings oldListerThings, ThingRequest req)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    var retval = newObj.ThingsMatching(req);
                    return retval;
                }

            }
            catch (Exception e)
            {

                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static List<Thing> hijackThingsOfDef(this ListerThings oldListerThings, ThingDef def)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    var retval = newObj.ThingsOfDef(def);
                    return retval;
                }

            }
            catch (Exception e)
            {

                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }

        public static List<Thing> hijackThingsInGroup(this ListerThings oldListerThings, ThingRequestGroup group)
        {
            newListerThings newObj = null;
            try
            {
                newObj = (newListerThings)ExpiringObjectMapper.GetReplacement(oldListerThings);
                if (newObj == null)
                    newObj = newInstance(oldListerThings);
                if (newObj == null)
                    throw new ArgumentException();
                lock (newObj)
                {
                    var retval = newObj.ThingsInGroup(group);
                    return retval;
                }

            }
            catch (Exception e)
            {

                Harmony.FileLog.Log(e.ToString());
                throw e;
            }
        }
    }
}
