using DG.Tweening;
using DuckGame.Ultilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleEffect : UIEffect
{
    RectTransform rectParent;
    public override void DoEffect()
    {
        rectParent = transform.parent.GetComponent<RectTransform>();
        gameObject.SetActive(true);
        DuckHelper.SetRectTop(rectTransform, rectParent.rect.height / 2);
        DuckHelper.SetRectBottom(rectTransform, rectParent.rect.height / 2);
        base.DoEffect();
    }
    protected override IEnumerator IDoEffect()
    {
        yield return base.IDoEffect();
        DOVirtual.Float(rectParent.rect.height / 2, 0, time, (value) =>
        {
            DuckHelper.SetRectTop(rectTransform, value);
            DuckHelper.SetRectBottom(rectTransform, value);
        }).OnComplete(() =>
        {
            onDone?.Invoke();
            gameObject.SetActive(false);
            });
    }
}
