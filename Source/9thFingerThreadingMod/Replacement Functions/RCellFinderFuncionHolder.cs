using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod.Replacement_Functions
{
    class RCellFinderFuncionHolder
    {
        private static EventWaitHandle blocker = new EventWaitHandle(true, EventResetMode.AutoReset);
        public static IntVec3 newRandomWanderDestFor(Pawn pawn, IntVec3 root, float radius, Func<Pawn, IntVec3, bool> validator, Danger maxDanger)
        {
            blocker.WaitOne();
            if (radius > 12f)
            {
                Log.Warning(string.Concat(new object[]
                {
                    "wanderRadius of ",
                    radius,
                    " is greater than Region.GridSize of ",
                    12,
                    " and will break."
                }));
            }
            if (root.GetRegion(pawn.Map, RegionType.Set_Passable) == null)
            {
                blocker.Set();
                return root;
            }
            int maxRegions = Mathf.Max((int)radius / 3, 13);
            CellFinder.AllRegionsNear(((List<Region>)AccessTools.Field(typeof(RCellFinder), "regions").GetValue(null)), root.GetRegion(pawn.Map, RegionType.Set_Passable),
                maxRegions, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false),
                (Region reg) => reg.extentsClose.ClosestDistSquaredTo(root) <= radius * radius, null,
                RegionType.Set_Passable);

            bool flag = UnityData.isDebugBuild && DebugViewSettings.drawDestSearch;
            if (flag)
            {
                pawn.Map.debugDrawer.FlashCell(root, 0.6f, "root");
            }
            if (((List<Region>)AccessTools.Field(typeof(RCellFinder), "regions").GetValue(null)).Count > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    IntVec3 randomCell = ((List<Region>)AccessTools.Field(typeof(RCellFinder), "regions").GetValue(null)).RandomElementByWeightWithFallback((Region reg) => (float)reg.CellCount, null).RandomCell;
                    if ((float)randomCell.DistanceToSquared(root) > radius * radius)
                    {
                        if (flag)
                        {
                            pawn.Map.debugDrawer.FlashCell(randomCell, 0.32f, "distance");
                        }
                    }
                    else
                    {
                        if ((bool)AccessTools.Method(typeof(RCellFinder), "CanWanderToCell").Invoke(null, new object[] { randomCell, pawn, root, validator, i, maxDanger }))
                        {
                            if (flag)
                            {
                                pawn.Map.debugDrawer.FlashCell(randomCell, 0.9f, "go!");
                            }
                            blocker.Set();
                            return randomCell;
                        }
                        if (flag)
                        {
                            pawn.Map.debugDrawer.FlashCell(randomCell, 0.6f, "validation");
                        }
                    }
                }
            }
            IntVec3 position;
            if (!CellFinder.TryFindRandomCellNear(root, pawn.Map, 20,
                (IntVec3 c) => c.InBounds(pawn.Map) &&
                pawn.CanReach(c, PathEndMode.OnCell, Danger.None, false, TraverseMode.ByPawn) &&
                !c.IsForbidden(pawn), out position) &&
                !CellFinder.TryFindRandomCellNear(root, pawn.Map, 30, (IntVec3 c) => c.InBounds(pawn.Map) &&
                pawn.CanReach(c, PathEndMode.OnCell, Danger.Deadly, false, TraverseMode.ByPawn), out position) &&
                !CellFinder.TryFindRandomCellNear(pawn.Position, pawn.Map, 5, (IntVec3 c) => c.InBounds(pawn.Map) &&
                pawn.CanReach(c, PathEndMode.OnCell, Danger.Deadly, false, TraverseMode.ByPawn), out position))

            {
                position = pawn.Position;
            }
            if (flag)
            {
                pawn.Map.debugDrawer.FlashCell(position, 0.4f, "fallback");
            }
            blocker.Set();
            return position;
        }
    }
}
