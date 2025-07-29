using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlashEffect : UIEffect
{
    public override void DoEffect()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        base.DoEffect();
    }
    protected override IEnumerator IDoEffect()
    {
        yield return base.IDoEffect();
        PlaySound();
        image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 1), time).OnComplete(() => image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0), time).SetEase(ease).OnComplete(() => onDone?.Invoke()));
    }
}
