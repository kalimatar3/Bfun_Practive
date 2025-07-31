using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buy_button : BaseClickyButton
{
    public override void OnClick()
    {
        if(Condition()) 
        {
            this.Pay();
            this.GetMechandise();
        }
        else NoticeCantBuy();
    }
    public abstract void GetMechandise();
    protected abstract void Pay();
    public abstract bool Condition();
    public abstract void NoticeCantBuy();
}
