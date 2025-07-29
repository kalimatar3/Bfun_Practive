using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DuckGame.Ultilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMultiScene : CanvasBase
{
    public RectTransform offsetReffrenceVertical;
    public RectTransform offsetReffrenceHorizontal;
    [SerializeField] Transform contentMultiCanvas;
    [HideInInspector] public float initOffsetReffrenceVertical;
    [HideInInspector] public float initOffsetReffrenceHorizontal;
   

    public float SafeAreaOffsetVertical()
    {
        return Mathf.Abs(initOffsetReffrenceVertical - offsetReffrenceVertical.localPosition.y);
    }

    public float SafeAreaOffsetHorizontal()
    {
        return Mathf.Abs(initOffsetReffrenceHorizontal - offsetReffrenceHorizontal.localPosition.x);
    }

       public override void Awake() {
        base.Awake();
        initOffsetReffrenceVertical = offsetReffrenceVertical.localPosition.y;
        initOffsetReffrenceHorizontal = offsetReffrenceHorizontal.localPosition.x;
    }

}
