using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace SRGB.Patches.Chroma;

internal class Fader : MonoBehaviour
{
    
}

public class Impl
{
    private static Fader _fader;
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

    private static GameObject _rgb;
    private static GameObject RgbRT
    {
        get
        {
            if (_rgb != null)
            {
                return _rgb;
            }
            else
            {
                _rgb = GameObject.Find("RgbRT");
                _fader = _rgb.AddComponent<Fader>();
            }

            return _rgb;
        }
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
        _fader.StartCoroutine(Fade(startColor,endColor,frames,frameDuration,looping));
        return false;
    }
}