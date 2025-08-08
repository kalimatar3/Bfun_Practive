using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;
using DG.DOTweenEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Clouds.Ultilities;
using System;

#if UNITY_EDITOR
[CustomEditor(typeof(Basepanel), true)]
public class BasePanelEditor : OdinEditor
{
    private static Dictionary<Transform, InitTransStatus> TransStatusStorage = new();
    private static Dictionary<UnityEngine.Object, Tween> activePreviews = new();
    private SignalName PreviewSignal;
    private bool isPlaying = false;
    private bool isPaused = false;
    private DateTime lastFixedUpdate;
    private double fixedDeltaTime = 0.02;

    private EditorApplication.CallbackFunction updateCallback;

    private Basepanel TarGet => (Basepanel)target;
    public override void OnInspectorGUI()
    {
        PreviewSignal = (SignalName)EditorGUILayout.EnumPopup("Preview Signal", PreviewSignal);
        if (isPlaying)
            GUIHelper.PushColor(Color.green); // Highlight background khi đang Play

        SirenixEditorGUI.Title("Preview Controller", null, TextAlignment.Left, true);

        EditorGUILayout.BeginHorizontal();
        GUI.enabled = !isPlaying;
        if (GUILayout.Button("▶ Play", GUILayout.Height(30)))
        {
            StartSimulation();
        }
        GUI.enabled = isPlaying && !isPaused;
        if (GUILayout.Button("⏸ Pause", GUILayout.Height(30)))
        {
            PauseSimulation();
        }
        GUI.enabled = isPlaying;
        if (GUILayout.Button("■ Stop", GUILayout.Height(30)))
        {
            StopSimulation();
        }
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();

        if (isPlaying)
            GUIHelper.PopColor();

        GUILayout.Space(10);
        base.OnInspectorGUI();
    }

    private void StartSimulation()
    {
        isPlaying = true;
        isPaused = false;
        lastFixedUpdate = DateTime.Now;

        this.Preview();
        // Bắt đầu gọi Update các frame
        updateCallback = SimulateRuntime;
        EditorApplication.update += updateCallback;
    }

    private void PauseSimulation()
    {
        isPaused = true;
    }

    private void StopSimulation()
    {
        isPlaying = false;
        isPaused = false;
        this.StopPreview();
        // Ngừng gọi update
        if (updateCallback != null)
            EditorApplication.update -= updateCallback;
    }

    private void SimulateRuntime()
    {
        if (!isPlaying || isPaused) return;

        // --- Mô phỏng Update ---
        // Chèn các hàm Update ở đây
        EditorUpdate();

        // --- Mô phỏng FixedUpdate ---
        if ((DateTime.Now - lastFixedUpdate).TotalSeconds >= fixedDeltaTime)
        {
            lastFixedUpdate = DateTime.Now;
            // Chèn các hàm FixedUpdate ở đây
            EditorFixedUpdate();
        }

        // --- Mô phỏng LateUpdate ---
        // Chèn các hàm LateUpdate ở đây
        EditorLateUpdate();
    }

    private void EditorLateUpdate()
    {
        UIGroup uIGroup = TarGet.UIGroupDics[PreviewSignal];
        
        foreach (var uIelement in uIGroup.UIS)
        {
            if (uIelement.BEHAVIOUR == UIBEHAVIOUR.TRIGGER) continue;
            foreach (var ele in uIelement.ConEffects)
            {
                if (ele.type == CONTINUOSEFFECT.FillText)
                {
                    ele.TextComponent.text = ele.Text;
                }
                else if (ele.type == CONTINUOSEFFECT.ChangeOpacity)
                {
                    Color color = ele.Image.color;
                    color.a = ele.Opacity;
                    ele.Image.color = color;
                }
            } 
        }
    }

    private void EditorFixedUpdate()
    {
    }

    private void EditorUpdate()
    {
    }

    // (Tuỳ chọn) Khi đóng cửa sổ hoặc reload script
    protected override void OnDisable()
    {
        base.OnDisable();
        if (updateCallback != null)
            EditorApplication.update -= updateCallback;
    }
    private void Preview()
    {
        TarGet.UIGroupDics[PreviewSignal].tweens = new List<Tween>();
        TarGet.AddTweens(PreviewSignal);
        foreach (var ele in TarGet.UIGroupDics[PreviewSignal].UIS)
        {
            if (ele.BEHAVIOUR == UIBEHAVIOUR.UPDATE) continue;
            StoreInitialState(ele.UIObj.transform);
            DOTweenEditorPreview.Start();
            if (TarGet.UIGroupDics[PreviewSignal].tweens != null)
            {
                foreach (var ele1 in TarGet.UIGroupDics[PreviewSignal].tweens)
                {
                    DOTweenEditorPreview.PrepareTweenForPreview(ele1);
                    activePreviews[TarGet] = ele1;
                }
            }
            else
            {
                Debug.LogWarning("Tween returned by TestMove is null!");
            }
        }
    }
    private void StopPreview()
    {
        TarGet.UIGroupDics[PreviewSignal].tweens = new List<Tween>();
        foreach (var ele in TarGet.UIGroupDics[PreviewSignal].UIS)
        {
            if (ele.BEHAVIOUR == UIBEHAVIOUR.UPDATE) continue;
            if (ele.UIObj == null) continue;
            DOTweenEditorPreview.Stop();
            if (activePreviews.TryGetValue(ele.UIObj, out var previewTween) && previewTween.IsActive())
            {
                previewTween.Kill();
                activePreviews.Remove(ele.UIObj.transform);
            }
            RestoreInitialState(ele.UIObj.transform);
           // _hasStoredInitialState = false;
            Debug.Log("Stopped preview and restored initial state.");
        }
    }
    private void StoreInitialState(Transform tr)
    {
        InitTransStatus initstatus = new InitTransStatus();
        initstatus._initialPosition = tr.localPosition;
        initstatus._initialRotation = tr.localRotation;
        initstatus._initialScale = tr.localScale;
        TransStatusStorage.Add(tr, initstatus);
    }

    private void RestoreInitialState(Transform tr)
    {
        InitTransStatus initstatus = TransStatusStorage[tr];
        tr.localPosition = initstatus._initialPosition;
        tr.localRotation = initstatus._initialRotation;
        tr.localScale = initstatus._initialScale;
        TransStatusStorage.Remove(tr);
    }
}
public struct InitTransStatus
{
    public Vector3 _initialPosition;
    public Quaternion _initialRotation;
    public Vector3 _initialScale;   
}
#endif
