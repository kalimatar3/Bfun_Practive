using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine.Events;
using System;

namespace DuckGame.Ultilities
{
    public enum TARGETTRANSFROM{This, Another};

    [RequireComponent(typeof(Button), typeof(Image))]
    public class ButtonEvents : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        // [SerializeField] bool allowEffect = true;
        // [SerializeField] bool isSound = true;
        // [SerializeField] bool allowWhenSlowMotion = true;
        // [SerializeField] [EnumToggleButtons] TARGETTRANSFROM useTranform;
        // [ShowIf("useTranform", TARGETTRANSFROM.Another)] [SerializeField] Transform targetTranform;
        // Transform choseTransform;
        // ButtonData buttonData;
        // [SerializeField] ButtonName buttonName;
        // [SerializeField] UnityEvent onClick;
        // Button button;

        // void Awake()
        // {
        //     button = GetComponent<Button>();
        //     choseTransform = (useTranform == TARGETTRANSFROM.This ? transform : targetTranform);
        // }

        // private void OnEnable()
        // {
        //     buttonData = ButtonManager.Instance.ButtonList[buttonName];
        //     button.onClick.AddListener(OnClick);
        // }

        // public void AddEvent(UnityAction e)
        // {
        //     onClick.AddListener(e);
        // }

        // public void RemoveEvent()
        // {
        //     onClick.RemoveAllListeners();
        // }

        // public void SetOnClick(UnityEvent e)
        // {
        //     onClick = e;
        // }

        // public void OnClick()
        // {
        //     if(!GUIManager.Instance.CanInteractiveWithButton || (!allowWhenSlowMotion && GameManager.Instance.isSloMotion)) return;
        //     if (allowEffect)
        //     {
        //         GUIManager.Instance.DelayPress();
        //         onClick?.Invoke();
        //         if (isSound) SoundManager.Instance.PlayUIClickSound();
        //         float initScale = choseTransform.transform.localScale.x;
        //         choseTransform.DOScale(initScale + 0.08f, 0.1f).OnComplete(() =>
        //         {
        //             choseTransform.DOScale(initScale, 0.1f).OnComplete(() =>
        //             {
        //                 buttonData.OnPointerClick.Invoke();
        //             }).SetId(1).SetUpdate(Time.timeScale == 0 ? true : false);
        //         }).SetId(1).SetUpdate(Time.timeScale == 0 ? true : false);
        //     }
        //     else
        //     {
        //         onClick?.Invoke();
        //         buttonData.OnPointerClick.Invoke();
        //     }
        // }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     buttonData.OnPointerPress.Invoke();
        // }

        // public void OnPointerUp(PointerEventData eventData)
        // {
        //     buttonData.OnPointerRelease.Invoke();
        // }

        // private void OnDisable()
        // {
        //     button.onClick.RemoveListener(OnClick);
        // }
        public void OnPointerDown(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
