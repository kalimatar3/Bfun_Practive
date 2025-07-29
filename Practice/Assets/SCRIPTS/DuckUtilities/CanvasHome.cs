using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Events;
using System;


namespace DuckGame.Ultilities
{
    public class CanvasHome : CanvasBase
    {
        [ReadOnly] public bool isIspectMode;      
        public override void Awake()
        {
            base.Awake();
        }
    }
}
