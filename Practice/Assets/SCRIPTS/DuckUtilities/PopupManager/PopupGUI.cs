// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Sirenix.OdinInspector;
// using DG.Tweening;
// using UnityEngine.UI;
// using TMPro;
// using System.IO;
// using UnityEditor;
// using UnityEngine.Events;
// using UnityEngine.AddressableAssets;

// #if UNITY_EDITOR
// using Sirenix.OdinInspector.Editor;
// #endif

// namespace DuckGame.Ultilities
// {
//     [HideMonoScript]
//     public class PopupGUI : MonoBehaviour
//     {
//         [HideLabel]
//         [PreviewField(100, ObjectFieldAlignment.Center)]
//         public Sprite Icon;

//         [TitleGroup("POPUP MANAGER", "@DuckGame", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]

//         [SerializeField] GUITEMPLATE canvasType;
        
//         [OnCollectionChanged("AfterListChanged")]
//         [Searchable] [SerializeField] List<Popups> Popups = new List<Popups>();

//         Dictionary<string, Popups> popupDataDics = new Dictionary<string, Popups>();

// #if UNITY_EDITOR
//         [Button(ButtonSizes.Large)] [GUIColor(0,1,0)]
//         void SAVE() {
//             foreach(Popups popups in Popups)
//             {
//                 if(popups.panelGO.name == "NewPanel") popups.panelGO.name = "--------------< " + popups.popupName + "Panel >--------------";
//             }

//             string filePathAndName = "Assets/DuckUtilities/PopupManager/Popup" + canvasType + ".cs";

//             using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
//             {
//                 streamWriter.WriteLine("namespace DuckGame.Ultilities");
//                 streamWriter.WriteLine("{");
//                 streamWriter.WriteLine("public class Popup" + canvasType);
//                 streamWriter.WriteLine("{");
//                 for (int i = 0; i < Popups.Count; i++)
//                 {
//                     streamWriter.WriteLine("\n public static readonly string " + Popups[i].popupName + " = " + "\"" + Popups[i].popupName + "\"" + ";");
//                 }
//                 streamWriter.WriteLine("}");
//                 streamWriter.WriteLine("}");
//             }
            
//             AssetDatabase.Refresh();            
//         }

//         public void AfterListChanged(CollectionChangeInfo info, object value)
//         {
//             if (info.ChangeType == CollectionChangeType.Add)
//             {
//                 Popups popup = info.Value as Popups;
//                 popup.panelGO = new GameObject("NewPanel");
//                 popup.panelGO.layer = LayerMask.NameToLayer("UI");
//                 popup.panelGO.transform.parent = transform.GetChild(1);
//                 RectTransform rect = popup.panelGO.AddComponent<RectTransform>();

//                 int valuePos = 2500 * (transform.GetChild(1).childCount);

//                 rect.anchorMin = Vector2.zero;
//                 rect.anchorMax = Vector2.one;
//                 rect.offsetMin = new Vector2(valuePos, 0);
//                 rect.offsetMax = new Vector2(valuePos, 0);
//             }

//             else if (info.ChangeType == CollectionChangeType.RemoveIndex)
//             {
//                 //DuckHelper.LogEditor("Delete " + transform.GetChild(1).GetChild(info.Index).gameObject.name + " Panel");
//                 //DestroyImmediate(transform.GetChild(1).GetChild(info.Index).gameObject);
//             }
//         }
// #endif



//         private void Awake() {
//             foreach(Popups popup in Popups) {
//                 popupDataDics.Add(popup.popupName, popup);
//             }
//         }

//         public void DoMoveCustom(Settings effect,float time, RectTransform rect, PopupData data, Ease ease,RectTransform moveFrom, bool loop, bool ignoreTimeScale)
//         {
//             rect.localPosition = new Vector2(moveFrom.localPosition.x, moveFrom.localPosition.y);
//             if (!loop)
//                 rect.DOLocalMove(data.initPos, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(data.initPos, time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveBelowIn(Settings effect,float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale) {
//             rect.localPosition = new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100));
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(data.initPos.x, data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveBelowOut(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             rect.localPosition = data.initPos;
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100)), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(rect.localPosition.x, -((Screen.height / 2) + (rect.sizeDelta.y / 2) + 100)), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveAboveIn(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, SAFREAREA safeArea, bool ignoreTimeScale) {    
//             rect.localPosition = new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100);
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(data.initPos.x, safeArea == SAFREAREA.Yes ? data.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(data.initPos.x, safeArea == SAFREAREA.Yes ? data.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveAboveOut(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             rect.localPosition = data.initPos;
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(rect.localPosition.x, (Screen.height / 2) + (rect.sizeDelta.y / 2) + 100), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveRightIn(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, SAFREAREA safeArea, bool ignoreTimeScale) {
//             rect.localPosition = new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, safeArea == SAFREAREA.Yes ? rect.localPosition.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : rect.localPosition.y);
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(data.initPos.x, safeArea == SAFREAREA.Yes ? data.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(data.initPos.x, safeArea == SAFREAREA.Yes ? data.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }   

//         public void DoMoveRightOut(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             if(!loop)
//                 rect.DOLocalMove(new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100, rect.localPosition.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveLeftIn(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, SAFREAREA safeArea, bool ignoreTimeScale) {
//             rect.localPosition = new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), safeArea == SAFREAREA.Yes ? rect.localPosition.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : rect.localPosition.y);
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(data.initPos.x, safeArea == SAFREAREA.Yes ? data.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : data.initPos.y), time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(data.initPos.x, safeArea == SAFREAREA.Yes ? data.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical() : data.initPos.y), time).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         public void DoMoveLeftOut(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             if(!loop)
//                 rect.DOLocalMove(new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y), time, false).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalMove(new Vector2(-((Screen.width / 2) + (rect.sizeDelta.x / 2) + 100), rect.localPosition.y), time, false).SetEase(ease).SetLoops(effect.loopCircleMove).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         void DoRotate(Settings effect,float time, RectTransform rect, PopupData data, Ease ease,bool loop, bool ignoreTimeScale)
//         {
//             if (!loop)
//                 rect.DOLocalRotate(effect.rotateTo, time, RotateMode.FastBeyond360).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOLocalRotate(effect.rotateTo, time, RotateMode.FastBeyond360).SetEase(ease).SetLoops(effect.loopCircleRotate).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         void DoScale(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             rect.localScale = effect.from;
//             if (!loop)
//                 rect.DOScale(effect.scaleTo, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 rect.DOScale(effect.scaleTo, time).SetEase(ease).SetLoops(effect.loopCircleScale).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         void DoShake(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             if (!loop)
//             {
//                 if (effect.shakePosition)
//                     rect.DOShakePosition(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.shakeRotation)
//                     rect.DOShakeRotation(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.shakeScale)
//                     rect.DOShakeScale(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             }
//             else
//             {
//                 if (effect.shakePosition)
//                     rect.DOShakePosition(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.shakeRotation)
//                     rect.DOShakeRotation(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.shakeScale)
//                     rect.DOShakeScale(time, effect.shakeStrength, effect.shakeVibrate, effect.shakeRandomness).SetEase(ease).SetLoops(effect.loopCircleShake).SetId(1).SetUpdate(ignoreTimeScale);
//             }
//         }

//         void DoPunch(Settings effect, float time, RectTransform rect, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             if (!loop)
//             {
//                 if (effect.punchPostion)
//                     rect.DOPunchPosition(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.punchRotation)
//                     rect.DOPunchRotation(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.punchScale)
//                     rect.DOPunchScale(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             }
//             else
//             {
//                 if (effect.punchPostion)
//                     rect.DOPunchPosition(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.punchRotation)
//                     rect.DOPunchRotation(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale);

//                 if (effect.punchScale)
//                     rect.DOPunchScale(effect.punchTo, time, effect.punchVibrate, effect.punchElasticity).SetEase(ease).SetLoops(effect.loopCirclePunch).SetId(1).SetUpdate(ignoreTimeScale);
//             }
//         }

//         void DoFade(Settings effect, float time, CanvasGroup canvas, PopupData data, Ease ease, bool loop, bool ignoreTimeScale)
//         {
//             if(!loop)
//                 canvas.DOFade(effect.fadeTo, time).SetEase(ease).SetId(1).SetUpdate(ignoreTimeScale);
//             else
//                 canvas.DOFade(effect.fadeTo, time).SetEase(ease).SetLoops(effect.loopCircleFade).SetId(1).SetUpdate(ignoreTimeScale);
//         }

//         void DisablePopup(string popupName, PopupData popupData)
//         {
//             if(popupData.popupGameObject == null)
//             {
//                 DuckHelper.LogEditor(popupName + " is null => can't destroy");
//                 return;
//             }

//             popupData.popupGameObject.SetActive(false);
//             popupData.popupGameObject.transform.SetParent(popupData.initParent);
//             popupData.popupGameObject.transform.localScale = popupData.initScale;
//             popupData.popupGameObject.GetComponent<RectTransform>().localPosition = popupData.realPos;
//         }

//         public void ClosePopup(string popupName, int[] dontClose)
//         {
//             if(popupDataDics.Count > 0)
//                 StartCoroutine(EndPopupSequence(popupName, dontClose));
//         }

//         public void ClosePopup(string popupName, int[] endChoseMuls, int[] enMuls)
//         {
//             if(popupDataDics.Count > 0)
//                 StartCoroutine(EndPopupSequence(popupName, null, endChoseMuls, enMuls));
//         }

//         public void ClosePopupIgnoreDelay(string popupName, int[] ignoreDelays, int[] newDelays)
//         {
//             if(popupDataDics.Count > 0)
//                 StartCoroutine(EndPopupSequence(popupName, null, null, null, ignoreDelays, newDelays));
//         }

//         public void ShowPopupIgnoreDelay(string popupName, int[] ignoreDelays, int[] newDelays)
//         {
//             if(popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName, null, null, null, ignoreDelays, newDelays));
//         }

//         public void ClosePopup(string popupName)
//         {
//             if(popupDataDics.Count > 0)
//                 StartCoroutine(EndPopupSequence(popupName));
//         }

//         public void ClosePopupNoInvoke(string popupName)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(EndPopupSequence(popupName, null, null, null, null, null, false, false));
//         }

//         public void ClosePopupIgnoreTimeScale(string popupName)
//         {
//             if(popupDataDics.Count > 0)
//                 StartCoroutine(EndPopupSequence(popupName, null, null, null, null, null, true));
//         }

//         public void ShowPopupThenClose(string popupName, float timeDelayClose)
//         {
//             if (popupDataDics.Count > 0)
//             {
//                 StartCoroutine(StartPopupSequence(popupName));
//                 StartCoroutine(CloseAfter(popupName, timeDelayClose));
//             }
//         }

//         IEnumerator CloseAfter(string popupName, float time)
//         {
//             yield return new WaitForSeconds(time);
//             ClosePopup(popupName);
//         }

//         public void ShowPopup(string popupName)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName));
//         }

//         public void ShowPopupNoInvoke(string popupName)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName, null, null, null, null, null, false, false, new Vector2(), null, false));
//         }

//         public void ShowPopupIgnoreTimeScale(string popupName)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName, null, null, null, null, null, true));
//         }

//         public void ShowPopupCustomInitPos(string popupName, Vector2 customInitPos, int[] customIniPoss)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName, null, null, null, null, null, false, true, customInitPos, customIniPoss));
//         }

//         public void ShowPopup(string popupName, int[] endChoseMuls, int[] enMuls)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName, null, endChoseMuls, enMuls));
//         }

//         public void ShowPopup(string popupName, int[] dontShow)
//         {
//             if (popupDataDics.Count > 0)
//                 StartCoroutine(StartPopupSequence(popupName, dontShow));
//         }

//         IEnumerator StartPopupSequence(string popupName, int[] dontShow = null, int[] mulPopups = null, int[] startMuls = null, int[] ignoreDelays = null, int[] newDelays = null, bool ignoreTimeScale = false, bool useCustomInitPos = false, Vector2 customInitPos = new Vector2(), int[] customInitPoss = null, bool invokeEvent = true)
//         {
//             Popups popups = popupDataDics[popupName];
//             if(popups.IsShowed) yield break;
//             popups.IsShowed = true;
//             bool ignoreDelay = false;
//             bool skip = false;
//             int i = 0;
//             PopupData[] popupDatas = popupDataDics[popupName].popups;
//             foreach (PopupData popupData in popupDatas)
//             {
//                 bool isMul = false;
//                 bool isCustomInitPos = false;
//                 skip = false;
//                 if(dontShow != null)
//                 {
//                     for(int j = 0; j < dontShow.Length; j++)
//                     {
//                         if(i == dontShow[j])
//                         {
//                             skip = true;
//                             break;
//                         }
//                     }
//                 }
//                 i++;
//                 if(skip) continue;

//                 if(mulPopups != null)
//                 {
//                     for(int k = 0; k < mulPopups.Length; k++)
//                     {
//                         if(i - 1 == mulPopups[k])
//                         { 
//                             isMul = true;
//                             break;
//                         }
//                     }
//                 }

//                 if(customInitPoss != null)
//                 {
//                     for(int h = 0; h < customInitPoss.Length; h++)
//                     {
//                         if(i - 1 == customInitPoss[h])
//                         { 
//                             isCustomInitPos = true;
//                             break;
//                         }
//                     }
//                 }

//                 if(ignoreDelays != null)
//                 {
//                     for(int l = 0; l < ignoreDelays.Length; l++)
//                     {
//                         if(i - 1 == ignoreDelays[l])
//                         { 
//                             ignoreDelay = true;
//                             break;
//                         }
//                     }
//                 }

//                 StartCoroutine(PopupDelaySound(popupData.soundDelayType == SOUNDDELAY.SeperateDelay ? popupData.soundDelayStart : popupData.showDelay, popupData, false));
//                 if (popupData.startEffect.Effect.Count > 0)
//                 {
//                     if(isMul && !ignoreDelay)
//                         StartCoroutine(StartEffectSequence(popupData, isMul, startMuls[i - 1], false, 0, ignoreTimeScale, useCustomInitPos, customInitPos, invokeEvent));
//                     else if(!isMul && !ignoreDelay)
//                         StartCoroutine(StartEffectSequence(popupData, false, -1, false, 0, ignoreTimeScale, useCustomInitPos, customInitPos, invokeEvent));
//                     else if(!isMul && ignoreDelay)
//                         StartCoroutine(StartEffectSequence(popupData, false, -1, true, newDelays[i - 1], ignoreTimeScale, useCustomInitPos, customInitPos, invokeEvent));
//                     else if(isMul && !ignoreDelay && isCustomInitPos)
//                         StartCoroutine(StartEffectSequence(popupData, isMul, startMuls[i - 1], false, 0, ignoreTimeScale, true, customInitPos, invokeEvent));
//                     else if(!isMul && !ignoreDelay && isCustomInitPos)
//                         StartCoroutine(StartEffectSequence(popupData, false, -1, false, 0, ignoreTimeScale, true, customInitPos, invokeEvent));
//                     else if(!isMul && ignoreDelay && isCustomInitPos)
//                         StartCoroutine(StartEffectSequence(popupData, false, -1, true, newDelays[i - 1], false, true, customInitPos, invokeEvent));
//                 }
//                 yield return null;
//             }
//         }

//         IEnumerator EndPopupSequence(string popupName, int[] dontClose = null, int[] mulPopups = null, int[] endMuls = null, int[] ignoreDelays = null, int[] newDelays = null, bool ignoreTimeScale = false, bool invokeEvent = true)
//         {
//             Popups popups = popupDataDics[popupName];
//             if(!popups.IsShowed) yield break;
//             popups.IsShowed = false;
//             bool ignoreDelay = false;
//             bool cantClose = false;
//             bool skip = false;
//             int i = 0;
//             PopupData[] popupDatas = popupDataDics[popupName].popups;
//             foreach (PopupData popupData in popupDatas)
//             {
//                 if(!popupData.popupGameObject.activeInHierarchy) break;
//                 bool isMul = false;
//                 skip = false;
//                 if(dontClose != null)
//                 {
//                     for(int j = 0; j < dontClose.Length; j++)
//                     {
//                         if(i == dontClose[j])
//                         {
//                             skip = true;
//                             break;
//                         }
//                     }
//                 }
//                 i++;
//                 if(skip) continue;
        
//                 StartCoroutine(PopupDelaySound(popupData.soundDelayType == SOUNDDELAY.SeperateDelay ? popupData.soundDelayEnd : popupData.closeDelay, popupData, true));
                
//                 if(mulPopups != null)
//                 {
//                     for(int k = 0; k < mulPopups.Length; k++)
//                     {
//                         if(i - 1 == mulPopups[k])
//                         { 
//                             isMul = true;
//                             break;
//                         }
//                     }
//                 }

//                 if(ignoreDelays != null)
//                 {
//                     for(int l = 0; l < ignoreDelays.Length; l++)
//                     {
//                         if(i - 1 == ignoreDelays[l])
//                         { 
//                             ignoreDelay = true;
//                             break;
//                         }
//                     }
//                 }

//                 if (popupData.endEffect.Effect.Count > 0)
//                 {
//                     if(isMul && !ignoreDelay)
//                         StartCoroutine(EndEffectSequence(popupName, popupData, cantClose, isMul, endMuls[i - 1], false, 0, ignoreTimeScale, invokeEvent));
//                     else if(!isMul && !ignoreDelay)
//                         StartCoroutine(EndEffectSequence(popupName, popupData, cantClose, false, -1, false, 0, ignoreTimeScale, invokeEvent));
//                     else if(!isMul && ignoreDelay)
//                         StartCoroutine(EndEffectSequence(popupName, popupData, cantClose, false, -1, true, newDelays[i - 1], ignoreTimeScale, invokeEvent));
//                 }
//                 yield return null;
//             }
//         }
        
//         IEnumerator PopupDelaySound(float time, PopupData popupData, bool end)
//         {
//             yield return new WaitForSeconds(time);
//             switch(popupData.soundTrigger)
//                 {
//                     case SOUNDTRIGGER.Same:
//                         SoundManager.Instance.PlaySound(popupData.startAndEndSound, AudioSetting.Default);
//                         break;
//                     case SOUNDTRIGGER.Seperate:
//                         SoundManager.Instance.PlaySound(end ? popupData.endSound : popupData.startSound, AudioSetting.Default);
//                         break;
//                 }
//         }

//         IEnumerator EndEffectSequence(string popupName, PopupData popupData, bool cantClose, bool multiEffect = false, int mulEnd = -1,bool ignoreDelay = false, int newDelay = 0, bool ignoreTimeScale = false, bool invokeEvent = true)
//         {
//             if(popupData.popupGameObject == null) yield break;
//             if (invokeEvent) popupData.onStartClose?.Invoke();
//             yield return ignoreTimeScale ? new WaitForSecondsRealtime(ignoreDelay ? newDelay : popupData.closeDelay) : new WaitForSeconds(ignoreDelay ? newDelay : popupData.closeDelay);
//             if (invokeEvent) popupData.onClose?.Invoke();
//             int i = 0;

//             foreach (Settings effect in (multiEffect ? popupData.multpleEndEffect[mulEnd].Effect : popupData.endEffect.Effect))
//             {
//                 i++;

//                 if(effect.type == EFFECT.Move && effect.moveType == MOVEEFFECT.Custom)
//                     cantClose = true;

//                 switch (effect.type)
//                 {
//                     case EFFECT.Move:
//                         switch(effect.moveType)
//                         {
//                             case MOVEEFFECT.Custom:
//                                 DoMoveCustom(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.moveFrom, effect.loopMove, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromAbove:
//                                 DoMoveAboveOut(effect, effect.timeMove,popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromBelow:
//                                 DoMoveBelowOut(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromLeft:
//                                 DoMoveLeftOut(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromRight:
//                                 DoMoveRightOut(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, ignoreTimeScale);
//                                 break;
//                         }
//                         if (effect.moveActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeMove) : new WaitForSeconds(effect.delayTimeMove));
//                         else if (effect.moveActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopMove ? effect.timeMove * effect.loopCircleMove : effect.timeMove) : new WaitForSeconds(effect.loopMove ? effect.timeMove * effect.loopCircleMove : effect.timeMove));
//                         else if(effect.moveActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Rotate:
//                         DoRotate(effect, effect.timeRotate, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeRotate, effect.loopRotate, ignoreTimeScale);
//                         if (effect.rotateActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeRotate) : new WaitForSeconds(effect.delayTimeRotate));
//                         else if (effect.rotateActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopRotate ? effect.timeRotate * effect.loopCircleRotate : effect.timeRotate) : new WaitForSeconds(effect.loopRotate ? effect.timeRotate * effect.loopCircleRotate : effect.timeRotate));
//                         else if (effect.rotateActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Scale:
//                         DoScale(effect, effect.timeScale, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeScale, effect.loopScale, ignoreTimeScale);
//                         if (effect.scaleActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeScale) : new WaitForSeconds(effect.delayTimeScale));
//                         else if (effect.scaleActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopScale ? effect.timeScale * effect.loopCircleScale : effect.timeScale) : new WaitForSeconds(effect.loopScale ? effect.timeScale * effect.loopCircleScale : effect.timeScale));
//                         else if (effect.scaleActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Shake:
//                         DoShake(effect, effect.timeShake, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeShake, effect.loopShake, ignoreTimeScale);
//                         if (effect.shakeActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeShake) : new WaitForSeconds(effect.delayTimeShake));
//                         else if (effect.shakeActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopShake ? effect.timeShake * effect.loopCircleShake : effect.timeShake) : new WaitForSeconds(effect.loopShake ? effect.timeShake * effect.loopCircleShake : effect.timeShake));
//                         else if (effect.shakeActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Punch:
//                         DoPunch(effect, effect.timePunch, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easePunch, effect.loopPunch, ignoreTimeScale);
//                         if (effect.punchActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimePunch) : new WaitForSeconds(effect.delayTimePunch));
//                         else if (effect.punchActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopPunch ? effect.timePunch * effect.loopCirclePunch : effect.timePunch) : new WaitForSeconds(effect.loopPunch ? effect.timePunch * effect.loopCirclePunch : effect.timePunch));
//                         else if (effect.punchActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;

//                     case EFFECT.Fade:
//                         DoFade(effect, effect.timeFade, popupData.popupGameObject.GetComponent<CanvasGroup>(), popupData, effect.easeFade, effect.loopFade, ignoreTimeScale);
//                         if (effect.fadeActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeFade) : new WaitForSeconds(effect.delayTimeFade));
//                         else if (effect.fadeActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopFade ? effect.timeFade * effect.loopCircleFade : effect.timeFade) : new WaitForSeconds(effect.loopFade ? effect.timeFade * effect.loopCircleFade : effect.timeFade));
//                         else if (effect.fadeActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                 }

//                 if (i == popupData.endEffect.Effect.Count && !effect.loopFade && !effect.loopMove && !effect.loopPunch && !effect.loopRotate && !effect.loopScale && !effect.loopShake && !cantClose)
//                 {
//                     switch(effect.type)
//                     {
//                         case EFFECT.Move:
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeMove) : new WaitForSeconds(effect.delayTimeMove));
//                             break;
//                         case EFFECT.Fade:
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeFade) : new WaitForSeconds(effect.delayTimeFade));
//                             break;
//                         case EFFECT.Nothing:
//                             yield return null;
//                             break;
//                         case EFFECT.Punch:
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimePunch) : new WaitForSeconds(effect.delayTimePunch));
//                             break;
//                         case EFFECT.Rotate:
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeRotate) : new WaitForSeconds(effect.delayTimeRotate));
//                             break;
//                         case EFFECT.Scale:
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeScale) : new WaitForSeconds(effect.delayTimeScale));
//                             break;
//                         case EFFECT.Shake:
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeShake) : new WaitForSeconds(effect.delayTimeShake));
//                             break;
//                     }
//                     if(effect.type == EFFECT.Move)
//                     {
//                         if(effect.moveType != MOVEEFFECT.Custom)
//                         {
//                             DisablePopup(popupName, popupData);
//                         }
//                     }
//                     else 
//                     {
//                         DisablePopup(popupName, popupData);
//                     }
//                 }
//             }
//             if (invokeEvent) popupData.onEndClose?.Invoke();
//         }

//         IEnumerator StartEffectSequence(PopupData popupData, bool multiEffect = false, int mulStart = -1, bool ignoreDelay = false, int newDelay = 0, bool ignoreTimeScale = false, bool useCustomInitPos = false, Vector2 customInitPos = new Vector2(), bool invokeEvent = true)
//         {
//             if(invokeEvent) popupData.onStartShow?.Invoke();
//             yield return ignoreTimeScale ? new WaitForSecondsRealtime(ignoreDelay ? newDelay : popupData.showDelay) : new WaitForSeconds(ignoreDelay ? newDelay : popupData.showDelay);

//             if (!popupData.popupGameObject.GetComponent<CanvasGroup>())
//                 popupData.popupGameObject.AddComponent<CanvasGroup>();
                
//             float offSet = popupData.popupGameObject.transform.parent.GetComponent<RectTransform>().localPosition.x;

//             popupData.initParent = popupData.popupGameObject.transform.parent;
//             popupData.realPos = popupData.popupGameObject.GetComponent<RectTransform>().localPosition;

//             bool isNotSameParent = false;
//             if(popupData.popupGameObject.transform.parent != popupData.spawnPoint)
//             {
//                 isNotSameParent = false;
//                 popupData.popupGameObject.transform.SetParent(popupData.spawnPoint);
//             }
//             else 
//             {
//                 isNotSameParent = true;
//             }

//             if (!useCustomInitPos)
//             { 
//                 if(!isNotSameParent)
//                     popupData.popupGameObject.GetComponent<RectTransform>().localPosition = popupData.initPos = new Vector2(popupData.popupGameObject.GetComponent<RectTransform>().localPosition.x - offSet, popupData.popupGameObject.GetComponent<RectTransform>().localPosition.y);
//             }
//             else
//             {
//                 popupData.popupGameObject.GetComponent<RectTransform>().localPosition = popupData.initPos = new Vector2(customInitPos.x , customInitPos.y);
//             }

//             popupData.initScale = popupData.popupGameObject.GetComponent<RectTransform>().localScale;
//             popupData.initFade = popupData.popupGameObject.GetComponent<CanvasGroup>().alpha;

//             if (!popupData.popupGameObject.activeInHierarchy)
//             {
//                 popupData.popupGameObject.SetActive(true);
//             }

//             if (invokeEvent) popupData.onShow?.Invoke();

//             foreach(Settings effect in (multiEffect ? popupData.multpleStartEffect[mulStart].Effect : popupData.startEffect.Effect))
//             {
//                 switch(effect.type)
//                 {
//                     case EFFECT.Move:
//                         if (popupData.keepPosAtInit == KEEPPOSATINIT.No)
//                         {
//                             switch (effect.moveType)
//                             {
//                                 case MOVEEFFECT.FromBelow:
//                                     if (popupData.safeArea == SAFREAREA.Yes) popupData.initPos = new Vector2(popupData.initPos.x + GUIManager.Instance.canvasMultiScene.SafeAreaOffsetHorizontal(), popupData.initPos.y);
//                                     popupData.popupGameObject.GetComponent<RectTransform>().localPosition = new Vector2(popupData.popupGameObject.GetComponent<RectTransform>().localPosition.x, -((Screen.height / 2) + (popupData.popupGameObject.GetComponent<RectTransform>().sizeDelta.y / 2) + 100));
//                                     break;
//                                 case MOVEEFFECT.FromAbove:
//                                     if (popupData.safeArea == SAFREAREA.Yes) popupData.initPos = new Vector2(popupData.initPos.x + GUIManager.Instance.canvasMultiScene.SafeAreaOffsetHorizontal(), popupData.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical());
//                                     popupData.popupGameObject.GetComponent<RectTransform>().localPosition = new Vector2(popupData.popupGameObject.GetComponent<RectTransform>().localPosition.x, (Screen.height / 2) + (popupData.popupGameObject.GetComponent<RectTransform>().sizeDelta.y / 2) + 100);
//                                     break;
//                                 case MOVEEFFECT.FromLeft:
//                                     if (popupData.safeArea == SAFREAREA.Yes) popupData.initPos = new Vector2(popupData.initPos.x + GUIManager.Instance.canvasMultiScene.SafeAreaOffsetHorizontal(), popupData.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical());
//                                     popupData.popupGameObject.GetComponent<RectTransform>().localPosition = new Vector2(-((Screen.width / 2) + (popupData.popupGameObject.GetComponent<RectTransform>().sizeDelta.x / 2) + 100), popupData.popupGameObject.GetComponent<RectTransform>().localPosition.y);
//                                     break;
//                                 case MOVEEFFECT.FromRight:
//                                     if (popupData.safeArea == SAFREAREA.Yes) popupData.initPos = new Vector2(popupData.initPos.x, popupData.initPos.y - GUIManager.Instance.canvasMultiScene.SafeAreaOffsetVertical());
//                                     popupData.popupGameObject.GetComponent<RectTransform>().localPosition = new Vector2((Screen.width / 2) + (popupData.popupGameObject.GetComponent<RectTransform>().sizeDelta.x / 2) + 100, popupData.popupGameObject.GetComponent<RectTransform>().localPosition.y);
//                                     break;
//                             }
//                         }
//                         break;
//                     case EFFECT.Scale:
//                         popupData.popupGameObject.transform.localScale = effect.from;
//                         break;
//                     case EFFECT.Fade:
//                         popupData.popupGameObject.GetComponent<CanvasGroup>().alpha = 0;
//                         break;
//                 }
//             }

//             int i = 0;
//             foreach (Settings effect in (multiEffect ? popupData.multpleStartEffect[mulStart].Effect : popupData.startEffect.Effect))
//             {
//                 i++;
//                 switch (effect.type)
//                 {
//                     case EFFECT.Move:
//                         switch (effect.moveType)
//                         {
//                             case MOVEEFFECT.Custom:
//                                 DoMoveCustom(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.moveFrom, effect.loopMove, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromAbove:
//                                 DoMoveAboveIn(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, popupData.safeArea, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromBelow:
//                                 DoMoveBelowIn(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromLeft:
//                                 DoMoveLeftIn(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, popupData.safeArea, ignoreTimeScale);
//                                 break;

//                             case MOVEEFFECT.FromRight:
//                                 DoMoveRightIn(effect, effect.timeMove, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeMove, effect.loopMove, popupData.safeArea, ignoreTimeScale);
//                                 break;
//                         }
//                         if (effect.moveActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeMove) : new WaitForSeconds(effect.delayTimeMove));
//                         else if (effect.moveActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopMove ? effect.timeMove * effect.loopCircleMove : effect.timeMove) : new WaitForSeconds(effect.loopMove ? effect.timeMove * effect.loopCircleMove : effect.timeMove));
//                         else if (effect.moveActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Rotate:
//                         DoRotate(effect, effect.timeRotate, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeRotate, effect.loopRotate, ignoreTimeScale);
//                         if (effect.rotateActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeRotate) : new WaitForSeconds(effect.delayTimeRotate));
//                         else if (effect.rotateActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopRotate ? effect.timeRotate * effect.loopCircleRotate : effect.timeRotate) : new WaitForSeconds(effect.loopRotate ? effect.timeRotate * effect.loopCircleRotate : effect.timeRotate));
//                         else if (effect.rotateActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Scale:
//                         DoScale(effect, effect.timeScale, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeScale, effect.loopScale, ignoreTimeScale);
//                         if (effect.scaleActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeScale) : new WaitForSeconds(effect.delayTimeScale));
//                         else if (effect.scaleActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopScale ? effect.timeScale * effect.loopCircleScale : effect.timeScale) : new WaitForSeconds(effect.loopScale ? effect.timeScale * effect.loopCircleScale : effect.timeScale));
//                         else if (effect.scaleActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Shake:
//                         DoShake(effect, effect.timeShake, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easeShake, effect.loopShake, ignoreTimeScale);
//                         if (effect.shakeActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeShake) : new WaitForSeconds(effect.delayTimeShake));
//                         else if (effect.shakeActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopShake ? effect.timeShake * effect.loopCircleShake : effect.timeShake) : new WaitForSeconds(effect.loopShake ? effect.timeShake * effect.loopCircleShake : effect.timeShake));
//                         else if (effect.shakeActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                     case EFFECT.Punch:
//                         DoPunch(effect, effect.timePunch, popupData.popupGameObject.GetComponent<RectTransform>(), popupData, effect.easePunch, effect.loopPunch, ignoreTimeScale);
//                         if (effect.punchActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimePunch) : new WaitForSeconds(effect.delayTimePunch));
//                         else if (effect.punchActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopPunch ? effect.timePunch * effect.loopCirclePunch : effect.timePunch) : new WaitForSeconds(effect.loopPunch ? effect.timePunch * effect.loopCirclePunch : effect.timePunch));
//                         else if (effect.punchActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;

//                     case EFFECT.Fade:
//                         DoFade(effect, effect.timeFade, popupData.popupGameObject.GetComponent<CanvasGroup>(), popupData, effect.easeFade, effect.loopFade, ignoreTimeScale);
//                         if (effect.fadeActivate == ACTIVATETYPE.Delay)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeFade) : new WaitForSeconds(effect.delayTimeFade));
//                         else if (effect.fadeActivate == ACTIVATETYPE.Sequence)
//                             yield return (ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopFade ? effect.timeFade * effect.loopCircleFade : effect.timeFade) : new WaitForSeconds(effect.loopFade ? effect.timeFade * effect.loopCircleFade : effect.timeFade));
//                         else if (effect.fadeActivate == ACTIVATETYPE.Continuously)
//                             yield return null;
//                         break;
//                 }
//             }
//             if (invokeEvent) popupData.onEndShow?.Invoke();
//         }
//     }
// }
