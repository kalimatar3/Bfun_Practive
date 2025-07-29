using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public enum SPRINGTTYPE {Scale, Rotate}
public enum SCALETYPE {ScaleX, ScaleY, ScaleZ, ScaleXY, ScaleXZ, ScaleYZ, ScaleXYZ}
public enum ROTATETYPE {RotateX, RotateY, RotateZ, RotateXY, RotateXZ, RotateYZ, RotateXYZ}
public enum CALLTYPE {Once, Loop}

[Serializable]
public class SpringEffect
{
    [TitleGroup("SRING EFFECT", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    public float delayCall;
    public SPRINGTTYPE springType;
    [ShowIf("springType", SPRINGTTYPE.Scale)]
    public SCALETYPE scaleType;
    
    [BoxGroup("ScaleX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleX || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float scaleXFROM;
    
    [BoxGroup("ScaleX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleX || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float scaleXTO;
    public SpringUtils.tDampedSpringMotionParams springParamsScaleX = new SpringUtils.tDampedSpringMotionParams();
    [HideInInspector] public float targetValueScaleX;
    [HideInInspector] public float currentValueScaleX;
    [HideInInspector] public float velScaleX;

    [BoxGroup("ScaleX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleX || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float frequencyScaleX = 15f;

    [BoxGroup("ScaleX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleX || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float dampingRatioScaleX = 0.5f;

    [BoxGroup("ScaleY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleY || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float scaleYFROM;

    [BoxGroup("ScaleY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleY || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float scaleYTO;
    public SpringUtils.tDampedSpringMotionParams springParamsScaleY = new SpringUtils.tDampedSpringMotionParams();
    [HideInInspector] public float targetValueScaleY;
    [HideInInspector] public float currentValueScaleY;
    [HideInInspector] public float velScaleY;

    [BoxGroup("ScaleY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleY || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float frequencyScaleY = 15f;

    [BoxGroup("ScaleY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleY || this.scaleType == SCALETYPE.ScaleXY || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float dampingRatioScaleY = 0.5f;

    [BoxGroup("ScaleZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleZ || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float scaleZFROM;
    
    [BoxGroup("ScaleZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleZ || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float scaleZTO;
    public SpringUtils.tDampedSpringMotionParams springParamsScaleZ = new SpringUtils.tDampedSpringMotionParams();
    [HideInInspector] public float targetValueScaleZ;
    [HideInInspector] public float currentValueScaleZ;
    [HideInInspector] public float velScaleZ;

    [BoxGroup("ScaleZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleZ || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float frequencyScaleZ = 15f;

    [BoxGroup("ScaleZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Scale && (this.scaleType == SCALETYPE.ScaleZ || this.scaleType == SCALETYPE.ScaleXZ || this.scaleType == SCALETYPE.ScaleYZ || this.scaleType == SCALETYPE.ScaleXYZ)")]
    public float dampingRatioScaleZ = 0.5f;
    
    [ShowIf("springType", SPRINGTTYPE.Rotate)]
    public ROTATETYPE rotateType;

    [BoxGroup("RotateX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateX || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float rotateXFROM;

    [BoxGroup("RotateX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateX || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float rotateXTO;
    public SpringUtils.tDampedSpringMotionParams springParamsRotateX = new SpringUtils.tDampedSpringMotionParams();
    [HideInInspector] public float targetValueRotateX;
    [HideInInspector] public float currentValueRotateX;
    [HideInInspector] public float velRotateX;

    [BoxGroup("RotateX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateX || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float frequencyRotateX = 15f;

    [BoxGroup("RotateX")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateX || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float dampingRatioRotateX = 0.5f;

    [BoxGroup("RotateY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateY || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float rotateYFROM;

    [BoxGroup("RotateY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateY || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float rotateYTO;
    public SpringUtils.tDampedSpringMotionParams springParamsRotateY = new SpringUtils.tDampedSpringMotionParams();
    [HideInInspector] public float targetValueRotateY;
    [HideInInspector] public float currentValueRotateY;
    [HideInInspector] public float velRotateY;

    [BoxGroup("RotateY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateY || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float frequencyRotateY = 15f;

    [BoxGroup("RotateY")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateY || this.rotateType == ROTATETYPE.RotateXY || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float dampingRatioRotateY = 0.5f;

    [BoxGroup("RotateZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateZ || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float rotateZFROM;

    [BoxGroup("RotateZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateZ || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float rotateZTO;
    public SpringUtils.tDampedSpringMotionParams springParamsRotateZ = new SpringUtils.tDampedSpringMotionParams();
    [HideInInspector] public float targetValueRotateZ;
    [HideInInspector] public float currentValueRotateZ;
    [HideInInspector] public float velRotateZ;

    [BoxGroup("RotateZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateZ || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float frequencyRotateZ = 15f;

    [BoxGroup("RotateZ")]
    [ShowIf("@this.springType == SPRINGTTYPE.Rotate && (this.rotateType == ROTATETYPE.RotateZ || this.rotateType == ROTATETYPE.RotateXZ || this.rotateType == ROTATETYPE.RotateYZ || this.rotateType == ROTATETYPE.RotateXYZ)")]
    public float dampingRatioRotateZ = 0.5f;
}

[Serializable]
public struct SpringData
{
    [EnumToggleButtons]
    public CALLTYPE callType;

    [ShowIf("callType", CALLTYPE.Loop)]
    public bool ignoreFirstTimeStart;

    [ShowIf("callType", CALLTYPE.Loop)]
    public float loopDelayTimeStart;

    [ShowIf("callType", CALLTYPE.Loop)]
    public float loopDelayTimeEnd;
    public SpringEffect[] springEffects;
}

public class UISpringEffect : MonoBehaviour
{
    [SerializeField] SpringData springData;
    RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartSpringEffect()
    {
        StartCoroutine(IStartSpringEffect());
    }


    public IEnumerator IStartSpringEffect() {

        if(springData.callType == CALLTYPE.Loop && !springData.ignoreFirstTimeStart) yield return new WaitForSeconds(springData.loopDelayTimeStart);

        if(springData.ignoreFirstTimeStart) springData.ignoreFirstTimeStart = false;

        foreach(SpringEffect springEffect in springData.springEffects)
        {
            yield return new WaitForSeconds(springEffect.delayCall);
            switch(springEffect.springType)
            {
                case SPRINGTTYPE.Scale:
                    switch(springEffect.scaleType)
                    {
                        case SCALETYPE.ScaleX:
                            springEffect.currentValueScaleX = springEffect.scaleXFROM;
                            springEffect.targetValueScaleX = springEffect.scaleXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleX, springEffect.targetValueScaleX, springEffect.springParamsScaleX, springEffect.frequencyScaleX, springEffect.dampingRatioScaleX, springEffect.velScaleX, springEffect));
                            break;
                        case SCALETYPE.ScaleY:
                            springEffect.currentValueScaleY = springEffect.scaleYFROM;
                            springEffect.targetValueScaleY = springEffect.scaleYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleY, springEffect.targetValueScaleY, springEffect.springParamsScaleY, springEffect.frequencyScaleY, springEffect.dampingRatioScaleY, springEffect.velScaleY, springEffect));
                            break;
                        case SCALETYPE.ScaleZ:
                            springEffect.currentValueScaleZ = springEffect.scaleZFROM;
                            springEffect.targetValueScaleZ = springEffect.scaleZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleZ, springEffect.targetValueScaleZ, springEffect.springParamsScaleZ, springEffect.frequencyScaleZ, springEffect.dampingRatioScaleZ, springEffect.velScaleZ, springEffect));
                            break;
                        case SCALETYPE.ScaleXY:
                            springEffect.currentValueScaleX = springEffect.scaleXFROM;
                            springEffect.targetValueScaleX = springEffect.scaleXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleX, springEffect.targetValueScaleX, springEffect.springParamsScaleX, springEffect.frequencyScaleX, springEffect.dampingRatioScaleX, springEffect.velScaleX, springEffect));
                            springEffect.currentValueScaleY = springEffect.scaleYFROM;
                            springEffect.targetValueScaleY = springEffect.scaleYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleY, springEffect.targetValueScaleY, springEffect.springParamsScaleY, springEffect.frequencyScaleY, springEffect.dampingRatioScaleY, springEffect.velScaleY, springEffect));
                            break;
                        case SCALETYPE.ScaleXZ:
                            springEffect.currentValueScaleX = springEffect.scaleXFROM;
                            springEffect.targetValueScaleX = springEffect.scaleXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleX, springEffect.targetValueScaleX, springEffect.springParamsScaleX, springEffect.frequencyScaleX, springEffect.dampingRatioScaleX, springEffect.velScaleX, springEffect));
                            springEffect.currentValueScaleZ = springEffect.scaleZFROM;
                            springEffect.targetValueScaleZ = springEffect.scaleZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleZ, springEffect.targetValueScaleZ, springEffect.springParamsScaleZ, springEffect.frequencyScaleZ, springEffect.dampingRatioScaleZ, springEffect.velScaleZ, springEffect));
                            break;
                        case SCALETYPE.ScaleYZ:
                            springEffect.currentValueScaleY = springEffect.scaleYFROM;
                            springEffect.targetValueScaleY = springEffect.scaleYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleY, springEffect.targetValueScaleY, springEffect.springParamsScaleY, springEffect.frequencyScaleY, springEffect.dampingRatioScaleY, springEffect.velScaleY, springEffect));
                            springEffect.currentValueScaleZ = springEffect.scaleZFROM;
                            springEffect.targetValueScaleZ = springEffect.scaleZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleZ, springEffect.targetValueScaleZ, springEffect.springParamsScaleZ, springEffect.frequencyScaleZ, springEffect.dampingRatioScaleZ, springEffect.velScaleZ, springEffect));
                            break;
                        case SCALETYPE.ScaleXYZ:
                            springEffect.currentValueScaleX = springEffect.scaleXFROM;
                            springEffect.targetValueScaleX = springEffect.scaleXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleX, springEffect.targetValueScaleX, springEffect.springParamsScaleX, springEffect.frequencyScaleX, springEffect.dampingRatioScaleX, springEffect.velScaleX, springEffect));
                            springEffect.currentValueScaleY = springEffect.scaleYFROM;
                            springEffect.targetValueScaleY = springEffect.scaleYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleY, springEffect.targetValueScaleY, springEffect.springParamsScaleY, springEffect.frequencyScaleY, springEffect.dampingRatioScaleY, springEffect.velScaleY, springEffect));
                            springEffect.currentValueScaleZ = springEffect.scaleZFROM;
                            springEffect.targetValueScaleZ = springEffect.scaleZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueScaleZ, springEffect.targetValueScaleZ, springEffect.springParamsScaleZ, springEffect.frequencyScaleZ, springEffect.dampingRatioScaleZ, springEffect.velScaleZ, springEffect));
                            break;
                    }
                    break;

                case SPRINGTTYPE.Rotate:
                    switch(springEffect.rotateType)
                    {
                        case ROTATETYPE.RotateX:
                            springEffect.currentValueRotateX = springEffect.rotateXFROM;
                            springEffect.targetValueRotateX = springEffect.rotateXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateX, springEffect.targetValueRotateX, springEffect.springParamsRotateX, springEffect.frequencyRotateX, springEffect.dampingRatioRotateX, springEffect.velRotateX, springEffect));
                            break;
                        case ROTATETYPE.RotateY:
                            springEffect.currentValueRotateY = springEffect.rotateYFROM;
                            springEffect.targetValueRotateY = springEffect.rotateYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateY, springEffect.targetValueRotateY, springEffect.springParamsRotateY, springEffect.frequencyRotateY, springEffect.dampingRatioRotateY, springEffect.velRotateY, springEffect));
                            break;
                        case ROTATETYPE.RotateZ:
                            springEffect.currentValueRotateZ = springEffect.rotateZFROM;
                            springEffect.targetValueRotateZ = springEffect.rotateZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateZ, springEffect.targetValueRotateZ, springEffect.springParamsRotateZ, springEffect.frequencyRotateZ, springEffect.dampingRatioRotateZ, springEffect.velRotateZ, springEffect));
                            break;
                        case ROTATETYPE.RotateXY:
                            springEffect.currentValueRotateX = springEffect.rotateXFROM;
                            springEffect.targetValueRotateX = springEffect.rotateXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateX, springEffect.targetValueRotateX, springEffect.springParamsRotateX, springEffect.frequencyRotateX, springEffect.dampingRatioRotateX, springEffect.velRotateX, springEffect));
                            springEffect.currentValueRotateY = springEffect.rotateYFROM;
                            springEffect.targetValueRotateY = springEffect.rotateYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateY, springEffect.targetValueRotateY, springEffect.springParamsRotateY, springEffect.frequencyRotateY, springEffect.dampingRatioRotateY, springEffect.velRotateY, springEffect));
                            break;
                        case ROTATETYPE.RotateXZ:
                            springEffect.currentValueRotateX = springEffect.rotateXFROM;
                            springEffect.targetValueRotateX = springEffect.rotateXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateX, springEffect.targetValueRotateX, springEffect.springParamsRotateX, springEffect.frequencyRotateX, springEffect.dampingRatioRotateX, springEffect.velRotateX, springEffect));
                            springEffect.currentValueRotateZ = springEffect.rotateZFROM;
                            springEffect.targetValueRotateZ = springEffect.rotateZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateZ, springEffect.targetValueRotateZ, springEffect.springParamsRotateZ, springEffect.frequencyRotateZ, springEffect.dampingRatioRotateZ, springEffect.velRotateZ, springEffect));
                            break;
                        case ROTATETYPE.RotateYZ:
                            springEffect.currentValueRotateY = springEffect.rotateYFROM;
                            springEffect.targetValueRotateY = springEffect.rotateYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateY, springEffect.targetValueRotateY, springEffect.springParamsRotateY, springEffect.frequencyRotateY, springEffect.dampingRatioRotateY, springEffect.velRotateY, springEffect));
                            springEffect.currentValueRotateZ = springEffect.rotateZFROM;
                            springEffect.targetValueRotateZ = springEffect.rotateZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateZ, springEffect.targetValueRotateZ, springEffect.springParamsRotateZ, springEffect.frequencyRotateZ, springEffect.dampingRatioRotateZ, springEffect.velRotateZ, springEffect));
                            break;
                        case ROTATETYPE.RotateXYZ:
                            springEffect.currentValueRotateX = springEffect.rotateXFROM;
                            springEffect.targetValueRotateX = springEffect.rotateXTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateX, springEffect.targetValueRotateX, springEffect.springParamsRotateX, springEffect.frequencyRotateX, springEffect.dampingRatioRotateX, springEffect.velRotateX, springEffect));
                            springEffect.currentValueRotateY = springEffect.rotateYFROM;
                            springEffect.targetValueRotateY = springEffect.rotateYTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateY, springEffect.targetValueRotateY, springEffect.springParamsRotateY, springEffect.frequencyRotateY, springEffect.dampingRatioRotateY, springEffect.velRotateY, springEffect));
                            springEffect.currentValueRotateZ = springEffect.rotateZFROM;
                            springEffect.targetValueRotateZ = springEffect.rotateZTO;
                            StartCoroutine(ISpringCalc(springEffect.currentValueRotateZ, springEffect.targetValueRotateZ, springEffect.springParamsRotateZ, springEffect.frequencyRotateZ, springEffect.dampingRatioRotateZ, springEffect.velRotateZ, springEffect));
                            break;
                    }
                    break;
            }
        }


        if(springData.callType == CALLTYPE.Loop) 
        {
            yield return new WaitForSeconds(springData.loopDelayTimeEnd);
            StopAllCoroutines();
            StartSpringEffect();
        }
    }

    IEnumerator ISpringCalc(float currentValue, float targetValue, SpringUtils.tDampedSpringMotionParams springParams, float frequency, float dampingRatio, float vel, SpringEffect springEffect)
    {
        while(currentValue != targetValue)
        {
            SpringUtils.CalcDampedSpringMotionParams(ref springParams, Time.deltaTime, frequency, dampingRatio);
            SpringUtils.UpdateDampedSpringMotion(ref currentValue, ref vel, targetValue, in springParams);
            switch(springEffect.springType)
            {
                case SPRINGTTYPE.Scale:
                    switch(springEffect.scaleType)
                    {
                        case SCALETYPE.ScaleX:
                            rectTransform.localScale = new Vector3(currentValue, rectTransform.localScale.y, rectTransform.localScale.z);
                            break;
                        case SCALETYPE.ScaleY:
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, currentValue, rectTransform.localScale.z);
                            break;
                        case SCALETYPE.ScaleZ:
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, currentValue);
                            break;
                        case SCALETYPE.ScaleXY:
                            rectTransform.localScale = new Vector3(currentValue, rectTransform.localScale.y, rectTransform.localScale.z);
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, currentValue, rectTransform.localScale.z);
                            break;
                        case SCALETYPE.ScaleXZ:
                            rectTransform.localScale = new Vector3(currentValue, rectTransform.localScale.y, rectTransform.localScale.z);
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, currentValue);
                            break;
                        case SCALETYPE.ScaleYZ:
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, currentValue, rectTransform.localScale.z);
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, currentValue);
                            break;
                        case SCALETYPE.ScaleXYZ:
                            rectTransform.localScale = new Vector3(currentValue, rectTransform.localScale.y, rectTransform.localScale.z);
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, currentValue, rectTransform.localScale.z);
                            rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, currentValue);
                            break;
                    }
                    break;
                
                case SPRINGTTYPE.Rotate:
                    switch(springEffect.rotateType)
                    {
                        case ROTATETYPE.RotateX:
                            rectTransform.eulerAngles = new Vector3(currentValue, rectTransform.eulerAngles.y, rectTransform.eulerAngles.z);
                            break;
                        case ROTATETYPE.RotateY:
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, currentValue, rectTransform.eulerAngles.z);
                            break;
                        case ROTATETYPE.RotateZ:
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, currentValue);
                            break;
                        case ROTATETYPE.RotateXY:
                            rectTransform.eulerAngles = new Vector3(currentValue, rectTransform.eulerAngles.y, rectTransform.eulerAngles.z);
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, currentValue, rectTransform.eulerAngles.z);
                            break;
                        case ROTATETYPE.RotateXZ:
                            rectTransform.eulerAngles = new Vector3(currentValue, rectTransform.eulerAngles.y, rectTransform.eulerAngles.z);
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, currentValue);
                            break;
                        case ROTATETYPE.RotateYZ:
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, currentValue, rectTransform.eulerAngles.z);
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, currentValue);
                            break;
                        case ROTATETYPE.RotateXYZ:
                            rectTransform.eulerAngles = new Vector3(currentValue, rectTransform.eulerAngles.y, rectTransform.eulerAngles.z);
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, currentValue, rectTransform.eulerAngles.z);
                            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, currentValue);
                            break;
                    }
                    break;
            }
            yield return null;
        }
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }
}
