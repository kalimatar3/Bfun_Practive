using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using Clouds.Ultilities;


#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Clouds.Ultilities
{
    [HideMonoScript]
    public class CanvasGUI : MonoBehaviour
    {
        [HideLabel]
        [PreviewField(100, ObjectFieldAlignment.Center)]
        public Sprite Icon;

        [TitleGroup("POPUP MANAGER", "@DuckGame", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]

        [SerializeField] GUITEMPLATE canvasType;

        [OnCollectionChanged("AfterListChanged")]
        [Searchable][SerializeField] List<Popup> Popups = new List<Popup>();

        Dictionary<string, Popup> uIelementDics = new Dictionary<string, Popup>();

#if UNITY_EDITOR
        [Button(ButtonSizes.Large)]
        [GUIColor(0, 1, 0)]
        void SAVE()
        {
            foreach (Popup popups in Popups)
            {
                if (popups.panelGO.name == "NewPanel") popups.panelGO.name = "--------------< " + popups.popupName + "Panel >--------------";
            }

            string filePathAndName = "Assets/DuckUtilities/PopupManager/Popup" + canvasType + ".cs";

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("namespace Clouds.Ultilities");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine("public class Popup" + canvasType);
                streamWriter.WriteLine("{");
                for (int i = 0; i < Popups.Count; i++)
                {
                    streamWriter.WriteLine("\n public static readonly string " + Popups[i].popupName + " = " + "\"" + Popups[i].popupName + "\"" + ";");
                }
                streamWriter.WriteLine("}");
                streamWriter.WriteLine("}");
            }

            AssetDatabase.Refresh();
        }

        public void AfterListChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                Popup popup = info.Value as Popup;
                popup.panelGO = new GameObject("NewPanel");
                popup.panelGO.layer = LayerMask.NameToLayer("UI");
                popup.panelGO.transform.parent = transform.GetChild(1);
                RectTransform rect = popup.panelGO.gameObject.AddComponent<RectTransform>();

                int valuePos = 2500 * (transform.GetChild(1).childCount);

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = new Vector2(valuePos, 0);
                rect.offsetMax = new Vector2(valuePos, 0);
            }

            else if (info.ChangeType == CollectionChangeType.RemoveIndex)
            {
                //DuckHelper.LogEditor("Delete " + transform.GetChild(1).GetChild(info.Index).gameObject.name + " Panel");
                //DestroyImmediate(transform.GetChild(1).GetChild(info.Index).gameObject);
            }
        }
#endif



        private void Awake()
        {
            foreach (Popup popup in Popups)
            {
                uIelementDics.Add(popup.popupName, popup);
            }
        }
        public void ShowPopup(string PopupName)
        {
            Popup popup = uIelementDics[PopupName];
            popup.Panel.transform.SetParent(popup.SpawnPoint);
            popup.Panel.transform.localPosition = Vector3.zero;
            popup.Panel.gameObject.SetActive(true);
            SignalMessage EnableMessage = new SignalMessage();
            EnableMessage.Type = SignalName.OnEnable;
            popup.Panel.EnableSignal.Send(EnableMessage);
        }
        public void ClosePopup(string PopupName)
        {
            Popup popup = uIelementDics[PopupName];
            popup.Panel.transform.SetParent(popup.SpawnPoint);
            popup.Panel.gameObject.SetActive(true);
            SignalMessage DisableMessge = new SignalMessage();
            DisableMessge.Type = SignalName.OnDisable;
            popup.Panel.EnableSignal.Send(DisableMessge);
            StartCoroutine(IEndPopup(popup));
        }
        void DisablePopup(Popup popupData)
        {
            if(popupData.Panel == null)
            {
                return;
            }
            popupData.Panel.gameObject.SetActive(false);
            popupData.Panel.gameObject.transform.SetParent(popupData.panelGO.transform);
        }
        protected IEnumerator IEndPopup(Popup popup)
        {
            int i = 0;
            if (!popup.Panel.UIGroupDics.ContainsKey(SignalName.OnDisable))
            {
                this.DisablePopup(popup);
                yield break;                
            }
            foreach (UIelement uIelement in popup.Panel.UIGroupDics[SignalName.OnDisable].UIS)
            {
                foreach (UIEffectData effect in uIelement.Effect)
                {
                    i++;
                    switch (effect.type)
                    {
                        case TRIGGEREFFECT.Move:
                            if (effect.moveActivate == ACTIVATETYPE.Delay)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeMove) : new WaitForSeconds(effect.delayTimeMove));
                            else if (effect.moveActivate == ACTIVATETYPE.Sequence)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopMove ? effect.timeMove * effect.loopCircleMove : effect.timeMove) : new WaitForSeconds(effect.loopMove ? effect.timeMove * effect.loopCircleMove : effect.timeMove));
                            else if (effect.moveActivate == ACTIVATETYPE.Continuously)
                                yield return null;
                            break;
                        case TRIGGEREFFECT.Rotate:
                            //                            UIUtility.DoRotate(effect, effect.timeRotate, uIelement.UIObj.GetComponent<RectTransform>(), uIelement, effect.easeRotate, effect.loopRotate, uIelement.ignoreTimeScale);
                            if (effect.rotateActivate == ACTIVATETYPE.Delay)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeRotate) : new WaitForSeconds(effect.delayTimeRotate));
                            else if (effect.rotateActivate == ACTIVATETYPE.Sequence)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopRotate ? effect.timeRotate * effect.loopCircleRotate : effect.timeRotate) : new WaitForSeconds(effect.loopRotate ? effect.timeRotate * effect.loopCircleRotate : effect.timeRotate));
                            else if (effect.rotateActivate == ACTIVATETYPE.Continuously)
                                yield return null;
                            break;
                        case TRIGGEREFFECT.Scale:
                            //                            UIUtility.DoScale(effect, effect.timeScale, uIelement.UIObj.GetComponent<RectTransform>(), uIelement, effect.easeScale, effect.loopScale, uIelement.ignoreTimeScale);
                            if (effect.scaleActivate == ACTIVATETYPE.Delay)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeScale) : new WaitForSeconds(effect.delayTimeScale));
                            else if (effect.scaleActivate == ACTIVATETYPE.Sequence)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopScale ? effect.timeScale * effect.loopCircleScale : effect.timeScale) : new WaitForSeconds(effect.loopScale ? effect.timeScale * effect.loopCircleScale : effect.timeScale));
                            else if (effect.scaleActivate == ACTIVATETYPE.Continuously)
                                yield return null;
                            break;
                        case TRIGGEREFFECT.Shake:
                            //                            UIUtility.DoShake(effect, effect.timeShake, uIelement.UIObj.GetComponent<RectTransform>(), uIelement, effect.easeShake, effect.loopShake, uIelement.ignoreTimeScale);
                            if (effect.shakeActivate == ACTIVATETYPE.Delay)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeShake) : new WaitForSeconds(effect.delayTimeShake));
                            else if (effect.shakeActivate == ACTIVATETYPE.Sequence)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopShake ? effect.timeShake * effect.loopCircleShake : effect.timeShake) : new WaitForSeconds(effect.loopShake ? effect.timeShake * effect.loopCircleShake : effect.timeShake));
                            else if (effect.shakeActivate == ACTIVATETYPE.Continuously)
                                yield return null;
                            break;
                        case TRIGGEREFFECT.Punch:
                            //                            UIUtility.DoPunch(effect, effect.timePunch, uIelement.UIObj.GetComponent<RectTransform>(), uIelement, effect.easePunch, effect.loopPunch, uIelement.ignoreTimeScale);
                            if (effect.punchActivate == ACTIVATETYPE.Delay)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimePunch) : new WaitForSeconds(effect.delayTimePunch));
                            else if (effect.punchActivate == ACTIVATETYPE.Sequence)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopPunch ? effect.timePunch * effect.loopCirclePunch : effect.timePunch) : new WaitForSeconds(effect.loopPunch ? effect.timePunch * effect.loopCirclePunch : effect.timePunch));
                            else if (effect.punchActivate == ACTIVATETYPE.Continuously)
                                yield return null;
                            break;

                        case TRIGGEREFFECT.Fade:
                            //                            UIUtility.DoFade(effect, effect.timeFade, uIelement.UIObj.GetComponent<CanvasGroup>(), uIelement, effect.easeFade, effect.loopFade, uIelement.ignoreTimeScale);
                            if (effect.fadeActivate == ACTIVATETYPE.Delay)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.delayTimeFade) : new WaitForSeconds(effect.delayTimeFade));
                            else if (effect.fadeActivate == ACTIVATETYPE.Sequence)
                                yield return (uIelement.ignoreTimeScale ? new WaitForSecondsRealtime(effect.loopFade ? effect.timeFade * effect.loopCircleFade : effect.timeFade) : new WaitForSeconds(effect.loopFade ? effect.timeFade * effect.loopCircleFade : effect.timeFade));
                            else if (effect.fadeActivate == ACTIVATETYPE.Continuously)
                                yield return null;
                            break;
                    }
                }
                yield return null;
            }
            this.DisablePopup(popup);
        }
    }
}
