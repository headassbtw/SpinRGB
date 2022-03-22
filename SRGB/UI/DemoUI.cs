using System;
using SpinCore.Handlers.UI;

namespace SRGB.UI;

public class DemoUI
{
    private static CustomSpinTab OverviewTab;
    public static void CreateUI()
    {
        SpinCoreMenu.OnCreateTabs = (Action) Delegate.Combine(SpinCoreMenu.OnCreateTabs, new Action(delegate
        {
            DemoUI.OverviewTab = SpinCoreMenu.ModMenu.CreateSpinTab("OpenRGB", true);
            CustomMenuText customMenuText = new CustomMenuText("OpenRGB Overview", DemoUI.OverviewTab);
        }));
    }
}