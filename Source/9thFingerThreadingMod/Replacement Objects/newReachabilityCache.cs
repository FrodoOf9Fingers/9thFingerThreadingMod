using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Verse;

namespace _9thFingerThreadingMod.Replacement_Objects
{
    public class newReachabilityCache
    {
        [StructLayout(LayoutKind.Sequential, Size = 1)]
        private struct CachedEntry : IEquatable<newReachabilityCache.CachedEntry>
        {
            public int FirstRoomID
            {
                get;
                private set;
            }

            public int SecondRoomID
            {
                get;
                private set;
            }

            public TraverseParms TraverseParms
            {
                get;
                private set;
            }

            public CachedEntry(int firstRoomID, int secondRoomID, TraverseParms traverseParms)
            {
                if (firstRoomID < secondRoomID)
                {
                    this.FirstRoomID = firstRoomID;
                    this.SecondRoomID = secondRoomID;
                }
                else
                {
                    this.FirstRoomID = secondRoomID;
                    this.SecondRoomID = firstRoomID;
                }
                this.TraverseParms = traverseParms;
            }

            public override bool Equals(object obj)
            {
                return obj is newReachabilityCache.CachedEntry && this.Equals((newReachabilityCache.CachedEntry)obj);
            }

            public bool Equals(newReachabilityCache.CachedEntry other)
            {
                return this.FirstRoomID == other.FirstRoomID && this.SecondRoomID == other.SecondRoomID && this.TraverseParms == other.TraverseParms;
            }

            public override int GetHashCode()
            {
                int seed = Gen.HashCombineInt(this.FirstRoomID, this.SecondRoomID);
                return Gen.HashCombineStruct<TraverseParms>(seed, this.TraverseParms);
            }

            public static bool operator ==(newReachabilityCache.CachedEntry lhs, newReachabilityCache.CachedEntry rhs)
            {
                return lhs.Equals(rhs);
            }

            public static bool operator !=(newReachabilityCache.CachedEntry lhs, newReachabilityCache.CachedEntry rhs)
            {
                return !lhs.Equals(rhs);
            }
        }

        private Dictionary<newReachabilityCache.CachedEntry, bool> cacheDict = new Dictionary<newReachabilityCache.CachedEntry, bool>();

        public int Count
        {
            get
            {
                return this.cacheDict.Count;
            }
        }

        public BoolUnknown CachedResultFor(Room A, Room B, TraverseParms traverseParams)
        {
            bool flag;
            if (this.cacheDict.TryGetValue(new newReachabilityCache.CachedEntry(A.ID, B.ID, traverseParams), out flag))
            {
                return (!flag) ? BoolUnknown.False : BoolUnknown.True;
            }
            return BoolUnknown.Unknown;
        }

        public void AddCachedResult(Room A, Room B, TraverseParms traverseParams, bool reachable)
        {
            newReachabilityCache.CachedEntry key = new newReachabilityCache.CachedEntry(A.ID, B.ID, traverseParams);
            if (!this.cacheDict.ContainsKey(key))
            {
                this.cacheDict.Add(key, reachable);
            }
        }

        public void Clear()
        {
            this.cacheDict.Clear();
        }
    }
}
