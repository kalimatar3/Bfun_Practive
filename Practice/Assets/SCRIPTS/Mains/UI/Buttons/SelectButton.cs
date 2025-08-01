using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : BaseClickyButton
{
    public LevelData LevelData
    {
        set
        {
            levelData = value;
        }
        get
        {
            return levelData;
        }
    }
    [SerializeField] protected LevelData levelData;
    [SerializeField] protected Transform IconLock;
    protected override void LoadUIComponents()
    {
        base.LoadUIComponents();
        Back = GetComponent<Image>();
    }
    [SerializeField] protected Image Back;
    public override void OnClick()
    {
        if (!levelData.isunlock) levelData.isunlock = true;
    }
    public void Active()
    {
        Back.color = Color.black;
    }
    public void InActive()
    {
        Back.color = Color.white;
    }

    protected override List<Signal> UpdateVirtualCaller()
    {
        return new List<Signal>()
        {
            this.buttonSignal
        };
    }
    public override void UpdateVirtual(SignalMessage caller)
    {
    }
}
