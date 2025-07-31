using System;
using JetBrains.Annotations;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public abstract class BaseClickyButton : BaseButton
{
    public Signal buttonSignal;
    protected override void LoadUIComponents()
    {
        base.LoadUIComponents();
        this.AddActButton();
    }
    protected void AddActButton()
    {
        if(thisbutton == null) return;
        thisbutton.onClick.AddListener(delegate () { this.ClickEvent(); });
    }
    protected virtual void ClickEvent()
    {
        if (!CanAct()) return;
        this.OnClick();
        buttonSignal.Send(new SignalMessage());
    }
    public abstract void OnClick();

    GameObject GetRoot()
    {
        return this.gameObject;
    }
}