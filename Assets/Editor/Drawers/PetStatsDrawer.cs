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
        return foldout ? 54 : 18;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = position.y;
        var HeaderRect = new Rect(position.x, height, position.width, 18);
        height += 18;
        var SpeedRect = new Rect(position.x, height, position.width, 18);
        height += 18;
        var MoveIntervalRect = new Rect(position.x, height, position.width, 18);

        EditorGUIUtility.wideMode = true; //why is this necessary to keep Vector2 fields in a single line.
        foldout = EditorGUI.BeginFoldoutHeaderGroup(HeaderRect, foldout, label);
        if(foldout)
        {
            EditorGUIUtility.labelWidth = 100;
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(SpeedRect, property.FindPropertyRelative(nameof(PetStats.Speed)));
            EditorGUI.PropertyField(MoveIntervalRect, property.FindPropertyRelative(nameof(PetStats.MoveInterval))); //Scuffed either way
            EditorGUI.EndProperty();
        }
        EditorGUI.EndFoldoutHeaderGroup();
    }
}
