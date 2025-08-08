using System;
using System.Collections;
using System.Collections.Generic;
using Clouds.Ultilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
public abstract class Basepanel : baseUI
{
    public Signal EnableSignal = new Signal()
    , DisableSignal = new Signal();
    public SerializableDictionary<SignalName, UIGroup> UIGroupDics = new SerializableDictionary<SignalName, UIGroup>();
    protected override void Awake()
    {
        base.Awake();
        foreach (var ele in UIGroupDics)
        {
            this.AddTweens(ele.Key);
        }
    }
    public void PlayTrigger(SignalMessage message)
    {
        if (!UIGroupDics.ContainsKey(message.Type)) return;
        UIGroup Element = UIGroupDics[message.Type];
        foreach (var ele in Element.tweens)
        {
            if (ele.IsPlaying() || ele.IsComplete()) ele.Restart();
            else ele.Play();
        }
    }
    protected override List<Signal> Caller()
    {
        return new List<Signal>()
        {
            EnableSignal,
            DisableSignal
        };
    }
    public override void UpdateVirtual(SignalMessage message)
    {
        this.PlayTrigger(message);
        this.PlayContinous(message);
    }
    public void PlayContinous(SignalMessage signalMessage)
    {
        if (!UIGroupDics.ContainsKey(signalMessage.Type)) return;
        UIGroup uIGroup = UIGroupDics[signalMessage.Type];
        foreach (var UIelement in uIGroup.UIS)
        {
            foreach (var ele in UIelement.ConEffects)
            {
                switch (ele.type)
                {
                    case CONTINUOSEFFECT.FillText:
                        UIUtility.FillText(ele, (string)signalMessage.Value);
                        break;
                    case CONTINUOSEFFECT.ChangeOpacity:
                        UIUtility.ChangeOpacity(ele, (float)signalMessage.Value);
                        break;
                }            
            }
        }
    }
    public void AddTweens(SignalName signalType, bool ignoreTimeScale = false)
    {
        UIGroup Element = UIGroupDics[signalType];
        Element.tweens = new List<Tween>();
        foreach (UIelement data in Element.UIS)
        {
            if (data.BEHAVIOUR == UIBEHAVIOUR.UPDATE) continue;
            foreach (UIEffectData effect in data.Effect)
            {
                Tween tween = null;
                switch (effect.type)
                {
                    case TRIGGEREFFECT.Move:
                        switch (effect.moveType)
                        {
                            case MOVEEFFECT.Custom:
                                tween = UIUtility.DoMoveCustom(effect, effect.timeMove, data.UIObj.GetComponent<RectTransform>(), data, effect.easeMove, effect.moveFrom, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromAbove:
                                tween = UIUtility.DoMoveAboveIn(effect, effect.timeMove, data.UIObj.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromBelow:
                                tween = UIUtility.DoMoveBelowIn(effect, effect.timeMove, data.UIObj.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromLeft:
                                tween = UIUtility.DoMoveLeftIn(effect, effect.timeMove, data.UIObj.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;

                            case MOVEEFFECT.FromRight:
                                tween = UIUtility.DoMoveRightIn(effect, effect.timeMove, data.UIObj.GetComponent<RectTransform>(), data, effect.easeMove, effect.loopMove, ignoreTimeScale);
                                break;
                        }
                        break;
                    case TRIGGEREFFECT.Rotate:
                        tween = UIUtility.DoRotate(effect, effect.timeRotate, data.UIObj.GetComponent<RectTransform>(), data, effect.easeRotate, effect.loopRotate, ignoreTimeScale);
                        break;
                    case TRIGGEREFFECT.Scale:
                        tween = UIUtility.DoScale(effect, effect.timeScale, data.UIObj.GetComponent<RectTransform>(), data, effect.easeScale, effect.loopScale, ignoreTimeScale);
                        break;
                    case TRIGGEREFFECT.Shake:
                        tween = UIUtility.DoShake(effect, effect.timeShake, data.UIObj.GetComponent<RectTransform>(), data, effect.easeShake, effect.loopShake, ignoreTimeScale);
                        break;
                    case TRIGGEREFFECT.Punch:
                        tween = UIUtility.DoPunch(effect, effect.timePunch, data.UIObj.GetComponent<RectTransform>(), data, effect.easePunch, effect.loopPunch, ignoreTimeScale);
                        break;

                    case TRIGGEREFFECT.Fade:
                        tween = UIUtility.DoFade(effect, effect.timeFade, data.UIObj.GetComponent<CanvasGroup>(), data, effect.easeFade, effect.loopFade, ignoreTimeScale);
                        break;
                }
                Element.tweens.Add(tween);
            }
        }
    }
}
