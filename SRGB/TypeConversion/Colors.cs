using OpenRGB.NET.Models;

namespace SRGB.TypeConversion;

public static class Colors
{
    public static Color FromUnity(this UnityEngine.Color col)
    {
        Color rtn = new Color((byte) ((int)col.r*255), 
                            (byte) ((int)col.g*255), 
                             (byte) ((int)col.b*255));
        return rtn;
    }
}