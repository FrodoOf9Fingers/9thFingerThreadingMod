using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace _9thFingerThreadingMod.Patches
{
    [HarmonyPatch(typeof(PawnUtility), "RecoverFromUnwalkablePositionOrKill")]
    class PawnUtilityPatch
    {
        [HarmonyPrefix]
        public static bool RecoverFromUnwalkablePositionOrKill(IntVec3 c, Map map)
        {
            List<Thing> tmpThings = new List<Thing>();
            if (!c.InBounds(map) || c.Walkable(map))
            {
                return false;
            }
            tmpThings.AddRange(c.GetThingList(map));
            for (int i = 0; i < tmpThings.Count; i++)
            {
                Pawn pawn = tmpThings[i] as Pawn;
                if (pawn != null)
                {
                    IntVec3 position;
                    if (CellFinder.TryFindBestPawnStandCell(pawn, out position, false))
                    {
                        pawn.Position = position;
                        pawn.Notify_Teleported(true);
                    }
                    else
                    {
                        DamageDef crush = DamageDefOf.Crush;
                        int amount = 99999;
                        BodyPartRecord brain = pawn.health.hediffSet.GetBrain();
                        DamageInfo damageInfo = new DamageInfo(crush, amount, -1f, null, brain, null, DamageInfo.SourceCategory.Collapse);
                        pawn.TakeDamage(damageInfo);
                        if (!pawn.Dead)
                        {
                            pawn.Kill(new DamageInfo?(damageInfo), null);
                        }
                    }
                }
            }
            return false;
        }
    }
}
