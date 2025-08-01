using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public abstract class BaseButton : baseUI
{
    [SerializeField] protected Button thisbutton;
    [SerializeField] protected bool Permission = true;
    public void setPermission(bool trigger)
    {
        this.Permission = trigger;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.Permission = true;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadthisbutton();
    }
    protected void Loadthisbutton()
    {
        thisbutton = GetComponent<Button>();
        if (thisbutton == null) Debug.LogWarning(this.transform + "dont have button");
    }
    protected virtual bool CanAct()
    {
        return Permission;
    }
}
