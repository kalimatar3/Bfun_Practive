using UnityEngine;

public abstract class baseClickyButton : BaseButton
{
    protected override void LoadUIComponents()
    {
        base.LoadUIComponents();
        this.AddActButton();
    }
    protected void AddActButton()
    {
        if(thisbutton == null) return;
        thisbutton.onClick.AddListener(delegate () { this.ClickEvent(); });
    }
    protected virtual void ClickEvent()
    {
        if (!CanAct()) return;
        this.UpdateUI();
        this.OnClick();
    }
    public abstract void OnClick();
}