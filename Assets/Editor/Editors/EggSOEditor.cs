using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EggScriptableObject))]
public class EggSOEditor : Editor
{
    EggScriptableObject egg;

    private void OnEnable()
    {
        egg = (EggScriptableObject)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("RetrievalText"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Phases"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Tags"));
        serializedObject.ApplyModifiedProperties();
    }
}
