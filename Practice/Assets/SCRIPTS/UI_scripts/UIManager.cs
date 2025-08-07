using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckGame.Ultilities;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using System;
using TMPro;

namespace Clouds.Ultilities
{
    public enum MOVEEFFECT { Custom, FromBelow, FromAbove, FromLeft, FromRight }
    public enum TRIGGEREFFECT { Move, Rotate, Scale, Shake, Punch, Fade, Nothing }
    public enum UIBEHAVIOUR
    {
        TRIGGER, UPDATE

    }
    public enum CONTINUOSEFFECT
    {
        FillText
    }
    public enum ACTIVATETYPE { Sequence, Delay, Continuously }
    public enum SOUNDTRIGGER { None, Same, Seperate }
    public enum SOUNDDELAY { SeperateDelay, SameAsPopup }
    public enum KEEPPOSATINIT { No, Yes }
    public enum SAFREAREA { No, Yes }
    public enum MULTIEFFECT { No, Yes }
    public enum DIRECTION { Left,Right}

    [System.Serializable]
    public struct UIEffectData
    {

        public TRIGGEREFFECT type;
        [Range(0, 5)]
        public float Delay;
        // Move
        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        public MOVEEFFECT moveType;

        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        [EnableIf("@this.type == TRIGGEREFFECT.Move && this.moveType == MOVEEFFECT.Custom")]
        public RectTransform moveFrom;

        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        public float timeMove;

        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        public ACTIVATETYPE moveActivate;

        [BoxGroup("Move Settings")]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        [PropertyOrder(1)]
        [ShowIf("moveActivate", ACTIVATETYPE.Delay)]
        public float delayTimeMove;
        [BoxGroup("Move Settings")]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        [PropertyOrder(1)]
        public Ease easeMove;

        [BoxGroup("Move Settings")]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        [PropertyOrder(1)]
        public bool loopMove;
        [ShowIfGroup("Move Settings/loopMove")]
        [ShowIf("type", TRIGGEREFFECT.Move)]
        [PropertyOrder(1)]
        public int loopCircleMove;

        // Rotate

        [BoxGroup("Rotate Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        public Vector3 rotateTo;

        [BoxGroup("Rotate Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        public float timeRotate;

        [BoxGroup("Rotate Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        public ACTIVATETYPE rotateActivate;

        [BoxGroup("Rotate Settings")]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        [PropertyOrder(1)]
        [ShowIf("rotateActivate", ACTIVATETYPE.Delay)]
        public float delayTimeRotate;
        [BoxGroup("Rotate Settings")]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        [PropertyOrder(1)]
        public Ease easeRotate;

        [BoxGroup("Rotate Settings")]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        [PropertyOrder(1)]
        public bool loopRotate;
        [ShowIfGroup("Rotate Settings/loopRotate")]
        [ShowIf("type", TRIGGEREFFECT.Rotate)]
        [PropertyOrder(1)]
        public int loopCircleRotate;

        // Scale

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        public Vector3 from;

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        public Vector3 scaleTo;

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        public float timeScale;

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        public ACTIVATETYPE scaleActivate;

        [BoxGroup("Scale Settings")]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        [PropertyOrder(1)]
        [ShowIf("scaleActivate", ACTIVATETYPE.Delay)]
        public float delayTimeScale;
        [BoxGroup("Scale Settings")]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        [PropertyOrder(1)]
        public Ease easeScale;

        [BoxGroup("Scale Settings")]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        [PropertyOrder(1)]
        public bool loopScale;
        [ShowIfGroup("Scale Settings/loopScale")]
        [ShowIf("type", TRIGGEREFFECT.Scale)]
        [PropertyOrder(1)]
        public int loopCircleScale;

        // Shake

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public bool shakePosition;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public bool shakeRotation;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public bool shakeScale;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public float shakeStrength;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public int shakeVibrate;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public float shakeRandomness;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public float timeShake;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        public ACTIVATETYPE shakeActivate;

        [BoxGroup("Shake Settings")]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        [PropertyOrder(1)]
        [ShowIf("shakeActivate", ACTIVATETYPE.Delay)]
        public float delayTimeShake;
        [BoxGroup("Shake Settings")]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        [PropertyOrder(1)]
        public Ease easeShake;

        [BoxGroup("Shake Settings")]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        [PropertyOrder(1)]
        public bool loopShake;
        [ShowIfGroup("Shake Settings/loopShake")]
        [ShowIf("type", TRIGGEREFFECT.Shake)]
        [PropertyOrder(1)]
        public int loopCircleShake;

        // Punch

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public bool punchPostion;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public bool punchRotation;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public bool punchScale;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public Vector2 punchTo;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public int punchVibrate;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public float punchElasticity;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public float timePunch;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        public ACTIVATETYPE punchActivate;

        [BoxGroup("Punch Settings")]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        [PropertyOrder(1)]
        [ShowIf("punchActivate", ACTIVATETYPE.Delay)]
        public float delayTimePunch;
        [BoxGroup("Punch Settings")]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        [PropertyOrder(1)]
        public Ease easePunch;

        [BoxGroup("Punch Settings")]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        [PropertyOrder(1)]
        public bool loopPunch;
        [ShowIfGroup("Punch Settings/loopPunch")]
        [ShowIf("type", TRIGGEREFFECT.Punch)]
        [PropertyOrder(1)]
        public int loopCirclePunch;

        // Fade
        [BoxGroup("Fade Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        [Range(0, 1)]
        public float fadeTo;

        [BoxGroup("Fade Settings")]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        [PropertyOrder(1)]
        public float timeFade;

        [BoxGroup("Fade Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        public ACTIVATETYPE fadeActivate;

        [BoxGroup("Fade Settings")]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        [PropertyOrder(1)]
        [ShowIf("fadeActivate", ACTIVATETYPE.Delay)]
        public float delayTimeFade;
        [BoxGroup("Fade Settings")]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        [PropertyOrder(1)]
        public Ease easeFade;

        [BoxGroup("Fade Settings")]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        [PropertyOrder(1)]
        public bool loopFade;
        [ShowIfGroup("Fade Settings/loopFade")]
        [ShowIf("type", TRIGGEREFFECT.Fade)]
        [PropertyOrder(1)]
        public int loopCircleFade;

        //
    }
    [System.Serializable]
    public struct UIContinousEffectdata
    {
        public CONTINUOSEFFECT type;
        [ShowIf("type", CONTINUOSEFFECT.FillText)]
        public string Text;
        [ShowIf("type", CONTINUOSEFFECT.FillText)]
        public TextMeshProUGUI TextComponent;
    }

    [System.Serializable]
    public class UIelement
    {
        [HideInInspector] public Vector3 realPos;
        [HideInInspector] public Vector3 initPos;
        [HideInInspector] public Vector3 initScale;
        [HideInInspector] public float initFade;
        [HideInInspector] public Transform initParent;
        public UIBEHAVIOUR BEHAVIOUR;
        [Range(0, 5)]
        public float Delay;
        public GameObject UIObj;
        [EnumToggleButtons]
        [ShowIf("BEHAVIOUR", UIBEHAVIOUR.TRIGGER)]
        public List<UIEffectData> Effect;
        [ShowIf("BEHAVIOUR", UIBEHAVIOUR.UPDATE)]
        public List<UIContinousEffectdata> ConEffects;
    }

    [System.Serializable]
    public class UIGroup
    {
        public SignalName PlotType;
        public UIelement[] UIS;
        public List<Tween> tweens = new List<Tween>();
    }
}
