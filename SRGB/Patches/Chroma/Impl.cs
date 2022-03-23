using System.Collections;
using HarmonyLib;
using SRGB.Backend;
using UnityEngine;

namespace SRGB.Patches.Chroma;


public class Impl //for actual lighting changes
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
    static bool SetAmbient(Color color,ref float ___lastSetAmbient)
    {
        
        if (Time.unscaledTime < ___lastSetAmbient + 0.033333335f)
        {
            return false;
        }

        ___lastSetAmbient = Time.unscaledTime;
        
        Color[] Colors = new Color[Plugin.RClientInterface.KeyboardLEDs];

        Plugin.RClientInterface.SetKeyboard(color);
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),"GetColors")]
    [HarmonyPrefix]
    static bool GetColors(ref int[] __result)
    {
        Plugin.Log.LogDebug("Colors getting got");
        int[] array = new int[Plugin.RClientInterface.KeyboardLEDs];
        
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        __result = array;
        return false;
    }

    

    static IEnumerator Fade(Color startColor, Color endColor, int frames, float frameDuration, bool looping = false)
    {
        for (int i = 0; i < frames; i++)
        {
            Plugin.RClientInterface.SetKeyboard(Color.Lerp(startColor, endColor, ((float)i / (float)frames)));
            yield return new WaitForSecondsRealtime(frameDuration);
        }
    }
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.PlayRgb))]
    [HarmonyPrefix]
    static bool RgbFade(string name, Color startColor, Color endColor, int frames, float frameDuration, bool looping = false)
    {
        Plugin.Log.LogDebug("Hitting the fade");
        FaderManager.fader.StartCoroutine(Fade(startColor,endColor,frames,frameDuration,looping));
        return false;
    }
}