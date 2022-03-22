using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SpinCore.Handlers.UI;
using SRGB.OpenRGB;
using SRGB.Patches;
using SRGB.UI;
using UnityEngine;

namespace SRGB
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance;
        internal static SRGB.OpenRGB.Client RClientInterface;
        internal static ManualLogSource Log;
        private void Awake()
        {
            
            
            
            Instance = this;
            Log = base.Logger;
            RClientInterface = new Client("Spin Rhythm XD");
            var harmony = new Harmony("SRGB");
            harmony.PatchAll(typeof(Patches.Chroma.Stubs));
            harmony.PatchAll(typeof(Patches.Chroma.Init));
            DemoUI.CreateUI();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
