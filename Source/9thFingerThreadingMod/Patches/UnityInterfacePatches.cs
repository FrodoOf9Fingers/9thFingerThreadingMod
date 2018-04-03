using _9thFingerThreadingMod.Utilities;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace _9thFingerThreadingMod.Patches
{
    class UnityInterfacePatches
    {
        [HarmonyPatch(typeof(MaterialPool), "MatFrom", new Type[] { typeof(Verse.MaterialRequest) })]
        static class MatFromPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params)
            {
                return UpdateLoopBlockers.forceMainThreadPrePatch(__methodId, __params);
            }
        }


        [HarmonyPatch(typeof(ContentFinder<AudioClip>), "Get", new Type[] { typeof(System.String), typeof(bool) })]
        class GetAudioClipPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, ref object __result, object[] __params)
            {
                return UpdateLoopBlockers.forceMainThreadPrePatch(__methodId, ref __result, __params);
            }
        }

        [HarmonyPatch(typeof(AudioSource), "Play", new Type[] { })]
        class AudioSourcePatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, object[] __params, object __instance)
            {
                return UpdateLoopBlockers.forceMainThreadPrePatch(__methodId, __params, __instance);
            }
        }


        [HarmonyPatch(typeof(ContentFinder<Texture2D>), "Get", new Type[] { typeof(System.String), typeof(bool) })]
        class GetTexture2DPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, ref object __result, object[] __params)
            {
                return UpdateLoopBlockers.forceMainThreadPrePatch(__methodId, ref __result, __params);
            }
        }

        /*[HarmonyPatch(typeof(ContentFinder<Texture2D>), "GetAllInFolder", new Type[] { typeof(System.String) })]
        class GetAllTexture2DPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, ref object __result, object[] __params)
            {
                return forceMainThreadPrePatch(__methodId, ref __result, __params);
            }
        }

        [HarmonyPatch(typeof(ContentFinder<AudioClip>), "GetAllInFolder", new Type[] { typeof(System.String) })]
        class GetAllAudioClipPatch
        {
            [HarmonyPrefix]
            public static bool prefix(int __methodId, ref object __result, object[] __params)
            {
                return forceMainThreadPrePatch(__methodId, ref __result, __params);
            }
        }*/

            /*
        //DataAnalysisLogger, is an internal class
        //method = typeof(DataAnalysisLogger).GetMethod("DoLog_LoadedAssets");
        //if (method == null) { throw new Exception("Method is null!"); }
        //harmony.Patch(method, new HarmonyMethod(prefixMethod), null);

        //DialogDatabase No ret val, probably not done during game loop
        //method = typeof(DialogDatabase).GetMethod("LoadAllDialog", BindingFlags.Static | BindingFlags.NonPublic);
        //harmony.Patch(method, new HarmonyMethod(prefixMethod), null);

        //DirectXMLLoader: Function is typed, research what types are used.
        //method = typeof(DirectXmlLoader).GetMethod("LoadXmlDataInResourcesFolder");
        //harmony.Patch(method, new HarmonyMethod(prefixMethod), null);

        //GenFile
        method = typeof(GenFile).GetMethod("TextFromResourceFile", new Type[] { typeof(string)});
        harmony.Patch(method, new HarmonyMethod(prefixMethod), null);
            
        //GraphicDatabaseUtility
        method = typeof(GraphicDatabaseUtility).GetMethod("GraphicNamesInFolder", new Type[] { typeof(System.String)});
        harmony.Patch(method, new HarmonyMethod(prefixMethod), null);
            
        //MaterialLoader
        method = typeof(MaterialLoader).GetMethod("MatsFromTexturesInFolder", new Type[] { typeof(System.String) });
        harmony.Patch(method, new HarmonyMethod(prefixMethod), null);
            
        //MatLoader
        method = typeof(MatLoader).GetMethod("LoadMat", new Type[] { typeof(System.String), typeof(int) });
        harmony.Patch(method, new HarmonyMethod(prefixMethod), null);
            
        //ShaderDatabase
        method = typeof(ShaderDatabase).GetMethod("LoadShader", new Type[] { typeof(System.String) });
        harmony.Patch(method, new HarmonyMethod(prefixMethod), null);*/
    }
}

