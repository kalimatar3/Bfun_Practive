using DG.Tweening;
using DuckGame.Ultilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct ButtonTutorialGroup
{
    public RectTransform buttonRectTransform;
    public RectTransform buttonRectTransformParent;
}

[System.Serializable]
public struct ButtonUIData
{
    public string buttonName;
    public ButtonTutorialGroup buttonGroup;
}


[System.Serializable]
public class SentenceData
{
    [TextArea(5, 10)]
    public string sentence;
    public string buttonName;
    public bool autoSkip;
    public bool showBlackBank;
    public UnityEvent onTutorial;
    public float delay;
}

[System.Serializable]
public class Dialouge
{
    [TitleGroup("DIALOUGE", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    public string dialougeName;
    public string characterName;
    [HideLabel]
    [PreviewField(100, ObjectFieldAlignment.Center)]
    public Sprite characterSprite;
    public SentenceData[] sentences;
}


public class DialogueManager : Singleton<DialogueManager>
{
    private Queue<SentenceData> sentences;

    [SerializeField] Dialouge[] dialouges;

    public Dictionary<DialougeName, Dialouge> DialougeList = new Dictionary<DialougeName, Dialouge>();

    DialougeName currentDialouge;

    SentenceData sentenceData;

    public bool canTouchSkip;


    Coroutine autoNextCoru, waitCour;


    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
#if UNITY_EDITOR
    private void SAVEBUTTONS()
    {
        string enumName = "DialougeName";
        string filePathAndName = "Assets/DuckUtilities/DialougeManager/Dialouge/" + enumName + ".cs";

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("namespace DuckGame.Ultilities");
            streamWriter.WriteLine("{");
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < dialouges.Length; i++)
            {
                streamWriter.WriteLine("\t" + dialouges[i].dialougeName + ",");
            }
            streamWriter.WriteLine("}");
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
#endif

    public override void Awake()
    {
        base.Awake();   
        sentences = new Queue<SentenceData>();
        foreach (Dialouge dialouge in dialouges)
        {
            DialougeName diName;
            Enum.TryParse(dialouge.dialougeName, out diName);
            DialougeList.Add(diName, dialouge);
        }
    }

    public void ShowDialouge(DialougeName dialougeName)
    {
        currentDialouge = dialougeName;
        Dialouge dialouge = DialougeList[dialougeName];
        //GUIManager.Instance.canvasMultiScene.SetupCharacterTutorialData(dialouge.characterName, dialouge.characterSprite);
        //GUIManager.Instance.canvasMultiScene.ShowTutorial(true);

        sentences.Clear();

        foreach(SentenceData sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        canTouchSkip = true;
        DisplayNextSentence(true);
    }

    public void DisplayNextSentence(bool init)
    {
        StartCoroutine(IDisplayNextSentence(init));
    }

    IEnumerator IDisplayNextSentence(bool init)
    {
        if (sentences.Count == 0)
        { if (sentenceData.autoSkip)
                sentenceData.onTutorial?.Invoke();
        }
        if (sentenceData != null && sentenceData.buttonName != "")
        {
            //GUIManager.Instance.canvasMultiScene.DestroyHandTuroial();
            //GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.parent = GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransformParent;
            //GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.GetComponent<ButtonEvents>().RemoveEvent();
            //Destroy(GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.gameObject.GetComponent<NextTutorial>());
            sentenceData = null;
        }

        //GUIManager.Instance.canvasMultiScene.ShowBlackBankTutorial(false);
        if (autoNextCoru != null) StopCoroutine(autoNextCoru);
        if (waitCour != null) StopCoroutine(waitCour);

        if (sentences.Count == 0)
        {
            EndDialouge();
            yield break;
        }

        //SoundManager.Instance.PlaySound(SoundName.NotifyTutorialCharacter, AudioSetting.Default);
        sentenceData = sentences.Dequeue();

        //GUIManager.Instance.canvasMultiScene.SetTouchTutorial(sentenceData.autoSkip);

        yield return new WaitForSeconds(sentenceData.delay);

        //if (sentenceData.showBlackBank)
            //GUIManager.Instance.canvasMultiScene.ShowBlackBankTutorial(true);

        yield return null;     

        if (sentenceData.buttonName != "")
        {
        //     GUIManager.Instance.canvasMultiScene.SpawnHandTutorial(GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform);
        //     GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.parent = GUIManager.Instance.canvasMultiScene.blackBank;
        //     GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.gameObject.AddComponent<NextTutorial>();
        //     GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.GetComponent<ButtonEvents>().SetOnClick(sentenceData.onTutorial);
        //     GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.GetComponent<ButtonEvents>().AddEvent(() => GUIManager.Instance.canvasHome.buttonUIDatasDics[sentenceData.buttonName].buttonGroup.buttonRectTransform.gameObject.GetComponent<NextTutorial>().DialougeTrigger());
        }

        if (sentenceData.autoSkip)
        {
            if (init)
                waitCour = StartCoroutine(IWaitShow(sentenceData.sentence));
            else
            {
                //GUIManager.Instance.canvasMultiScene.ShowTextTutorialDialouge(sentenceData.sentence);
            }
            autoNextCoru = StartCoroutine(IAutoNext());
        }
        else
        {
            //GUIManager.Instance.canvasMultiScene.ShowTextTutorialDialouge(sentenceData.sentence);
        }
    }

    IEnumerator IWaitShow(string sentence)
    {
        yield return new WaitForSeconds(1);
        //GUIManager.Instance.canvasMultiScene.ShowTextTutorialDialouge(sentence);
    }

    IEnumerator IAutoNext()
    {
        yield return new WaitForSeconds(5);
        DisplayNextSentence(false);
    }


    private void EndDialouge()
    {
        canTouchSkip = false;
        //GUIManager.Instance.canvasMultiScene.ShowTutorial(false);
        //GUIManager.Instance.canvasMultiScene.ShowBlackBankTutorial(false);
        // switch (currentDialouge)
        // {
        //     case DialougeName.IntroOpening:
        //         GUIManager.Instance.canvasHome.SetIntroVehicleIndex(1);
        //         SoundManager.Instance.PlaySound(SoundName.CinemeticBoom, AudioSetting.Default);
        //         GameManager.Instance.homeSceneData.ShowSpawnCam(false);
        //         GUIManager.Instance.canvasHome.ShowSelectCarIntroOpening(true);
        //         GameManager.Instance.DelayFucn(0.5f, () =>
        //         {
        //             GUIManager.Instance.canvasHome.ShowVehicleInfoArrowOpening(true, true);
        //         });
        //         break;
        // }      
        // DuckHelper.LogEditor("DIALOUGE FINISH!!");
    }
}
