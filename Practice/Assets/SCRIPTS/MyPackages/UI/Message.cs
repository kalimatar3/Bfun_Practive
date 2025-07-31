using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : BaseTextUI
{
    protected Signal signal;
    [SerializeField] protected string insidemessage;
    public string message { get {return insidemessage;} 
        set { 
                insidemessage = value;
                this.formatmessage();
                this.ShowText();
            }
        }
    public virtual void formatmessage() {
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.formatmessage();
    }
    public override void ShowText()
    {
        this.text.text = insidemessage;
    }

    public override void UpdateVirtual(SignalMessage caller)
    {
    }
    protected override List<Signal> UpdateVirtualCaller()
    {
        return new List<Signal>
        {
            signal
        };
    }
}
