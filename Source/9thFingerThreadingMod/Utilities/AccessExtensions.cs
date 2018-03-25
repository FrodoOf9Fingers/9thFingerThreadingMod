using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Utilities
{
    static class AccessExtensions
    {
        //Because Masochism
        public static object call(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }

        public static void SetField(this object o, string memberName, object value)
        {
            var mi = o.GetType().GetProperty(memberName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                mi.SetValue(o, value, null);
            }
        }

        public static object GetField(this object o, string memberName)
        {
            var mi = o.GetType().GetField(memberName, Harmony.AccessTools.all);

            if (mi != null)
            {
                return mi.GetValue(o);
            }
            return null;
        }
    }
}
