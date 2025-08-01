using System.Collections.Generic;
using UnityEngine;

public class Button1 : BaseClickyButton
{
    public override void OnClick()
    {
    }

    public override void UpdateVirtual(SignalMessage message)
    {
    }

    protected override List<Signal> Caller()
    {
        return null;
    }

    protected override void LoadUIComponents()
    {
    }
}