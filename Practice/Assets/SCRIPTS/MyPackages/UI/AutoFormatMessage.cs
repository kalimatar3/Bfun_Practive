using TMPro;
using UnityEngine;

public class AutoFormatMessage : Message 
{
public override void formatmessage()
    {
        base.formatmessage();
        TextMeshProUGUI textComponent = GetComponentInChildren<TextMeshProUGUI>();
        
        if (textComponent != null)
        {
            textComponent.ForceMeshUpdate(); 
            float preferredWidth = textComponent.preferredWidth;
            RectTransform rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(preferredWidth, rt.sizeDelta.y);
        }
    }}