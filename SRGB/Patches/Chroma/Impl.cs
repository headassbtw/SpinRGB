using HarmonyLib;
using UnityEngine;

namespace SRGB.Patches.Chroma;

public class Impl
{
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
    [HarmonyPatch(typeof(RazerChroma),"GetColors")]
    [HarmonyPrefix]
    static int[] GetColors()
    {
        int[] array = new int[Plugin.RClientInterface.KeyboardLEDs];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = 0;
        }
        
        
        return array;
    }
}