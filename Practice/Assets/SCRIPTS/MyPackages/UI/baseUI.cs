using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class baseUI : MyBehaviour
{
    // Update virutal this UI
    public long ID;
    protected override void Awake()
    {
        ID = UIIDManager.GetUIID();
        base.Awake();
    }
    protected abstract void LoadUIComponents();
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIComponents();
    }
    // UpdateUI of panel will be called in Enable
    public abstract void UpdateVirtual(SignalMessage caller);
    protected void Testupdate(SignalMessage signalMessage)
    {
        if (ID == signalMessage.TARGETID)
        {
            UpdateVirtual(signalMessage);
        }
    }
    // Which Action UI can call UpdateVirtual Method to this UI
    protected abstract List<Signal> UpdateVirtualCaller();
    private Dictionary<Signal, Action<SignalMessage>> _cachedHandlers = new();

    protected virtual void SubscribeUpdateVirtualAcion()
    {
        foreach (var ele in UpdateVirtualCaller())
        {
            Action<SignalMessage> handler = (SignalMessage) => { Testupdate(SignalMessage); };
            ele.Event += handler;
            _cachedHandlers[ele] = handler;
        }
    }
    protected virtual void UnSubscribeUpdateVirtualAcion()
    {
        foreach (var ele in UpdateVirtualCaller())
        {
            if (_cachedHandlers.TryGetValue(ele, out var handler))
            {
                ele.Event -= handler;
            }
        }
        _cachedHandlers.Clear();
    }
    protected virtual void OnEnable()
    {
        this.SubscribeUpdateVirtualAcion();
    }
    protected virtual void OnDisable()
    {
        this.UnSubscribeUpdateVirtualAcion();
    }

    #if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
    [GUIColor(0,1,1)]
    public void LOADUICOMPONENTS()
    {
        this.LoadUIComponents();
    }
#endif

}