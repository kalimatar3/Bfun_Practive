using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(UIEffect), true)] // Tạo editor cho các class kế thừa Spawner<T>
public class UIEffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Play"))
        {
            GameObject gameObject = (target as MonoBehaviour)?.gameObject;            
            gameObject.GetComponent<UIEffect>().DoEffect();
        }
    }

}
