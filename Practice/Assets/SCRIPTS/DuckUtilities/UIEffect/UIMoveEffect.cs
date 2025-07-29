using DG.Tweening;
using DuckGame.Ultilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveEffect : UIEffect
{
    [SerializeField] DIRECTION direction;

    public override void DoEffect()
    {
        if (direction == DIRECTION.Left)
        {
            DuckHelper.SetRectLeft(rectTransform, 0);
            DuckHelper.SetRectRight(rectTransform, Screen.width / 2);
        }
        else
        {
            DuckHelper.SetRectLeft(rectTransform, Screen.width / 2);
            DuckHelper.SetRectRight(rectTransform, 0);
        }
        base.DoEffect();
    }
    protected override IEnumerator IDoEffect()
    {
        yield return base.IDoEffect();
        rectTransform.DOLocalMove(new Vector2(direction == DIRECTION.Left? -Screen.width - 100 : Screen.width + 100, rectTransform.localPosition.y), time).SetEase(ease).OnComplete(() => onDone?.Invoke());
    }
}
