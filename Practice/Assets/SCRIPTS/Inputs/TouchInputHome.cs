using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Clouds.Ultilities;
using UnityEngine.EventSystems;
using Cinemachine;
using System;

public class TouchInputHome : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    [HideInInspector] public Vector2 PrePos;
    [HideInInspector]  public Vector2 CurPos;
    public float TouchSensitivity_x = 10f;
    public float TouchSensitivity_y = 10f;
    [SerializeField] private bool ispress;
    public bool IsPress {
        set { 
            if(!ispress && value) {
               // HomeSceneData.Instance.dolyCartController.LoadCurPos();
            }
            else if(ispress && !value) {

            }
            ispress = value;
        }
        get {return ispress; }
    }
    protected Vector2 deltaPos;
    public float SwipeDuration = 0.5f;
    protected Coroutine DurationSwipeCr;
    // Use this for initialization
    public void IsTouchOverUIButton()
    {
        if(Input.touchCount <=0) {
            IsPress = false;
            return;
        }
        Touch touch = Input.GetTouch(0);
        PointerEventData pointerEventData = new PointerEventData(GUIManager.Instance.EventSystem.GetComponent<EventSystem>());
        pointerEventData.position = touch.position;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                this.IsPress = false;
                return ;
            }
        }
        float z = 15f; // hoặc khoảng cách từ camera đến đối tượng bạn đang thao tác
        if (!IsPress)
        {
            PrePos = touch.position;
            IsPress = true;
        }
        CurPos = touch.position;
        deltaPos = CurPos - PrePos;
    }
    public void LateUpdate()
    {
        // if(!ScenesManager.Instance.IsHomeScene) return;
        // this.IsTouchOverUIButton();
        // if(IsPress) {
        //     HomeSceneData.Instance.SetCamCartPos(deltaPos.x);
        // }
        // else {
        //     HomeSceneData.Instance.AutoMoveCam();
        // }
    }
}
