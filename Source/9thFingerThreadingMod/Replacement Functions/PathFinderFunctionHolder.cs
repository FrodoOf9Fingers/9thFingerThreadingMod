using System.Reflection;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod
{
    static class PathFinderFunctionHolder
    {
        public static PawnPath NewFindPath(this PathFinder pf, IntVec3 start, LocalTargetInfo dest, TraverseParms traverseParms, PathEndMode peMode = PathEndMode.OnCell)
        {
            FieldInfo mapField = typeof(PathFinder).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance);
            Map map = (Map)mapField.GetValue(pf);
            string index = map.GetUniqueLoadID();

            int ticket = -1;
            newPathFinder finder = PathFinderInstanceContainer.GetInstance().requestFinder(index, ref ticket, map);
            var val = finder.FindPath(start, dest, traverseParms, peMode);
            PathFinderInstanceContainer.GetInstance().CheckInFinder(index, ticket);
            try
            {
                var someVal = val.LastNode;
            }
            catch (System.Exception)
            {
                Log.Message("Tried to return a path without an end.... ???");
                return PawnPath.NotFound;
            }
            return val;
        }
    }
}