using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GrowthRequirement))]
public class GrowthRequirementDrawer : PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int height = 18;

        switch (property.FindPropertyRelative(nameof(GrowthRequirement.Type)).enumValueIndex)
        {
            case (int)GrowthRequirementType.Age:
                height += 18;
                break;
            case (int)GrowthRequirementType.Food:
                height += 18;
                break;
            default:
                break;
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float y = position.y;
        var typeRect = new Rect(position.x, y, position.width, 18);
        y += 18;
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative(nameof(GrowthRequirement.Type)), new GUIContent("Type"));

        switch (property.FindPropertyRelative(nameof(GrowthRequirement.Type)).enumValueIndex)
        {
            case (int)GrowthRequirementType.Age:
                var AgeRect = new Rect(position.x, y, position.width, 18);
                EditorGUI.PropertyField(AgeRect, property.FindPropertyRelative(nameof(GrowthRequirement.MinAge)), new GUIContent("Age (min)"));
                y += 18;
                break;
            case (int)GrowthRequirementType.Food:
                var FoodRect = new Rect(position.x, y, position.width, 18);
                EditorGUI.LabelField(FoodRect, new GUIContent("<color=red>Food Requirement not implemented</color>"), 
                    new GUIStyle() { alignment = TextAnchor.MiddleCenter });
                y += 18;
                break;
            default:
                break;
        }
    }
}
