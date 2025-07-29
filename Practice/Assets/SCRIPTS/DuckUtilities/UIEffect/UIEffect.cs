using DG.Tweening;
using DuckGame.Ultilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour
{
    [SerializeField] protected float delay = 0f;
    protected Image image;
    protected RectTransform rectTransform;
    [SerializeField] protected float time= 0.5f;
    [SerializeField] protected Ease ease;
    [SerializeField] protected bool useSound, ignoreDelay;
    [SerializeField] SoundName soundName;
    [SerializeField] protected UnityEvent onStart, onStartShow, onDone;
    [SerializeField] bool initOnEnable;

    protected virtual void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if (initOnEnable)
            DoEffect();
    }

    public virtual void Reset()
    {
        if(!image) image = GetComponent<Image>();
        ignoreDelay = false;
    }

    public virtual void DoEffect()
    {
        if (!image) image = GetComponent<Image>();
        StartCoroutine(IDoEffect());
    }
    public virtual void IgnoreDelay(bool ignore)
    {
        ignoreDelay = ignore;
    }

    protected virtual IEnumerator IDoEffect()
    {
        if (!enabled) yield break;
        onStart?.Invoke();
        yield return new WaitUntil(() => isActiveAndEnabled);
        if(!ignoreDelay) yield return new WaitForSeconds(delay);
        onStartShow?.Invoke();
    }

    protected void PlaySound()
    {
        if (!useSound) return;
        SoundManager.Instance.PlaySound(soundName, AudioSetting.Default);
    }
}
