using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillEffect : UIEffect
{
    [SerializeField] bool reverse;

    protected override void Awake()
    {
        base.Awake();
        Reset();
    }

    public void SetRevrese(bool reverse)
    {
        this.reverse = reverse;
    }

    public override void Reset()
    {
        base.Reset();
        image.fillAmount = reverse ? 1 : 0;
    }
    public override void DoEffect()
    {
        if(!enabled) return;
        if (!image) image = GetComponent<Image>();
        image.fillAmount = reverse ? 1 : 0;
        base.DoEffect();
    }
    protected override IEnumerator IDoEffect()
    {
        yield return base.IDoEffect();
        PlaySound();
        image.DOFillAmount(reverse ? 0 : 1, time).SetEase(ease).OnComplete(() => onDone?.Invoke());
    }
}
