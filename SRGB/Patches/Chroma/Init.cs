using System;
using HarmonyLib;
using OpenRGB.NET;
using SRGB.TypeConversion;
using UnityEngine;

namespace SRGB.Patches.Chroma;

public class Init //Init stuff
{
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.InitService))]
    [HarmonyPrefix]
    static bool PauseChamp(ref bool __result)
    {
        Plugin.Log.LogInfo($"Razer Chroma Intercepted");
        __result = Plugin.RClientInterface.Init();
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.ShutdownService))]
    [HarmonyPrefix]
    static bool ShutChamp()
    {
        Plugin.Log.LogInfo($"Razer Chroma shut down");
        return false;
    }
}