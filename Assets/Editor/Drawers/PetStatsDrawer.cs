using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PetStats))]
public class PetStatsDrawer : PropertyDrawer
{
    bool foldout;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lines = 7;
        return foldout ? 20 * lines : 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = position.y;
        var HeaderRect = new Rect(position.x, height, position.width, 18);
        height += 20;
        var MovementSectionHeader = new Rect(position.x + 20, height, position.width - 20, 18);
        height += 20;
        var SpeedRect = new Rect(position.x + 20, height, position.width - 20, 18);
        height += 20;
        var MoveIntervalRect = new Rect(position.x + 20, height, position.width + 92, 18);
        height += 20;
        var SpeakingSectionHeader = new Rect(position.x + 20, height, position.width - 20, 18);
        height += 20;
        var SpeakIntervalRect = new Rect(position.x + 20, height, position.width + 92, 18);
        height += 20;
        var SpeakSpeedRect = new Rect(position.x + 20, height, position.width - 20, 18);

        EditorGUIUtility.wideMode = true; //why is this necessary to keep Vector2 fields in a single line. They're scuffed either way..
        foldout = EditorGUI.BeginFoldoutHeaderGroup(HeaderRect, foldout, label);
        if(foldout)
        {
            EditorGUIUtility.labelWidth = 100;
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(MovementSectionHeader, new GUIContent("<color=white>Movement</color>"), new GUIStyle() { fontStyle = FontStyle.Bold });
            EditorGUI.PropertyField(SpeedRect, property.FindPropertyRelative(nameof(PetStats.Speed)));
            EditorGUI.PropertyField(MoveIntervalRect, property.FindPropertyRelative(nameof(PetStats.MoveInterval)));
            EditorGUI.LabelField(SpeakingSectionHeader, new GUIContent("<color=white>Speaking</color>"), new GUIStyle() { fontStyle = FontStyle.Bold });
            EditorGUI.PropertyField(SpeakIntervalRect, property.FindPropertyRelative(nameof(PetStats.SpeakInterval)));
            EditorGUI.PropertyField(SpeakSpeedRect, property.FindPropertyRelative(nameof(PetStats.SpeakSpeed)));
            EditorGUI.EndProperty();
        }
        EditorGUI.EndFoldoutHeaderGroup();
    }
}
