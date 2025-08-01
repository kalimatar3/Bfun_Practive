using System;
using JetBrains.Annotations;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public abstract class BaseClickyButton : BaseButton
{
    public Signal buttonSignal = new Signal();
    public SignalMessage DefaultMessage;
    protected override void LoadComponents()
    {
        base.LoadComponents();
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
        buttonSignal.Send(DefaultMessage);
    }
    public abstract void OnClick();
}