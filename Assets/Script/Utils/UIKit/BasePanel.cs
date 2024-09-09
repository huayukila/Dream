using UnityEngine;
using Framework.Farm;

public enum UILevel
{
    Common,
    Pop,
    Bg,
}

public enum PanelState
{
    Opening = 0,
    Hide,
    Closed,
}

public interface IBasePanel
{
    Transform Transform { get; }

    UIInfo Info { get; set; }

    PanelState State { get; set; }
    void Enter();
    void Exit();
    void Pause();
    void Resume();
    void Show();
    void Hide();
    void Init();
}

public abstract class BasePanel : ProjectCtrler, IBasePanel
{
    public Transform Transform => transform;
    public UIInfo Info { get; set; }
    public PanelState State { get; set; }
    
    void IBasePanel.Enter()
    {
        OnEnter();
    }

    void IBasePanel.Exit()
    {
        OnExit();
    }

    void IBasePanel.Pause()
    {
        OnPause();
    }

    void IBasePanel.Resume()
    {
        OnResume();
    }

    void IBasePanel.Show()
    {
        OnShow();
    }

    void IBasePanel.Hide()
    {
        OnHide();
    }


    void IBasePanel.Init()
    {
        OnInit();
    }


    protected abstract void OnEnter();

    protected abstract void OnExit();

    protected abstract void OnShow();

    protected abstract void OnHide();

    protected abstract void OnInit();

    protected virtual void OnPause()
    {
    }

    protected virtual void OnResume()
    {
    }

    protected void CloseSelf()
    {
        UIKit.ClosePanel();
        OnExit();
    }
}