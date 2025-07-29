using UnityEngine;

public abstract class baseUI  : MyBehaviour
{
    protected abstract void LoadUIComponents(); // LoadComponents on Load Data; 
    public abstract void UpdateUI(); //On Update by Data and Enable;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIComponents();
    }
}