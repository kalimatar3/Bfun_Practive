using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public abstract class baseUI : MyBehaviour
{
    [SerializeField, HideInInspector]
    public long ID;
    protected List<Signal> callers;
    protected override void Awake()
    {
        this.callers = UpdateVirtualCaller();
        base.Awake();
    }
    protected abstract void LoadUIComponents();
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIComponents();
    }
    // Update virutal this UI
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
    private Dictionary<Signal, Action<SignalMessage>> _cachedHandlers = new Dictionary<Signal, Action<SignalMessage>>();

    protected virtual void SubscribeUpdateVirtualAcion()
    {
        foreach (var ele in callers)
        {
            if (_cachedHandlers.ContainsKey(ele)) continue;
            Action<SignalMessage> handler = (SignalMessage) => { Testupdate(SignalMessage); };
            ele.AddListener(handler);
            _cachedHandlers[ele] = handler;
        }
    }
    protected virtual void UnSubscribeUpdateVirtualAcion()
    {
        foreach (var ele in callers)
        {
            if (_cachedHandlers.TryGetValue(ele, out var handler))
            {
                ele.ClearAllListeners();
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
    protected virtual void OnDestroy() {
        UnSubscribeUpdateVirtualAcion();
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