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
        this.gameObject.SetActive(false);
    }

    protected override List<Signal> Caller()
    {
        return new List<Signal>()
        {
            this.buttonSignal
        };
    }
    protected override void LoadUIComponents()
    {
    }
}
