using UnityEngine;
using UnityEngine.UI;
public abstract class BaseSlider : baseUI
{
    [SerializeField] protected Slider slider;
    public float value;
    protected void LoadSlider()
    {
        Slider slider =  GetComponent<Slider>();
        if(slider == null)
        {
            Debug.LogWarning(this.transform.name + " Cant Found Slider");
            return;
        }
        this.slider = slider;
    }
    protected override void LoadUIComponents()
    {
        this.LoadSlider();
    }
    public override void UpdateUI()
    {
        this.slider.value = value;
    }
    public virtual void SetvalueSlider(float number)
    {
        this.value = number;
        this.UpdateUI();
    }
    public virtual float getvalueSlider()
    {
        return this.slider.value;
    }
}
