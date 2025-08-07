using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Panel2 : Basepanel
{
    public List<BaseClickyButton> buttons;
    protected override void LoadUIComponents()
    {
    }
    protected override List<Signal> Caller()
    {
        List<Signal> signals = new List<Signal>();
        foreach (var item in buttons)
        {
            signals.Add(item.buttonSignal);
        }
        return signals;
    }

}