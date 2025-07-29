using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(SplineGenerator), true)] 
public class SplineGeneratorEditor : Editor 
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("GenerateSpline"))
        {
            GameObject gameObject = (target as MonoBehaviour)?.gameObject;            
            gameObject.GetComponent<SplineGenerator>().GenerarteSpline();
        }
        
    }
}
#endif