using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
public class SerializableDictionaryDrawer : PropertyDrawer
{
    private float lineHeight = EditorGUIUtility.singleLineHeight;
    private float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty keysProp = property.FindPropertyRelative("keys");
        return (keysProp.arraySize + 2) * (lineHeight + verticalSpacing);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty keysProp = property.FindPropertyRelative("keys");
        SerializedProperty valuesProp = property.FindPropertyRelative("values");

        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        int count = Mathf.Min(keysProp.arraySize, valuesProp.arraySize);

        Rect rect = new(position.x, position.y, position.width, lineHeight);

        for (int i = 0; i < count; i++)
        {
            SerializedProperty keyProp = keysProp.GetArrayElementAtIndex(i);
            SerializedProperty valueProp = valuesProp.GetArrayElementAtIndex(i);

            float halfWidth = rect.width / 2f;

            Rect keyRect = new(rect.x, rect.y, halfWidth - 5, lineHeight);
            Rect valueRect = new(rect.x + halfWidth + 5, rect.y, halfWidth - 5, lineHeight);

            EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none, true);
            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none, true);

            rect.y += lineHeight + verticalSpacing;
        }

        // Add button
        Rect buttonRect = new(rect.x, rect.y, rect.width, lineHeight);
        if (GUI.Button(buttonRect, "Add Entry"))
        {
            keysProp.arraySize++;
            valuesProp.arraySize++;
        }

        EditorGUI.EndProperty();
    }
}
#endif
