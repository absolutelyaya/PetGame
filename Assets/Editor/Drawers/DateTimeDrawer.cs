using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UDateTime))]
public class DateTimeDrawer : PropertyDrawer
{
    bool foldout;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return foldout ? 58 : 18;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent());
        EditorGUI.indentLevel = 0;
        float height = position.y;
        var FoldoutRect = new Rect(position.x, height, position.width, 18);
        height += 20;
        EditorGUI.indentLevel++;
        var DayRect = new Rect(position.x + 8, height, position.width / 3 - 8, 18);
        var MonthRect = new Rect(position.x + position.width / 3 + 8, height, position.width / 3 - 8, 18);
        var YearRect = new Rect(position.x + (position.width / 3) * 2 + 8, height, position.width / 3 - 8, 18);
        height += 20;
        var HourRect = new Rect(position.x + 8, height, position.width / 3 - 8, 18);
        var MinuteRect = new Rect(position.x + position.width / 3 + 8, height, position.width / 3 - 8, 18);
        var SecondRect = new Rect(position.x + (position.width / 3) * 2 + 8, height, position.width / 3 - 8, 18);
        EditorGUI.indentLevel--;

        foldout = EditorGUI.BeginFoldoutHeaderGroup(FoldoutRect, foldout, label);
        if(foldout)
        {
            EditorGUIUtility.labelWidth = 16;
            EditorGUI.PropertyField(DayRect, property.FindPropertyRelative("Day"), new GUIContent("D"));
            EditorGUI.PropertyField(MonthRect, property.FindPropertyRelative("Month"), new GUIContent("M"));
            EditorGUI.PropertyField(YearRect, property.FindPropertyRelative("Year"), new GUIContent("Y"));
            EditorGUI.PropertyField(HourRect, property.FindPropertyRelative("Hour"), new GUIContent("h"));
            EditorGUI.PropertyField(MinuteRect, property.FindPropertyRelative("Minute"), new GUIContent("m"));
            EditorGUI.PropertyField(SecondRect, property.FindPropertyRelative("Second"), new GUIContent("s"));
        }
        EditorGUI.EndFoldoutHeaderGroup();
    }
}
