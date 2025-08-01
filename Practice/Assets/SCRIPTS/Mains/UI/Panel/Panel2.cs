using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Panel2 : Basepanel
{
    public List<BaseClickyButton> buttons;
    protected override void LoadUIComponents()
    {
    }
    public override void UpdateVirtual(SignalMessage message)
    {
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOKill();
        switch (message.Type)
        {
            case SignalType.Rotate:
                rect.DOMoveX(this.transform.position.x - 5, 1f).SetLoops(-1, LoopType.Yoyo);
                break;
            case SignalType.Move:
                rect.DORotate(new Vector3(0, 0,this.transform.rotation.eulerAngles.z -  360), 1f, RotateMode.FastBeyond360)
                    .SetLoops(-1)
                    .SetEase(Ease.Linear);
                break;
        }
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