using Harmony;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace _9thFingerThreadingMod
{
    [HarmonyPatch(typeof(TickManager), "DoSingleTick")]
    public static class SingleTickPatch
    {
        [HarmonyPrefix]
        public static bool DoSingleTick(TickManager __instance)
        {
            DoSingleTickDetour(__instance);
            return false;
        }

        public static void DoSingleTickDetour(TickManager __instance)
        {
            FieldInfo gameTicks = typeof(TickManager).GetField("ticksGameInt", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo nTicks = typeof(TickManager).GetField("tickListNormal", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo rTicks = typeof(TickManager).GetField("tickListRare", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo lTicks = typeof(TickManager).GetField("tickListLong", BindingFlags.NonPublic | BindingFlags.Instance);

            List<Map> maps = Find.Maps;
            Parallel.For(0, maps.Count, i => { maps[i].MapPreTick(); });

            if (!DebugSettings.fastEcology)
            {
                gameTicks.SetValue(__instance, (int)gameTicks.GetValue(__instance) + 1);
            }
            else
            {
                gameTicks.SetValue(__instance, (int)gameTicks.GetValue(__instance) + 250);
            }
            Shader.SetGlobalFloat(ShaderPropertyIDs.GameSeconds, ((int)gameTicks.GetValue(__instance)).TicksToSeconds());

            ((TickList)nTicks.GetValue(__instance)).Tick();
            ((TickList)rTicks.GetValue(__instance)).Tick();
            ((TickList)lTicks.GetValue(__instance)).Tick();

            try
            {
                Find.DateNotifier.DateNotifierTick();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            try
            {
                Find.Scenario.TickScenario();
            }
            catch (Exception ex2)
            {
                Log.Error(ex2.ToString());
            }
            try
            {
                Find.World.WorldTick();
            }
            catch (Exception ex3)
            {
                Log.Error(ex3.ToString());
            }
            try
            {
                Find.StoryWatcher.StoryWatcherTick();
            }
            catch (Exception ex4)
            {
                Log.Error(ex4.ToString());
            }
            try
            {
                Find.GameEnder.GameEndTick();
            }
            catch (Exception ex5)
            {
                Log.Error(ex5.ToString());
            }
            try
            {
                Find.Storyteller.StorytellerTick();
            }
            catch (Exception ex6)
            {
                Log.Error(ex6.ToString());
            }
            try
            {
                Current.Game.taleManager.TaleManagerTick();
            }
            catch (Exception ex7)
            {
                Log.Error(ex7.ToString());
            }
            try
            {
                Find.World.WorldPostTick();
            }
            catch (Exception ex8)
            {
                Log.Error(ex8.ToString());
            }
            for (int j = 0; j < maps.Count; j++)
            {
                maps[j].MapPostTick();
            }
            try
            {
                Find.History.HistoryTick();
            }
            catch (Exception ex9)
            {
                Log.Error(ex9.ToString());
            }
            GameComponentUtility.GameComponentTick();
            try
            {
                Find.LetterStack.LetterStackTick();
            }
            catch (Exception ex10)
            {
                Log.Error(ex10.ToString());
            }
            try
            {
                Find.Autosaver.AutosaverTick();
            }
            catch (Exception ex11)
            {
                Log.Error(ex11.ToString());
            }
            Debug.developerConsoleVisible = false;
        }
    }
}
