using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EggPhase))]
public class EggPhaseDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 127;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float lineHeight = position.y;
        var BGRect = new Rect(position.x - 5, lineHeight, position.width + 5, position.height - 3); lineHeight += 8;
        var LabelRect = new Rect(position.x - 5, position.y, position.width + 5, 18); lineHeight += 18;
        var SpriteRect = new Rect(position.x + 80, lineHeight, position.width - 85, 18);
        var SpritePreviewRect = new Rect(position.x, lineHeight - 4, 75, 100); lineHeight += 18;
        var DurationRect = new Rect(position.x + 80, lineHeight, position.width - 85, 18); lineHeight += 18;
        var WiggleLabelRect = new Rect(position.x - 5, lineHeight, position.width + 5, 18); lineHeight += 18;
        var WiggleAmountRect = new Rect(position.x + 80, lineHeight, position.width - 85, 18); lineHeight += 18;
        var WiggleSpeedRect = new Rect(position.x + 80, lineHeight, position.width - 85, 18);

        EditorGUIUtility.labelWidth = 75;

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.DrawRect(BGRect, new Color(30 / 255f, 30 / 255f, 30 / 255f));
        EditorGUI.DrawRect(LabelRect, GetHeaderColor(label.text));
        EditorGUI.LabelField(LabelRect, new GUIContent($"<color=white>{label.text.Replace("Element", "Phase")}</color>"), 
            new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold });

        EditorGUI.PropertyField(SpriteRect, property.FindPropertyRelative(nameof(EggPhase.Sprite)), new GUIContent("Sprite"));
        Sprite sprite = (Sprite)property.FindPropertyRelative(nameof(EggPhase.Sprite)).objectReferenceValue;
        EditorGUI.DrawRect(SpritePreviewRect, Color.black);
        if (sprite != null)
            GUI.DrawTextureWithTexCoords(SpritePreviewRect, sprite.texture, GetTextureCoords(sprite));
        else
            GUI.Label(SpritePreviewRect, new GUIContent("<color=white>?</color>"), new GUIStyle() { alignment = TextAnchor.MiddleCenter , fontSize = 64 });
        EditorGUI.PropertyField(DurationRect, property.FindPropertyRelative(nameof(EggPhase.Duration)), new GUIContent("Duration", "Minutes"));

        EditorGUI.LabelField(WiggleLabelRect, new GUIContent($"<color=white>Wiggling</color>"),
            new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold });
        EditorGUI.PropertyField(WiggleAmountRect, property.FindPropertyRelative(nameof(EggPhase.WiggleAmount)), new GUIContent("Amount", "Degrees"));
        EditorGUI.PropertyField(WiggleSpeedRect, property.FindPropertyRelative(nameof(EggPhase.WiggleSpeed)), new GUIContent("Speed"));

        EditorGUI.EndProperty();
    }
    
    private Color GetHeaderColor(string label)
    {
        var segments = label.Split(' ');
        if (segments.Length == 2)
        {
            int i = int.Parse(segments[1]);
            //return Color.HSVToRGB((18 * i / 255f) % 1f, 0.75f, 0.45f); //Rainbow
            return new Color((20 + 10 * (i + 1)) / 255f, 20 / 255f, 20 / 255f); //Red
        }
        else
            return new Color(40 / 255f, 20 / 255f, 20 / 255f);
    }

    private Rect GetTextureCoords(Sprite s)
    {
        Rect rect = s.rect;
        Texture tex = s.texture;
        rect.xMin /= tex.width;
        rect.xMax /= tex.width;
        rect.yMin /= tex.height;
        rect.yMax /= tex.height;
        return rect;
    }
}
