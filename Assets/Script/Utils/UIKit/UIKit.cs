public class UIKit
{
    private static UIMgr _mgr;

    public static void Init()
    {
        _mgr = UIRoot.Instance.Manager;
    }

    public static void OpenPanel(string name, UIData data = null, UILevel level = UILevel.Common)
    {
        _mgr.OpenPanelAsync(name, data, level);
    }

    public static void ClosePanel()
    {
        _mgr.ClosePanel();
    }

    public static IBasePanel GetCurrentPanel()
    {
        return _mgr.GetCurrentPanel();
    }

    public static void CloseAllPanel(bool isClearHidePanel = false)
    {
        _mgr.CloseAllPanel(isClearHidePanel);
    }
}