using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(UniqueIDComponent))]
public abstract class baseUI : MyBehaviour
{
    [HideInInspector] public UniqueIDComponent idComponent;
    public int ID => idComponent != null ? idComponent.UniqueID : -1;
    protected List<Signal> callers;
    private Dictionary<Signal, Action<SignalMessage>> _cachedHandlers = new Dictionary<Signal, Action<SignalMessage>>();
    protected abstract void LoadUIComponents();

    // Update virutal this UI
    public abstract void UpdateVirtual(SignalMessage message);
    
    // Which Action UI can call UpdateVirtual Method to this UI
    protected abstract List<Signal> Caller();
    protected override void Awake()
    {
        this.callers = Caller();
        base.Awake();
        if (idComponent == null)
            idComponent = GetComponent<UniqueIDComponent>();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected virtual void SubscribeUpdateVirtualAcion()
    {
        if (callers == null) return;
        foreach (var ele in callers)
        {
            if (_cachedHandlers.ContainsKey(ele)) continue;
            Action<SignalMessage> handler = (SignalMessage) =>
            {
                SignalMessage signalMessage = SignalMessage;
                signalMessage.ROOTID = this.ID;
                UpdateVirtual(signalMessage);
            };
            ele.AddListener(handler);
            _cachedHandlers[ele] = handler;
        }
    }
    protected virtual void UnSubscribeUpdateVirtualAcion()
    {
        if (callers == null) return;
        foreach (var ele in callers)
        {
            if (_cachedHandlers.TryGetValue(ele, out var handler))
            {
                ele.RemoveListener(handler);
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
    public virtual void LOADUICOMPONENTS()
    {
        this.LoadUIComponents();
    }
#endif

}