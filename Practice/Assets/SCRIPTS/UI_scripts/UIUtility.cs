using System.Collections;
using Clouds.Ultilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public static class UIUtility
{
    public static void FillText(UIContinousEffectdata effect, string text)
    {
        effect.TextComponent.text = text;
#if UNITY_EDITOR
        if(Application.isPlaying) effect.Text = text;
#endif        
    }
    public static Tween DoMoveCustom(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, RectTransform moveFrom, bool loop, bool ignoreTimeScale)
    {
        Vector2 initpos = data.UIObj.transform.localPosition;
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(ignoreTimeScale);
        seq.SetId(1);
        seq.AppendInterval(effect.Delay);
        seq.SetAutoKill(false);
        seq.Pause();
        seq.AppendCallback(() =>
        {
            rect.localPosition = new Vector2(moveFrom.localPosition.x, moveFrom.localPosition.y);
        });
        Tween moveTween = rect.DOLocalMove(initpos, time).SetEase(ease);
        if (loop)
            moveTween.SetLoops(effect.loopCircleMove);
        seq.Append(moveTween);
        return seq;
    }

    public static Tween DoMoveBelowIn(UIEffectData effect,float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale) {
        rect.localPosition = new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100));
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoMoveBelowOut(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        rect.localPosition = data.initPos;
        if(!loop)
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100)), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100)), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoMoveAboveIn(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale) {    
        rect.localPosition = new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100);
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoMoveAboveOut(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        rect.localPosition = data.initPos;
        if(!loop)
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoMoveRightIn(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale) {
        rect.localPosition = new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y);
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }   

    public static Tween DoMoveRightOut(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if(!loop)
            return rect.DOLocalMove(new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoMoveLeftIn(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale) {
        rect.localPosition = new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y);
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoMoveLeftOut(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if(!loop)
            return rect.DOLocalMove(new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y), time, false).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalMove(new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y), time, false).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoRotate(UIEffectData effect,float time, RectTransform rect, UIelement data, Ease ease,bool loop, bool ignoreTimeScale)
    {
        if (!loop)
            return rect.DOLocalRotate(effect.rotateTo, time, RotateMode.FastBeyond360).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOLocalRotate(effect.rotateTo, time, RotateMode.FastBeyond360).SetEase(ease).SetLoops(effect.loopCircleRotate).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoScale(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        rect.localScale = effect.from;
        if (!loop)
            return rect.DOScale(effect.scaleTo, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return rect.DOScale(effect.scaleTo, time).SetEase(ease).SetLoops(effect.loopCircleScale).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }

    public static Tween DoShake(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if (!loop)
        {
            if (effect.shakePosition)
                return rect.DOShakePosition(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.shakeRotation)
                return rect.DOShakeRotation(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.shakeScale)
                return rect.DOShakeScale(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        }
        else
        {
            if (effect.shakePosition)
                return rect.DOShakePosition(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.shakeRotation)
                return rect.DOShakeRotation(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.shakeScale)
                return rect.DOShakeScale(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        }
        return null;
    }

    public static Tween DoPunch(UIEffectData effect, float time, RectTransform rect, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if (!loop)
        {
            if (effect.punchPostion)
                return rect.DOPunchPosition(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.punchRotation)
                return rect.DOPunchRotation(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.punchScale)
                return  rect.DOPunchScale(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        }
        else
        {
            if (effect.punchPostion)
                return rect.DOPunchPosition(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.punchRotation)
                return rect.DOPunchRotation(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();

            if (effect.punchScale)
                return  rect.DOPunchScale(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        }
        return null;
    }

    public static Tween DoFade(UIEffectData effect, float time, CanvasGroup canvas, UIelement data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if(!loop)
            return canvas.DOFade(effect.fadeTo, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
        else
            return canvas.DOFade(effect.fadeTo, time).SetEase(ease).SetLoops(effect.loopCircleFade).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay).SetAutoKill(false).Pause();
    }
    
    public static IEnumerator StartEffectSequence(UIelement UIelementData, bool multiEffect = false, int mulStart = -1, bool ignoreDelay = false, int newDelay = 0, bool ignoreTimeScale = false, bool useCustomInitPos = false, Vector2 customInitPos = new Vector2(), bool invokeEvent = true)
    {
        yield return ignoreTimeScale ? new WaitForSecondsRealtime(ignoreDelay ? newDelay : UIelementData.Delay) : new WaitForSeconds(ignoreDelay ? newDelay : UIelementData.Delay);

        if (!UIelementData.UIObj.GetComponent<CanvasGroup>())
            UIelementData.UIObj.AddComponent<CanvasGroup>();
            
        float offSet = UIelementData.UIObj.transform.parent.GetComponent<RectTransform>().localPosition.x;

        UIelementData.initParent = UIelementData.UIObj.transform.parent;
        UIelementData.realPos = UIelementData.UIObj.GetComponent<RectTransform>().localPosition;

        if (!useCustomInitPos)
        { 
            // if(!isNotSameParent)
            //     UIelementData.UIElement.GetComponent<RectTransform>().localPosition = UIelementData.initPos = new Vector2(UIelementData.UIElement.GetComponent<RectTransform>().localPosition.x - offSet, UIelementData.UIElement.GetComponent<RectTransform>().localPosition.y);
        }
        else
        {
            UIelementData.UIObj.GetComponent<RectTransform>().localPosition = UIelementData.initPos = new Vector2(customInitPos.x , customInitPos.y);
        }

        UIelementData.initScale = UIelementData.UIObj.GetComponent<RectTransform>().localScale;
        UIelementData.initFade = UIelementData.UIObj.GetComponent<CanvasGroup>().alpha;

        if (!UIelementData.UIObj.activeInHierarchy)
        {
            UIelementData.UIObj.SetActive(true);
        }

        foreach(UIEffectData effect in  UIelementData.Effect)
        {
            switch(effect.type)
            {
                case TRIGGEREFFECT.Move:
                    switch (effect.moveType)
                    {
                        case MOVEEFFECT.FromBelow:
                            UIelementData.UIObj.GetComponent<RectTransform>().localPosition = new Vector2(UIelementData.UIObj.GetComponent<RectTransform>().localPosition.x, -((Screen.height / 2) + (UIelementData.UIObj.GetComponent<RectTransform>().sizeDelta.y / 2) + 100));
                            break;
                        case MOVEEFFECT.FromAbove:
                            UIelementData.UIObj.GetComponent<RectTransform>().localPosition = new Vector2(UIelementData.UIObj.GetComponent<RectTransform>().localPosition.x, (Screen.height / 2) + (UIelementData.UIObj.GetComponent<RectTransform>().sizeDelta.y / 2) + 100);
                            break;
                        case MOVEEFFECT.FromLeft:
                            UIelementData.UIObj.GetComponent<RectTransform>().localPosition = new Vector2(-((Screen.width / 2) + (UIelementData.UIObj.GetComponent<RectTransform>().sizeDelta.x / 2) + 100), UIelementData.UIObj.GetComponent<RectTransform>().localPosition.y);
                            break;
                        case MOVEEFFECT.FromRight:
                            UIelementData.UIObj.GetComponent<RectTransform>().localPosition = new Vector2((Screen.width / 2) + (UIelementData.UIObj.GetComponent<RectTransform>().sizeDelta.x / 2) + 100, UIelementData.UIObj.GetComponent<RectTransform>().localPosition.y);
                            break;
                    }
                    break;
                case TRIGGEREFFECT.Scale:
                    UIelementData.UIObj.transform.localScale = effect.from;
                    break;
                case TRIGGEREFFECT.Fade:
                    UIelementData.UIObj.GetComponent<CanvasGroup>().alpha = 0;
                    break;
            }
        }

        int i = 0;
        foreach (UIEffectData effect in  UIelementData.Effect)
        {
            i++;
            switch (effect.type)
            {
                case TRIGGEREFFECT.Move:
                    switch (effect.moveType)
                    {
                        case MOVEEFFECT.Custom:
                            DoMoveCustom(effect, effect.timeMove, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.moveFrom, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromAbove:
                            DoMoveAboveIn(effect, effect.timeMove, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromBelow:
                            DoMoveBelowIn(effect, effect.timeMove, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromLeft:
                            DoMoveLeftIn(effect, effect.timeMove, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromRight:
                            DoMoveRightIn(effect, effect.timeMove, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;
                    }
                    break;
                case TRIGGEREFFECT.Rotate:
                    DoRotate(effect, effect.timeRotate, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeRotate, effect.loopRotate, ignoreTimeScale);
                    break;
                case TRIGGEREFFECT.Scale:
                    DoScale(effect, effect.timeScale, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeScale, effect.loopScale, ignoreTimeScale);
                    break;
                case TRIGGEREFFECT.Shake:
                    DoShake(effect, effect.timeShake, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easeShake, effect.loopShake, ignoreTimeScale);
                    break;
                case TRIGGEREFFECT.Punch:
                    DoPunch(effect, effect.timePunch, UIelementData.UIObj.GetComponent<RectTransform>(), UIelementData, effect.easePunch, effect.loopPunch, ignoreTimeScale);
                    break;

                case TRIGGEREFFECT.Fade:
                    DoFade(effect, effect.timeFade, UIelementData.UIObj.GetComponent<CanvasGroup>(), UIelementData, effect.easeFade, effect.loopFade, ignoreTimeScale);
                    break;
            }
        }
    }
}