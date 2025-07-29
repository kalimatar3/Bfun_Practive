using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public abstract class BaseButton : baseUI
{
    [SerializeField] protected Button thisbutton;
    [SerializeField] protected bool Permission = true;
    public void setPermission(bool trigger)
    {
        this.Permission = trigger;
    }
    protected virtual void OnEnable()
    {
        this.Permission = true;
    }
    protected IEnumerator CrSetPermissionbytime(float time)
    {
        this.Permission = false;
        yield return new WaitForSeconds(time);
        this.Permission = true;
    }
    protected override void LoadUIComponents()
    {
        this.Loadthisbutton();
    }
    protected void Loadthisbutton()
    {
        thisbutton = GetComponent<Button>();
        if (thisbutton == null) Debug.LogWarning(this.transform + "dont have button");
    }
    protected virtual bool CanAct()
    {
        return Permission;
    }
    public override void UpdateUI()
    {
    }
#if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
    [GUIColor(0,1,1)]
    public void LOADUIsCOMPONENTS()
    {
        this.LoadUIComponents();
    }
#endif

}
