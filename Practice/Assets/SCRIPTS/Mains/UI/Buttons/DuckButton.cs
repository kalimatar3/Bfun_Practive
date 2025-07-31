using System.Collections;
using System.Collections.Generic;
using DuckGame.Ultilities;
using UnityEngine;

public class DuckButton : BaseClickyButton
{
    public override void OnClick()
    {
        PopupManager.Popup.ShowPopup(PopupHome.Select);
    }

    public override void UpdateVirtual(SignalMessage caller)
    {
    }

    protected override List<Signal> UpdateVirtualCaller()
    {
        return new List<Signal>()
        {
            this.buttonSignal
        };
    }
}
