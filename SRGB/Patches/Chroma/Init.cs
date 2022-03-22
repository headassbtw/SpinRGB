using System;
using HarmonyLib;
using OpenRGB.NET;
using SRGB.TypeConversion;
using UnityEngine;

namespace SRGB.Patches.Chroma;

public class Init
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
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.HighlightKey))]
    [HarmonyPrefix]
    static bool HighlightChamp(KeyCode key, Color color)
    {
        Plugin.Log.LogInfo($"Key {key} highlighted with {color}");
        return false;
    }
    
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.SetRgbFromColors))]
    [HarmonyPrefix]
    static bool BigChamp(Color[] pixels, int width)
    {
        Plugin.RClientInterface.SetKeyboard(pixels);
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.SetAmbientLighting))]
    [HarmonyPrefix]
    static bool SetAmbient(Color color,ref float __lastSetAmbient)
    {
        if (Time.unscaledTime < __lastSetAmbient + 0.033333335f)
        {
            return false;
        }

        __lastSetAmbient = Time.unscaledTime;
        
        Color[] Colors = new Color[Plugin.RClientInterface.KeyboardLEDs];

        Plugin.RClientInterface.SetKeyboard(color);
        return false;
    }
}