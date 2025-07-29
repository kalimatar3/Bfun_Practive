using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class VehicleController_Button : basePressedButton
{
    [SerializeField] protected Sprite Active, InActive;
    [SerializeField] protected Image Image;

    public override void OnHolding()
    {
        Image.sprite = Active;
    }

    public override void OnPressed()
    {
        Image.sprite = Active;
    }

    public override void OnRelease()
    {
        Image.sprite = InActive;
    }
    public virtual void KeyBoradControl() { }
#if UNITY_EDITOR
    protected virtual void FixedUpdate() {
        this.KeyBoradControl();
    }
#endif
}