using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor_button : BaseClickyButton
{
    private Signal signal;
    public LevelData LevelData
    {
        set
        {
            levelData = value;
            signal.Send(new SignalMessage());
        }
        get
        {
            return levelData;
        }
    }
    [SerializeField] protected LevelData levelData;
    [SerializeField] protected Transform IconLock;
    public BaseClickyButton baseButton;
    protected override void LoadUIComponents()
    {
        Back = GetComponent<Image>();
    }
    [SerializeField] protected Image Back;
    public override void OnClick()
    {
        if (!levelData.isunlock) levelData.isunlock = true;
    }
    protected override List<Signal> Caller()
    {
        return new List<Signal>()
        {
            this.buttonSignal,
            this.baseButton.buttonSignal
        };
    }
    public override void UpdateVirtual(SignalMessage caller)
    {
       if(caller.Type == SignalType.None) this.TweenToRandomColor();
    }
     public void TweenToRandomColor()
    {
        Back.DOKill();
        if (Back == null) return;
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Back.DOColor(randomColor, .3f);
    }
}
