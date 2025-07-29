using System.Collections;
using System.Collections.Generic;
using DuckGame.Ultilities;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CanvasBase : MonoBehaviour
{
    [HideInInspector] public Canvas canvas;
    [SerializeField] GameObject panel;
    [SerializeField] RectTransform mainScreen, banner;
    CanvasGroup canvasGroup;
    CanvasScaler canvasScaler;
    public bool IsCanvas
    {
        get { return canvasGroup.alpha == 1; }
    }

    public virtual void Awake() {
        canvas = GetComponent<Canvas>();    
        canvasScaler = GetComponent<CanvasScaler>();
        canvasGroup = GetComponent<CanvasGroup>();
        panel.SetActive(false);
        SetMatchScaleByScreen();
    }

    private void OnConnectedToServer()
    {
        DuckHelper.SetRectBottom(mainScreen, 120);
        DuckHelper.SetRectBottom(banner, Screen.height - 120);
    }

    public void ShowCanvas(bool show, bool fade = true)
    {
        canvasGroup.DOFade(show ? 1 : 0, fade ? 0.5f : 0);
    }

    void SetMatchScaleByScreen()
    {
        if (GUIManager.Instance.cameraUI.aspect >= 1.8)// 16:9
            canvasScaler.matchWidthOrHeight = 1;
        else if (GUIManager.Instance.cameraUI.aspect >= 1.7)// 16:9
            canvasScaler.matchWidthOrHeight = 0.5625f;
        else if (GUIManager.Instance.cameraUI.aspect > 1.6)// 5:3
            canvasScaler.matchWidthOrHeight = 0.1f;
        else if (GUIManager.Instance.cameraUI.aspect == 1.6)// 16:10
            canvasScaler.matchWidthOrHeight = 0.5f;
        else if (GUIManager.Instance.cameraUI.aspect >= 1.5)// 3:2
            canvasScaler.matchWidthOrHeight = 0.5625f;
        else// 4:3
            canvasScaler.matchWidthOrHeight = 0;
        DuckHelper.LogGame("Set Screen Scale : " + canvasScaler.matchWidthOrHeight);
    }
}
