using HarmonyLib;

namespace SRGB.Patches.Chroma;

public class Stubs //for funcs that i don't want to run, or don't have an OpenRGB translation
{
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.PlayRgbAnimation))]
    [HarmonyPrefix]
    static bool StubPlayAnim()
    {
            Plugin.Log.LogDebug("PlayRgbAnimation Stubbed");
        return false;
    }
    
    
    
    
    
    
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.StopRgbAnimation))]
    [HarmonyPrefix]
    static bool StubStop()
    {
        Plugin.Log.LogDebug("StopRgbAnimation Stubbed");
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.StopAllAnimations))]
    [HarmonyPrefix]
    static bool StubStopAll()
    {
        Plugin.Log.LogDebug("StopAllAnimations Stubbed");
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),"GetAnimId")]
    [HarmonyPrefix]
    static bool StubGetID()
    {
        Plugin.Log.LogDebug("GetAnimId Stubbed");
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),"UpdateCurrentAnimation")]
    [HarmonyPrefix]
    static bool StubUpdateCurrent()
    {
        Plugin.Log.LogDebug("UpdateCurrentAnimation Stubbed");
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),"PlayBarGraphFrame")]
    [HarmonyPrefix]
    static bool StubPlayBarGraphFrame()
    {
        Plugin.Log.LogDebug("PlayBarGraphFrame Stubbed");
        return false;
    }
}