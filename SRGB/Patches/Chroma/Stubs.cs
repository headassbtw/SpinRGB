using HarmonyLib;

namespace SRGB.Patches.Chroma;

public class Stubs
{
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.PlayRgbAnimation))]
    [HarmonyPrefix]
    static bool StubPlayAnim()
    {
        return false;
    }
    
    
    
    
    
    
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.StopRgbAnimation))]
    [HarmonyPrefix]
    static bool StubStop()
    {
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),nameof(RazerChroma.StopAllAnimations))]
    [HarmonyPrefix]
    static bool StubStopAll()
    {
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),"GetAnimId")]
    [HarmonyPrefix]
    static bool StubGetID()
    {
        return false;
    }
    [HarmonyPatch(typeof(RazerChroma),"UpdateCurrentAnimation")]
    [HarmonyPrefix]
    static bool StubUpdateCurrent()
    {
        return false;
    }
}