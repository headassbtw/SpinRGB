using System.Linq;
using OpenRGB.NET;
using OpenRGB.NET.Enums;
using OpenRGB.NET.Models;
using SRGB.TypeConversion;

namespace SRGB.OpenRGB;

public class Client
{
    private OpenRGBClient RGBClient;
    private int KeyboardWidth;
    public int KeyboardLEDs { get; private set; }
    private int KeyboardHeight = 6;
    private bool HasKeyboard;
    private Device _keyboard;
    private int _kbIdx;

    internal Client(string clientName)
    {
        if (RGBClient == null)
            RGBClient = new OpenRGBClient(name: clientName, autoconnect: true, timeout: 1000);
    }

    internal bool SetKeyboard(UnityEngine.Color[] colors)
    {
        if (!HasKeyboard) return false;
        
        var leds = Enumerable.Range(0, _keyboard.Colors.Length)
            .Select(i => colors[i].FromUnity())
            .ToArray();
        RGBClient.UpdateLeds(_kbIdx, leds);
        return true;
    }
    internal bool SetKeyboard(UnityEngine.Color color)
    {
        if (!HasKeyboard) return false;
        Color ORGBCol = color.FromUnity();
        var leds = Enumerable.Range(0, _keyboard.Colors.Length)
            .Select(i => ORGBCol)
            .ToArray();
        RGBClient.UpdateLeds(_kbIdx, leds);
        return true;
    }

    internal bool Init()
    {
        int controllerCount = RGBClient.GetControllerCount();
        var devices = RGBClient.GetAllControllerData();
        if (controllerCount >= 1)
            Plugin.Log.LogInfo("OpenRGB Devices:");
        else
            return false;
        for (int i = 0; i < devices.Length; i++)
        {
            Plugin.Log.LogInfo("");
            Plugin.Log.LogInfo(devices[i].Name);
            Plugin.Log.LogInfo(devices[i].Description);
            Plugin.Log.LogInfo(devices[i].Location);
            
            
            if (devices[i].Type == DeviceType.Keyboard)
            {
                KeyboardWidth = devices[i].Leds.Length / 6;
                KeyboardLEDs = devices[i].Leds.Length;
                _keyboard = devices[i];
                _kbIdx = i;
                HasKeyboard = true;
                break;
            }
        }

        return true;
    }

    internal bool Shutdown()
    {
        if (RGBClient.Connected)
        {
            RGBClient.Dispose();
        }
        return true;
    }
}