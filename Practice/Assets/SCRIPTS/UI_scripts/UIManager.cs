using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckGame.Ultilities;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

namespace Clouds.Ultilities
{
    public enum MOVEEFFECT { Custom, FromBelow, FromAbove, FromLeft, FromRight }
    public enum EFFECT { Move, Rotate, Scale, Shake, Punch, Fade, Nothing }
    public enum ACTIVATETYPE { Sequence, Delay, Continuously }
    public enum SOUNDTRIGGER { None, Same, Seperate }
    public enum SOUNDDELAY { SeperateDelay, SameAsPopup }
    public enum KEEPPOSATINIT { No, Yes }
    public enum SAFREAREA { No, Yes }
    public enum MULTIEFFECT { No, Yes }
    public enum DIRECTION { Left,Right}

    [System.Serializable]
    public struct Settings
    {

        public EFFECT type;
        // Move
        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Move)]
        public MOVEEFFECT moveType;

        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Move)]
        [EnableIf("@this.type == EFFECT.Move && this.moveType == MOVEEFFECT.Custom")]
        public RectTransform moveFrom;

        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Move)]
        public float timeMove;

        [BoxGroup("Move Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Move)]
        public ACTIVATETYPE moveActivate;

        [BoxGroup("Move Settings")]
        [ShowIf("type", EFFECT.Move)]
        [PropertyOrder(1)]
        [ShowIf("moveActivate", ACTIVATETYPE.Delay)]
        public float delayTimeMove;
        [BoxGroup("Move Settings")]
        [ShowIf("type", EFFECT.Move)]
        [PropertyOrder(1)]
        public Ease easeMove;

        [BoxGroup("Move Settings")]
        [ShowIf("type", EFFECT.Move)]
        [PropertyOrder(1)]
        public bool loopMove;
        [ShowIfGroup("Move Settings/loopMove")]
        [ShowIf("type", EFFECT.Move)]
        [PropertyOrder(1)]
        public int loopCircleMove;

        // Rotate

        [BoxGroup("Rotate Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Rotate)]
        public Vector3 rotateTo;

        [BoxGroup("Rotate Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Rotate)]
        public float timeRotate;

        [BoxGroup("Rotate Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Rotate)]
        public ACTIVATETYPE rotateActivate;

        [BoxGroup("Rotate Settings")]
        [ShowIf("type", EFFECT.Rotate)]
        [PropertyOrder(1)]
        [ShowIf("rotateActivate", ACTIVATETYPE.Delay)]
        public float delayTimeRotate;
        [BoxGroup("Rotate Settings")]
        [ShowIf("type", EFFECT.Rotate)]
        [PropertyOrder(1)]
        public Ease easeRotate;

        [BoxGroup("Rotate Settings")]
        [ShowIf("type", EFFECT.Rotate)]
        [PropertyOrder(1)]
        public bool loopRotate;
        [ShowIfGroup("Rotate Settings/loopRotate")]
        [ShowIf("type", EFFECT.Rotate)]
        [PropertyOrder(1)]
        public int loopCircleRotate;

        // Scale

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Scale)]
        public Vector3 from;

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Scale)]
        public Vector3 scaleTo;

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Scale)]
        public float timeScale;

        [BoxGroup("Scale Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Scale)]
        public ACTIVATETYPE scaleActivate;

        [BoxGroup("Scale Settings")]
        [ShowIf("type", EFFECT.Scale)]
        [PropertyOrder(1)]
        [ShowIf("scaleActivate", ACTIVATETYPE.Delay)]
        public float delayTimeScale;
        [BoxGroup("Scale Settings")]
        [ShowIf("type", EFFECT.Scale)]
        [PropertyOrder(1)]
        public Ease easeScale;

        [BoxGroup("Scale Settings")]
        [ShowIf("type", EFFECT.Scale)]
        [PropertyOrder(1)]
        public bool loopScale;
        [ShowIfGroup("Scale Settings/loopScale")]
        [ShowIf("type", EFFECT.Scale)]
        [PropertyOrder(1)]
        public int loopCircleScale;

        // Shake

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public bool shakePosition;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public bool shakeRotation;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public bool shakeScale;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public float shakeStrength;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public int shakeVibrate;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public float shakeRandomness;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public float timeShake;

        [BoxGroup("Shake Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Shake)]
        public ACTIVATETYPE shakeActivate;

        [BoxGroup("Shake Settings")]
        [ShowIf("type", EFFECT.Shake)]
        [PropertyOrder(1)]
        [ShowIf("shakeActivate", ACTIVATETYPE.Delay)]
        public float delayTimeShake;
        [BoxGroup("Shake Settings")]
        [ShowIf("type", EFFECT.Shake)]
        [PropertyOrder(1)]
        public Ease easeShake;

        [BoxGroup("Shake Settings")]
        [ShowIf("type", EFFECT.Shake)]
        [PropertyOrder(1)]
        public bool loopShake;
        [ShowIfGroup("Shake Settings/loopShake")]
        [ShowIf("type", EFFECT.Shake)]
        [PropertyOrder(1)]
        public int loopCircleShake;

        // Punch

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public bool punchPostion;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public bool punchRotation;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public bool punchScale;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public Vector2 punchTo;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public int punchVibrate;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public float punchElasticity;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public float timePunch;

        [BoxGroup("Punch Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Punch)]
        public ACTIVATETYPE punchActivate;

        [BoxGroup("Punch Settings")]
        [ShowIf("type", EFFECT.Punch)]
        [PropertyOrder(1)]
        [ShowIf("punchActivate", ACTIVATETYPE.Delay)]
        public float delayTimePunch;
        [BoxGroup("Punch Settings")]
        [ShowIf("type", EFFECT.Punch)]
        [PropertyOrder(1)]
        public Ease easePunch;

        [BoxGroup("Punch Settings")]
        [ShowIf("type", EFFECT.Punch)]
        [PropertyOrder(1)]
        public bool loopPunch;
        [ShowIfGroup("Punch Settings/loopPunch")]
        [ShowIf("type", EFFECT.Punch)]
        [PropertyOrder(1)]
        public int loopCirclePunch;

        // Fade
        [BoxGroup("Fade Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Fade)]
        [Range(0, 1)]
        public float fadeTo;

        [BoxGroup("Fade Settings")]
        [ShowIf("type", EFFECT.Fade)]
        [PropertyOrder(1)]
        public float timeFade;

        [BoxGroup("Fade Settings")]
        [PropertyOrder(1)]
        [ShowIf("type", EFFECT.Fade)]
        public ACTIVATETYPE fadeActivate;

        [BoxGroup("Fade Settings")]
        [ShowIf("type", EFFECT.Fade)]
        [PropertyOrder(1)]
        [ShowIf("fadeActivate", ACTIVATETYPE.Delay)]
        public float delayTimeFade;
        [BoxGroup("Fade Settings")]
        [ShowIf("type", EFFECT.Fade)]
        [PropertyOrder(1)]
        public Ease easeFade;

        [BoxGroup("Fade Settings")]
        [ShowIf("type", EFFECT.Fade)]
        [PropertyOrder(1)]
        public bool loopFade;
        [ShowIfGroup("Fade Settings/loopFade")]
        [ShowIf("type", EFFECT.Fade)]
        [PropertyOrder(1)]
        public int loopCircleFade;

        //
    }

    [System.Serializable]
    public struct EffectData
    {

        public List<Settings> Effect;
        [HideInInspector] public Image image;
        [HideInInspector] public RectTransform rect;
    }

    [System.Serializable]
    public class UIelementData
    {
        [Range(0, 5)]
        public float showDelay;
        public GameObject UIElement;
        [EnumToggleButtons]
        public MULTIEFFECT multpleEffect;
        public EffectData startEffect;
        [ShowIf("multpleEffect", MULTIEFFECT.Yes)]
        public EffectData[] multpleStartEffect;
    }

    [System.Serializable]
    public class UIelement
    {
        // [TitleGroup("UI GROUP :", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        //[GUIColor(0, 1, 1)]
        public SignalType signalType;
        public UIelementData[] UIS;
        public Tween TestMove(Transform UI)
        {
            UI.DOKill();
            return UI.DORotate(new Vector3(0, 0, 360), .5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
        }
    }
}
