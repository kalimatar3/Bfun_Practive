using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public abstract class Basepanel : MyBehaviour
{
    // UpdateUI of panel will be called in Enable
    public abstract void UpdateUI();
    protected virtual void OnEnable()
    {
        this.UpdateUI();
    }
#if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
    [GUIColor(0,1,1)]
    public void LOADCOMPONENTS()
    {
        this.LoadComponents();
    }
#endif
}