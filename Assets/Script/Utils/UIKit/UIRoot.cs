using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    public Camera UICamera;
    public Canvas Canvas;
    public CanvasScaler CanvasScaler;
    public GraphicRaycaster GraphicRaycaster;

    public RectTransform Bg;
    public RectTransform Common;
    public RectTransform Pop;

    public UIMgr Manager;

    private static UIRoot instance;

    public static UIRoot Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<UIRoot>();
            }

            if (!instance)
            {
                var obj = Instantiate(Resources.Load<GameObject>("UIRoot"));
                instance = obj.GetComponent<UIRoot>();
                obj.name = "UIRoot";
                DontDestroyOnLoad(obj);
            }

            return instance;
        }
    }

    public Camera Camera
    {
        get { return UICamera; }
    }

    public void SetUICameraActive(bool isActive)
    {
        UICamera.gameObject.SetActive(isActive);
    }

    public void SetResolution(int width, int height, float matchOnWidthOrHeight)
    {
        CanvasScaler.referenceResolution = new Vector2(width, height);
        CanvasScaler.matchWidthOrHeight = matchOnWidthOrHeight;
    }

    public Vector2 GetResolution()
    {
        return CanvasScaler.referenceResolution;
    }

    public float GetMatchOrWidthOrHeight()
    {
        return CanvasScaler.matchWidthOrHeight;
    }

    public void ScreenSpaceOverlayRenderMode()
    {
        Canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
        UICamera.gameObject.SetActive(false);
    }

    public void ScreenSpaceCameraRenderMode()
    {
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        UICamera.gameObject.SetActive(true);
        Canvas.worldCamera = UICamera;
    }

    public void SetLevelOfPanel(UILevel level, IBasePanel panel)
    {
        switch (level)
        {
            case UILevel.Bg:
                panel.Transform.SetParent(Bg);
                break;
            case UILevel.Common:
                panel.Transform.SetParent(Common);
                break;
            case UILevel.Pop:
                panel.Transform.SetParent(Pop);
                break;
        }

        if (panel.Info != null && panel.Info.level != level)
        {
            panel.Info.level = level;
        }
    }
}