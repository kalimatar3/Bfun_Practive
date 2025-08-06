using System.Collections;
using System.Collections.Generic;
using Clouds.Ultilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
public abstract class Basepanel : baseUI
{
    [Searchable][SerializeField] public List<UIelement> Plots = new List<UIelement>();
    public Dictionary<SignalType,UIelement> UIelementDataDics = new Dictionary<SignalType, UIelement>();
    protected override void Awake() {
        base.Awake();
        if (UIelementDataDics.Count > 0) return;
        foreach (UIelement popup in Plots)
        {
            UIelementDataDics.Add(popup.PlotType, popup);
        }
    }
    public void PlayPlotbySignal(SignalType signalType)
    {
        UIelement Element = UIelementDataDics[signalType];
        foreach (var ele in Element.tweens)
        {
            ele.Play();
        }
    }
    public void AddTweens(SignalType signalType,bool ignoreTimeScale = false) 
    {
        int i = 0;
        this.Awake();
        UIelement Element = UIelementDataDics[signalType];
        Element.tweens = new List<Tween>();
        foreach (UIelementData data in Element.UIS)
        {
            foreach (Settings effect in data.startEffect.Effect)
            {
                i++;
                Tween tween = null;
                switch (effect.type)
                {
                    case EFFECT.Move:
                        switch (effect.moveType)
                        {
                            case MOVEEFFECT.Custom:
                                tween = DoMoveCustom(effect, effect.timeMove, data.UIElement.GetComponent<RectTransform>(), data, effect.easeMove, effect.moveFrom, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromAbove:
                                tween = DoMoveAboveIn(effect, effect.timeMove, data.UIElement.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromBelow:
                                tween = DoMoveBelowIn(effect, effect.timeMove, data.UIElement.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromLeft:
                                tween = DoMoveLeftIn(effect, effect.timeMove, data.UIElement.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromRight:
                                tween = DoMoveRightIn(effect, effect.timeMove, data.UIElement.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;
                        }
                        break;
                    case EFFECT.Rotate:
                        tween = DoRotate(effect, effect.timeRotate, data.UIElement.GetComponent<RectTransform>(), data, effect.easeRotate, effect.loopRotate, ignoreTimeScale);
                        break;
                    case EFFECT.Scale:
                        tween = DoScale(effect, effect.timeScale, data.UIElement.GetComponent<RectTransform>(), data, effect.easeScale, effect.loopScale, ignoreTimeScale);
                        break;
                    case EFFECT.Shake:
                        tween = DoShake(effect, effect.timeShake, data.UIElement.GetComponent<RectTransform>(), data, effect.easeShake, effect.loopShake, ignoreTimeScale);
                        break;
                    case EFFECT.Punch:
                        tween = DoPunch(effect, effect.timePunch, data.UIElement.GetComponent<RectTransform>(), data, effect.easePunch, effect.loopPunch, ignoreTimeScale);
                        break;

                    case EFFECT.Fade:
                        tween = DoFade(effect, effect.timeFade, data.UIElement.GetComponent<CanvasGroup>(), data, effect.easeFade, effect.loopFade, ignoreTimeScale);
                        break;
                }
                Element.tweens.Add(tween);
            }
        }
    }
    public Tween DoMoveCustom(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, RectTransform moveFrom, bool loop, bool ignoreTimeScale)
    {
        // Tạo một Sequence để delay trước khi set vị trí ban đầu
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(ignoreTimeScale);
        seq.SetId(1);

        // Delay bước đầu
        seq.AppendInterval(effect.Delay);

        // Sau delay, set vị trí bắt đầu
        seq.AppendCallback(() =>
        {
            rect.localPosition = new Vector2(moveFrom.localPosition.x, moveFrom.localPosition.y);
        });

        // Tween đến vị trí đích
        Tween moveTween = rect.DOLocalMove(data.initPos, time).SetEase(ease);

        if (loop)
            moveTween.SetLoops(effect.loopCircleMove);
        seq.Append(moveTween);
        return seq;
    }

    public Tween DoMoveBelowIn(Settings effect,float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale) {
        rect.localPosition = new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100));
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public Tween DoMoveBelowOut(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        rect.localPosition = data.initPos;
        if(!loop)
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100)), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100)), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public Tween DoMoveAboveIn(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale) {    
        rect.localPosition = new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100);
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public Tween DoMoveAboveOut(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        rect.localPosition = data.initPos;
        if(!loop)
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public Tween DoMoveRightIn(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale) {
        rect.localPosition = new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y);
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }   

    public Tween DoMoveRightOut(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if(!loop)
            return rect.DOLocalMove(new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public Tween DoMoveLeftIn(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale) {
        rect.localPosition = new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y);
        if(!loop)
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(data.initPos.x,  data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public Tween DoMoveLeftOut(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if(!loop)
            return rect.DOLocalMove(new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y), time, false).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalMove(new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y), time, false).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    Tween DoRotate(Settings effect,float time, RectTransform rect, UIelementData data, Ease ease,bool loop, bool ignoreTimeScale)
    {
        if (!loop)
            return rect.DOLocalRotate(effect.rotateTo, time, RotateMode.FastBeyond360).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOLocalRotate(effect.rotateTo, time, RotateMode.FastBeyond360).SetEase(ease).SetLoops(effect.loopCircleRotate).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    Tween DoScale(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        rect.localScale = effect.from;
        if (!loop)
            return rect.DOScale(effect.scaleTo, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return rect.DOScale(effect.scaleTo, time).SetEase(ease).SetLoops(effect.loopCircleScale).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    Tween DoShake(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if (!loop)
        {
            if (effect.shakePosition)
                return rect.DOShakePosition(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.shakeRotation)
                return rect.DOShakeRotation(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.shakeScale)
                return rect.DOShakeScale(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        }
        else
        {
            if (effect.shakePosition)
                return rect.DOShakePosition(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.shakeRotation)
                return rect.DOShakeRotation(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.shakeScale)
                return rect.DOShakeScale(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        }
        return null;
    }

    Tween DoPunch(Settings effect, float time, RectTransform rect, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if (!loop)
        {
            if (effect.punchPostion)
                return rect.DOPunchPosition(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.punchRotation)
                return rect.DOPunchRotation(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.punchScale)
                return  rect.DOPunchScale(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        }
        else
        {
            if (effect.punchPostion)
                return rect.DOPunchPosition(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.punchRotation)
                return rect.DOPunchRotation(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);

            if (effect.punchScale)
                return  rect.DOPunchScale(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        }
        return null;
    }

    Tween DoFade(Settings effect, float time, CanvasGroup canvas, UIelementData data, Ease ease, bool loop, bool ignoreTimeScale)
    {
        if(!loop)
            return canvas.DOFade(effect.fadeTo, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
        else
            return canvas.DOFade(effect.fadeTo, time).SetEase(ease).SetLoops(effect.loopCircleFade).SetId(1).SetUpdate(ignoreTimeScale).SetDelay(effect.Delay);
    }

    public void ShowPopup(SignalType signalType)
    {
        if (UIelementDataDics.Count > 0)
            StartCoroutine(StartPopupSequence(signalType));
    }

    public void ShowPopupIgnoreTimeScale(SignalType signalType)
    {
        if (UIelementDataDics.Count > 0)
            StartCoroutine(StartPopupSequence(signalType, null, null, null, null, null, true));
    }

    IEnumerator StartPopupSequence(SignalType signalType, int[] dontShow = null, int[] mulPopups = null, int[] startMuls = null, int[] ignoreDelays = null, int[] newDelays = null, bool ignoreTimeScale = false, bool useCustomInitPos = false, Vector2 customInitPos = new Vector2(), int[] customInitPoss = null, bool invokeEvent = true)
    {
        UIelement uilement = UIelementDataDics[signalType];
        bool ignoreDelay = false;
        bool skip = false;
        int i = 0;
        UIelementData[] UIelementDatas = UIelementDataDics[signalType].UIS;
        foreach (UIelementData ele in UIelementDatas)
        {
            bool isMul = false;
            bool isCustomInitPos = false;
            skip = false;
            if(dontShow != null)
            {
                for(int j = 0; j < dontShow.Length; j++)
                {
                    if(i == dontShow[j])
                    {
                        skip = true;
                        break;
                    }
                }
            }
            i++;
            if(skip) continue;

            if(mulPopups != null)
            {
                for(int k = 0; k < mulPopups.Length; k++)
                {
                    if(i - 1 == mulPopups[k])
                    { 
                        isMul = true;
                        break;
                    }
                }
            }

            if(customInitPoss != null)
            {
                for(int h = 0; h < customInitPoss.Length; h++)
                {
                    if(i - 1 == customInitPoss[h])
                    { 
                        isCustomInitPos = true;
                        break;
                    }
                }
            }

            if(ignoreDelays != null)
            {
                for(int l = 0; l < ignoreDelays.Length; l++)
                {
                    if(i - 1 == ignoreDelays[l])
                    { 
                        ignoreDelay = true;
                        break;
                    }
                }
            }

            if (ele.startEffect.Effect.Count > 0)
            {
                if(isMul && !ignoreDelay)
                    StartCoroutine(StartEffectSequence(ele, isMul, startMuls[i - 1], false, 0, ignoreTimeScale, useCustomInitPos, customInitPos, invokeEvent));
                else if(!isMul && !ignoreDelay)
                    StartCoroutine(StartEffectSequence(ele, false, -1, false, 0, ignoreTimeScale, useCustomInitPos, customInitPos, invokeEvent));
                else if(!isMul && ignoreDelay)
                    StartCoroutine(StartEffectSequence(ele, false, -1, true, newDelays[i - 1], ignoreTimeScale, useCustomInitPos, customInitPos, invokeEvent));
                else if(isMul && !ignoreDelay && isCustomInitPos)
                    StartCoroutine(StartEffectSequence(ele, isMul, startMuls[i - 1], false, 0, ignoreTimeScale, true, customInitPos, invokeEvent));
                else if(!isMul && !ignoreDelay && isCustomInitPos)
                    StartCoroutine(StartEffectSequence(ele, false, -1, false, 0, ignoreTimeScale, true, customInitPos, invokeEvent));
                else if(!isMul && ignoreDelay && isCustomInitPos)
                    StartCoroutine(StartEffectSequence(ele, false, -1, true, newDelays[i - 1], false, true, customInitPos, invokeEvent));
            }
            yield return null;
        }
    }
    
    IEnumerator StartEffectSequence(UIelementData UIelementData, bool multiEffect = false, int mulStart = -1, bool ignoreDelay = false, int newDelay = 0, bool ignoreTimeScale = false, bool useCustomInitPos = false, Vector2 customInitPos = new Vector2(), bool invokeEvent = true)
    {
        yield return ignoreTimeScale ? new WaitForSecondsRealtime(ignoreDelay ? newDelay : UIelementData.Delay) : new WaitForSeconds(ignoreDelay ? newDelay : UIelementData.Delay);

        if (!UIelementData.UIElement.GetComponent<CanvasGroup>())
            UIelementData.UIElement.AddComponent<CanvasGroup>();
            
        float offSet = UIelementData.UIElement.transform.parent.GetComponent<RectTransform>().localPosition.x;

        UIelementData.initParent = UIelementData.UIElement.transform.parent;
        UIelementData.realPos = UIelementData.UIElement.GetComponent<RectTransform>().localPosition;

        if (!useCustomInitPos)
        { 
            // if(!isNotSameParent)
            //     UIelementData.UIElement.GetComponent<RectTransform>().localPosition = UIelementData.initPos = new Vector2(UIelementData.UIElement.GetComponent<RectTransform>().localPosition.x - offSet, UIelementData.UIElement.GetComponent<RectTransform>().localPosition.y);
        }
        else
        {
            UIelementData.UIElement.GetComponent<RectTransform>().localPosition = UIelementData.initPos = new Vector2(customInitPos.x , customInitPos.y);
        }

        UIelementData.initScale = UIelementData.UIElement.GetComponent<RectTransform>().localScale;
        UIelementData.initFade = UIelementData.UIElement.GetComponent<CanvasGroup>().alpha;

        if (!UIelementData.UIElement.activeInHierarchy)
        {
            UIelementData.UIElement.SetActive(true);
        }

        foreach(Settings effect in  UIelementData.startEffect.Effect)
        {
            switch(effect.type)
            {
                case EFFECT.Move:
                    switch (effect.moveType)
                    {
                        case MOVEEFFECT.FromBelow:
                            UIelementData.UIElement.GetComponent<RectTransform>().localPosition = new Vector2(UIelementData.UIElement.GetComponent<RectTransform>().localPosition.x, -((Screen.height / 2) + (UIelementData.UIElement.GetComponent<RectTransform>().sizeDelta.y / 2) + 100));
                            break;
                        case MOVEEFFECT.FromAbove:
                            UIelementData.UIElement.GetComponent<RectTransform>().localPosition = new Vector2(UIelementData.UIElement.GetComponent<RectTransform>().localPosition.x, (Screen.height / 2) + (UIelementData.UIElement.GetComponent<RectTransform>().sizeDelta.y / 2) + 100);
                            break;
                        case MOVEEFFECT.FromLeft:
                            UIelementData.UIElement.GetComponent<RectTransform>().localPosition = new Vector2(-((Screen.width / 2) + (UIelementData.UIElement.GetComponent<RectTransform>().sizeDelta.x / 2) + 100), UIelementData.UIElement.GetComponent<RectTransform>().localPosition.y);
                            break;
                        case MOVEEFFECT.FromRight:
                            UIelementData.UIElement.GetComponent<RectTransform>().localPosition = new Vector2((Screen.width / 2) + (UIelementData.UIElement.GetComponent<RectTransform>().sizeDelta.x / 2) + 100, UIelementData.UIElement.GetComponent<RectTransform>().localPosition.y);
                            break;
                    }
                    break;
                case EFFECT.Scale:
                    UIelementData.UIElement.transform.localScale = effect.from;
                    break;
                case EFFECT.Fade:
                    UIelementData.UIElement.GetComponent<CanvasGroup>().alpha = 0;
                    break;
            }
        }

        int i = 0;
        foreach (Settings effect in  UIelementData.startEffect.Effect)
        {
            i++;
            switch (effect.type)
            {
                case EFFECT.Move:
                    switch (effect.moveType)
                    {
                        case MOVEEFFECT.Custom:
                            DoMoveCustom(effect, effect.timeMove, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.moveFrom, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromAbove:
                            DoMoveAboveIn(effect, effect.timeMove, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromBelow:
                            DoMoveBelowIn(effect, effect.timeMove, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromLeft:
                            DoMoveLeftIn(effect, effect.timeMove, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;

                        case MOVEEFFECT.FromRight:
                            DoMoveRightIn(effect, effect.timeMove, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeMove, effect.loopMove, ignoreTimeScale);
                            break;
                    }
                    break;
                case EFFECT.Rotate:
                    DoRotate(effect, effect.timeRotate, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeRotate, effect.loopRotate, ignoreTimeScale);
                    break;
                case EFFECT.Scale:
                    DoScale(effect, effect.timeScale, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeScale, effect.loopScale, ignoreTimeScale);
                    break;
                case EFFECT.Shake:
                    DoShake(effect, effect.timeShake, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easeShake, effect.loopShake, ignoreTimeScale);
                    break;
                case EFFECT.Punch:
                    DoPunch(effect, effect.timePunch, UIelementData.UIElement.GetComponent<RectTransform>(), UIelementData, effect.easePunch, effect.loopPunch, ignoreTimeScale);
                    break;

                case EFFECT.Fade:
                    DoFade(effect, effect.timeFade, UIelementData.UIElement.GetComponent<CanvasGroup>(), UIelementData, effect.easeFade, effect.loopFade, ignoreTimeScale);
                    break;
            }
        }
    }
}