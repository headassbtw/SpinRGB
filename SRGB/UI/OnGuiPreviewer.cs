using System;
using UnityEngine;

namespace SRGB.UI;

public class OnGuiPreviewer
{
    private static GameObject obj;
    private static RGBGUI UI;
    internal static void Init()
    {
        Plugin.Log.LogInfo("Spawning GUI");
        if (obj == null)
        {
            obj = new GameObject("RGB_GUI");
        }

        if (UI == null)
        {
            UI = obj.AddComponent<RGBGUI>();
        }
    }
}

public class RGBGUI : MonoBehaviour
{
    internal static Color[] cols;
    private Rect windowRect = new Rect (20, 20, 120, 80);
    
    void OnGUI ()
    {
        GUI.Label(Rect.zero, "Hi");
        windowRect = GUI.Window (0, windowRect, WindowFunction, "My Window");
        
        for (int i = 0; i < Plugin.RClientInterface.KeyboardHeight; i++)
        {
            
            for (int j = 0; j < Plugin.RClientInterface.KeyboardWidth; j++)
            {
                Rect a = new Rect(j*10,i*10,10,10);
                GUI.backgroundColor = cols[(i * Plugin.RClientInterface.KeyboardWidth.Value) + j];
                GUI.Button(a,"A");

            }   
        }
    }
    
    void WindowFunction (int windowID) 
    {
        GUI.Label(Rect.zero, "Hi");
    }
}