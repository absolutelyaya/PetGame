using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Egg))]
public class EggEditor : Editor
{
    Egg egg;
    Editor eggDataEditor;

    private void OnEnable()
    {
        egg = (Egg)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        return base.CreateInspectorGUI();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EggData"));
        if(egg.phaseData != null)
            EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), (egg.phaseTime / 60) / egg.phaseData.Duration, "PhaseTime");
        if(egg.EggData)
        {
            CreateCachedEditor(egg.EggData, null, ref eggDataEditor);
            eggDataEditor.OnInspectorGUI();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
