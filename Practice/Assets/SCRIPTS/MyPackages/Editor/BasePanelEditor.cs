using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;
using DG.DOTweenEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Clouds.Ultilities;

#if UNITY_EDITOR
[CustomEditor(typeof(Basepanel), true)]
public class BasePanelEditor : OdinEditor
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;

    private static Dictionary<Object, Tween> activePreviews = new();
    private bool _hasStoredInitialState = false;

    public override void OnInspectorGUI()
    {
        var ui = (Basepanel)target;


        // Kiểm tra trùng SignalType trong Plots
        var plots = typeof(Basepanel).GetField("Plots", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(ui) as List<UIelement>;
        HashSet<SignalType> seenSignals = new HashSet<SignalType>();
        List<SignalType> duplicates = new List<SignalType>();

        if (plots != null)
        {
            foreach (var el in plots)
            {
                if (!seenSignals.Add(el.signalType))
                {
                    if (!duplicates.Contains(el.signalType))
                        duplicates.Add(el.signalType);
                }
            }

            if (duplicates.Count > 0)
            {
                SirenixEditorGUI.ErrorMessageBox("Dupplicated Message : " + string.Join(", ", duplicates));
            }
        }

        // Vẽ phần mặc định của Odin
        base.OnInspectorGUI();

        GUILayout.Space(10);

        // ▶ Preview
        if (GUILayout.Button("▶ Preview", GUILayout.Height(30)))
        {
            if (activePreviews.TryGetValue(ui, out var oldTween) && oldTween.IsActive())
            {
                oldTween.Kill();
                Debug.Log("Killed previous tween.");
            }

            if (!_hasStoredInitialState)
            {
                StoreInitialState(ui.transform);
                _hasStoredInitialState = true;
            }

            DOTweenEditorPreview.Start();

            Tween newTween = ui.TestMove();
            if (newTween != null)
            {
                DOTweenEditorPreview.PrepareTweenForPreview(newTween);
                activePreviews[ui] = newTween;
                Debug.Log("Started preview tween.");
            }
            else
            {
                Debug.LogWarning("Tween returned by TestMove is null!");
            }
        }

        // ■ Stop
        if (GUILayout.Button("■ Stop Tween Preview", GUILayout.Height(30)))
        {
            DOTweenEditorPreview.Stop();

            if (activePreviews.TryGetValue(ui, out var previewTween) && previewTween.IsActive())
            {
                previewTween.Kill();
                activePreviews.Remove(ui);
            }

            if (_hasStoredInitialState)
            {
                RestoreInitialState(ui.transform);
                _hasStoredInitialState = false;
                Debug.Log("Stopped preview and restored initial state.");
            }
            else
            {
                Debug.Log("Stopped preview.");
            }
        }
    }

    private void StoreInitialState(Transform tr)
    {
        _initialPosition = tr.localPosition;
        _initialRotation = tr.localRotation;
        _initialScale = tr.localScale;
    }

    private void RestoreInitialState(Transform tr)
    {
        tr.localPosition = _initialPosition;
        tr.localRotation = _initialRotation;
        tr.localScale = _initialScale;
    }
}
#endif
