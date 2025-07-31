using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DuckGame.Ultilities;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : Basepanel
{
    [SerializeField] protected Transform TransformButonHolder;
    [SerializeField] protected List<SelectButton> listbuttons;
    [SerializeField] protected Image Back;
    protected override void LoadUIComponents()
    {
    }
    protected override List<Signal> UpdateVirtualCaller()
    {
        List<Signal> actionableUIs = new List<Signal>();
        foreach (var ele in listbuttons)
        {
            actionableUIs.Add(ele.buttonSignal);
        }
        return actionableUIs;
    }
    public void TweenImageToBlack()
    {
        if (Back == null) return;
        Back.DOKill();
        Back.color = Color.white;
        Back.DOColor(Color.black, .5f);
    }
    protected void FetchingData()
    {
        for (int i = 0; i < listbuttons.Count; i++)
        {
            listbuttons[i].LevelData = DataManager.Instance.levelDynamicData.levelDatas[i];
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.FetchingData();
        this.Testupdate(new SignalMessage());
    }

    public override void UpdateVirtual(SignalMessage caller)
    {
        TweenImageToBlack();
        foreach (var ele in listbuttons)
        {
            if (ele.ID == caller.ROOTID)
            {
                ele.Active();
            }
            else{
                ele.InActive();
            }
        }

    }
}
