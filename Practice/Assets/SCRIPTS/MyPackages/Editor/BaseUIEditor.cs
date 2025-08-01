#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(baseUI), true)]
public class BaseUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var ui = (baseUI)target;
        EditorGUILayout.LabelField("Unique ID", ui.ID.ToString());
    }
}
#endif
