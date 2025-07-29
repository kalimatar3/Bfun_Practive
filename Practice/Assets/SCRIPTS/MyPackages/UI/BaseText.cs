using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseTextUI : baseUI
{
    [SerializeField] protected TextMeshProUGUI text;
     public TextMeshProUGUI Text {get {return text;}}
    protected override void LoadUIComponents()
    {
        this.LoadText();
    }
    protected void LoadText() {
        this.text = GetComponent<TextMeshProUGUI>();
        if(text == null) Debug.LogWarning("Cant found Text");
    }
    public override void UpdateUI()
    {
        this.ShowText();
    }
    public abstract void ShowText();
}
