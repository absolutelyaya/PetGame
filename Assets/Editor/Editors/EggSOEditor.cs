using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EggSO))]
public class EggSOEditor : Editor
{
    //EggSO egg;

    //private void OnEnable()
    //{
    //    egg = (EggSO)target;
    //}

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
