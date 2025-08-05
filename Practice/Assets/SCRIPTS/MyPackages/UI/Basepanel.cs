using System;
using System.Collections.Generic;
using Clouds.Ultilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public abstract class Basepanel : baseUI
{
    [Searchable][SerializeField] List<UIelement> Plots = new List<UIelement>();
    public Tween TestMove()
    {
        transform.DOKill();
        return transform.DORotate(new Vector3(0, 0, 360), .5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
    }

}