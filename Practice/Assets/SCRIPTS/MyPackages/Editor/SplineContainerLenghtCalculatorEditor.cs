using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Unity.VisualScripting;

[CustomEditor(typeof(SplineContainerLengthCaculator))]
public class SplineContainerLenghtCalculatorEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("CalLength")) {
            target.GetComponent<SplineContainerLengthCaculator>().CalculateLength();
        }
    }
}