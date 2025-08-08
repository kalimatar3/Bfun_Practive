using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class Panel1 : Basepanel
{
    public List<BaseClickyButton> buttons;
    protected override void LoadUIComponents()
    {
    }
    protected override List<Signal> Caller()
    {
        List<Signal> signals = base.Caller();
        foreach (var item in buttons)
        {
            signals.Add(item.buttonSignal);
        }
        signals.Add(Player.Instance.PlayerSignal);
        signals.Add(Player.Instance.Signal2);
        signals.Add(Player.Instance.Signal3);
        return signals;
    }
}
