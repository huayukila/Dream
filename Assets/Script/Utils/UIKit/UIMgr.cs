using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIMgr : MonoBehaviour
{
    private readonly string Path = "Assets/Prefab/UI/";

    private Stack<IBasePanel> panelStack = new Stack<IBasePanel>();
    private Dictionary<Type, IBasePanel> hidedPanel = new Dictionary<Type, IBasePanel>();

    private IBasePanel currentPanel;

    public void OpenPanelAsync(string name, UIData data = null, UILevel level = UILevel.Common)
    {
        AsyncLoadGameObj(name, obj =>
        {
            var panel = obj.GetComponent<IBasePanel>();
            PushPanel(panel);
            currentPanel = panel;
            currentPanel.Info = new UIInfo()
            {
                level = level,
                panelType = panel.GetType(),
                uiData = data
            };
            currentPanel.State = PanelState.Opening;
            UIRoot.Instance.SetLevelOfPanel(level, currentPanel);
            SetDefaultSizeOfPanel(currentPanel);
            currentPanel.Init();
            currentPanel.Enter();
            currentPanel.Show();
        });
    }

    public IBasePanel GetCurrentPanel()
    {
        return currentPanel;
    }

    public void HidePanel()
    {
        var panel = panelStack.Pop();
        if (panelStack.Count > 0)
        {
            panelStack.Peek().Resume();
        }

        panel.Hide();
        hidedPanel.Add(panel.Info.panelType, panel);
    }

    public T ShowPanel<T>() where T : class, IBasePanel
    {
        hidedPanel.TryGetValue(typeof(T), out var panel);
        var originalPanel = panel as T;
        return originalPanel;
    }

    public void ClosePanel()
    {
        var panel = PopPanel();
        panel.Exit();
        if (panelStack.Count > 0)
        {
            currentPanel = panelStack.Peek();
            currentPanel.Resume();
        }
        Destroy(panel.Transform.gameObject);
    }

    void PushPanel(IBasePanel panel)
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().Pause();
        }

        panelStack.Push(panel);
    }

    IBasePanel PopPanel()
    {
        return panelStack.Pop();
    }

    private void AsyncLoadGameObj(string name, Action<GameObject> onCompleted = null)
    {
        Addressables.InstantiateAsync(Path + name + ".prefab").Completed += handle =>
        {
            onCompleted?.Invoke(handle.Result);
        };
    }

    private void SetDefaultSizeOfPanel(IBasePanel panel)
    {
        var panelRectTrans = panel.Transform as RectTransform;

        panelRectTrans.offsetMin = Vector2.zero;
        panelRectTrans.offsetMax = Vector2.zero;
        panelRectTrans.anchoredPosition3D = Vector3.zero;
        panelRectTrans.anchorMin = Vector2.zero;
        panelRectTrans.anchorMax = Vector2.one;

        panelRectTrans.localScale = Vector3.one;
    }
}