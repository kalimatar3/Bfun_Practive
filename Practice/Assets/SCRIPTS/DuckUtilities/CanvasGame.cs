using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DuckGame.Ultilities;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.Playables;
using Unity.VisualScripting;
using System.Data.Common;

public class StuntString
{
    public string stuntName;
    public string distance;
    public int money;

    public StuntString(string stuntName, string distance, int money)
    {
        this.stuntName = stuntName;
        this.distance = distance;
        this.money = money;
    }
}

public class CanvasGame : CanvasBase
{

    CanvasGroup canvasGroup;
    Queue<StuntString> stuntQueue = new Queue<StuntString>();

    public Message MissionNotice;
    protected Coroutine DelayCloseMissionNotifyCr;
    public bool IsCanvas
    {
        get { return canvasGroup.alpha == 1; }
    }

    public override void Awake()
    {
        base.Awake();

    }
    public void NoticeMission(string message)
    {
        PopupManager.Popup.ShowPopup(PopupGame.Notify);
        this.MissionNotice.message = message;
        if (DelayCloseMissionNotifyCr != null) StopCoroutine(DelayCloseMissionNotifyCr);
        DelayCloseMissionNotifyCr = StartCoroutine(IDelayCloseMissionNotify());
    }
    protected IEnumerator IDelayCloseMissionNotify()
    {
        yield return new WaitForSeconds(1f);
        PopupManager.Popup.ClosePopup(PopupGame.Notify);
    }
}
