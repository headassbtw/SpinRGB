using SRGB.Patches.Chroma;
using UnityEngine;

namespace SRGB.Backend;

public class Fader : MonoBehavior {}

internal static class FaderManager
{
    internal static Fader fader;
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
                fader = _rgb.AddComponent<Fader>();
            }

            return _rgb;
        }
    }
}
