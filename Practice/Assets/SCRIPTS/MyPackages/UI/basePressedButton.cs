using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class basePressedButton : BaseButton,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected bool isHolding;
    private Coroutine holdingCoroutine;
    public SignalContainer Signal;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CanAct()) return;

        isHolding = true;
        OnPressed();

        if (holdingCoroutine == null)
            holdingCoroutine = StartCoroutine(HoldingRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!CanAct()) return;

        isHolding = false;
        OnRelease();

        if (holdingCoroutine != null)
        {
            StopCoroutine(holdingCoroutine);
            holdingCoroutine = null;
        }
    }

    private IEnumerator HoldingRoutine()
    {
        while (isHolding)
        {
            OnHolding();
            Signal.Send(null);
            yield return null; // mỗi frame gọi một lần
        }
    }

    public abstract void OnHolding();
    public abstract void OnPressed();
    public abstract void OnRelease();

}