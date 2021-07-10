using System;
using UnityEditor;
using UnityEngine;

[EditorWindowTitle(title = "Adoption Place Editor")]
public class AdoptionPlaceEditor : EditorWindow
{
    AdoptionPlaceSO place;
    EggSO selectedEgg;
    float ScrollPosition;
    float ScrollBarMax;
    GUIStyle invisibutton;
    int eggAnimStep = 0;
    float lastFrameTime = 0;

    [MenuItem("Window/Adoption Place Editor")]
    public static void ShowWindow()
    {
        AdoptionPlaceEditor window = GetWindow<AdoptionPlaceEditor>();

        window.invisibutton = new GUIStyle();
        Texture2D bg = new Texture2D(1, 1);
        bg.SetPixel(0, 0, Color.clear);
        bg.Apply();
        window.invisibutton.normal.background = bg;

    }

    private void OnGUI()
    {
        if (Selection.objects.Length > 0 && Selection.objects[0] is AdoptionPlaceSO selection)
            place = selection;

        if (place != null)
        {
            SerializedObject so = new SerializedObject(place);
            so.Update();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box) { stretchWidth = true });
            EditorGUILayout.LabelField(new GUIContent($"<color=white>Editing <b>{place.name}</b></color>"),
                new GUIStyle() { alignment = TextAnchor.UpperCenter, contentOffset = new Vector2(0, -4), fontSize = 22 });
            EditorGUILayout.Space(5);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            EditorGUILayout.Space(EditorGUIUtility.labelWidth + EditorGUIUtility.fieldWidth, true);
            var r = GUILayoutUtility.GetLastRect();
            if ((Sprite)so.FindProperty(nameof(AdoptionPlaceSO.Preview)).objectReferenceValue != null)
            {
                GUI.DrawTexture(r, ((Sprite)so.FindProperty(nameof(AdoptionPlaceSO.Preview)).objectReferenceValue).texture, ScaleMode.ScaleToFit, true, 
                    0, new Color(1, 1, 1, 0.25f), 0, 0);
            }
            EditorGUI.PropertyField(new Rect(r.x + r.width / 4, r.y + r.height / 2 - EditorGUIUtility.singleLineHeight / 2, r.width - r.width / 4 * 2, 
                EditorGUIUtility.singleLineHeight), so.FindProperty(nameof(AdoptionPlaceSO.Preview)), new GUIContent());
            EditorGUI.LabelField(r, new GUIContent("<color=white>Preview Sprite</color>"),
                new GUIStyle() { alignment = TextAnchor.UpperCenter, fontStyle = FontStyle.Bold, fontSize = 18 });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            EditorGUILayout.Space(EditorGUIUtility.labelWidth + EditorGUIUtility.fieldWidth, true);
            r = GUILayoutUtility.GetLastRect();
            if ((Sprite)so.FindProperty(nameof(AdoptionPlaceSO.ButtonSprite)).objectReferenceValue != null)
            {
                GUI.DrawTexture(r, ((Sprite)so.FindProperty(nameof(AdoptionPlaceSO.ButtonSprite)).objectReferenceValue).texture, ScaleMode.ScaleToFit, true, 
                    0, new Color(1, 1, 1, 0.25f), 0, 0);
            }
            EditorGUI.PropertyField(new Rect(r.x + r.width / 4, r.y + r.height / 2 - EditorGUIUtility.singleLineHeight / 2, r.width - r.width / 4 * 2, 
                EditorGUIUtility.singleLineHeight), so.FindProperty(nameof(AdoptionPlaceSO.ButtonSprite)), new GUIContent());
            EditorGUI.LabelField(r, new GUIContent("<color=white>Button Texture</color>"), 
                new GUIStyle() { alignment = TextAnchor.UpperCenter, fontStyle = FontStyle.Bold, fontSize = 18 });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box) { stretchWidth = true, stretchHeight = true, clipping = TextClipping.Clip });
            EditorGUILayout.LabelField(new GUIContent("<color=white>Egg Pool</color>"),
                new GUIStyle() { alignment = TextAnchor.UpperCenter, fontStyle = FontStyle.Bold, fontSize = 18 }); // 28 high
            EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            Rect eggListRect = EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { fixedHeight = 426, padding = new RectOffset(5, 0, 5, 5) });
            EditorGUI.LabelField(eggListRect, new GUIContent("<color=#FFFFFF96>Drag and Drop Egg Scriptable Objects here!</color>"), 
                new GUIStyle() { alignment = TextAnchor.MiddleCenter });
            SerializedProperty eggPool = so.FindProperty(nameof(AdoptionPlaceSO.EggPool));
            int eggsPerLine = 10;
            int eggListLines = Mathf.CeilToInt(eggPool.arraySize / (float)eggsPerLine);
            if (eggListLines == 0)
                EditorGUILayout.Space();
            if (ScrollBarMax != eggListLines)
                ScrollBarMax = eggListLines;
            for (int line = Mathf.Max(Mathf.RoundToInt(ScrollPosition), 0); line < Mathf.Clamp(Mathf.RoundToInt(ScrollPosition) + 3, 0, (float)eggListLines); line++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int i = line * eggsPerLine; i < Mathf.Clamp(line * eggsPerLine + eggsPerLine, 0, eggPool.arraySize); i++)
                {
                    EggSO egg = (EggSO)eggPool.GetArrayElementAtIndex(i).objectReferenceValue;
                    r = EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box) { stretchWidth = false, stretchHeight = false, fixedHeight = 125, fixedWidth = 100 });
                    EditorGUILayout.Space(100);
                    if(egg != null)
                    {
                        if (selectedEgg == egg)
                        {
                            Color selColor = new Color(58 / 255f, 121 / 255f, 187 / 255f);
                            EditorGUI.DrawRect(new Rect(r.x + 1, r.y, r.width - 1, 1), selColor);
                            EditorGUI.DrawRect(new Rect(r.x, r.y + 1, 1, r.height - 1), selColor);
                            EditorGUI.DrawRect(new Rect(r.x + r.width, r.y + 1, 1, r.height - 1), selColor);
                            EditorGUI.DrawRect(new Rect(r.x + 1, r.y + r.height, r.width - 1, 1), selColor);
                        }
                        var sprite = egg.Phases[eggAnimStep % egg.Phases.Count].Sprite;
                        float aspect = sprite.rect.width / sprite.rect.height;
                        GUI.DrawTextureWithTexCoords(new Rect(r.x + (r.width - r.width / 10 * 9), r.y,
                            r.width / 10 * 9 * aspect + (r.width - r.width / 10 * 9), r.width / 10 * 9 + (r.width - r.width / 10 * 9)),
                            sprite.texture, GetTextureCoords(sprite), true);
                        EditorGUI.LabelField(r, new GUIContent($"<color=white>{egg.name}</color>"), 
                            new GUIStyle() { alignment = TextAnchor.LowerCenter, wordWrap = true });
                        if (GUI.Button(r, new GUIContent(), invisibutton))
                            selectedEgg = egg;
                    }
                    else
                    {
                        place.EggPool.RemoveAt(i);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.Space(0, true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(5);
            }
            EditorGUILayout.EndVertical();

            EditorGUI.BeginDisabledGroup(selectedEgg == null);
            if (GUI.Button(new Rect(eggListRect.x + 5, eggListRect.y + eggListRect.height - 23, 80, 18), new GUIContent("Remove")))
                RemoveEgg(selectedEgg);
            EditorGUI.EndDisabledGroup();

            ScrollPosition = GUILayout.VerticalScrollbar(ScrollPosition, 3, 0, ScrollBarMax, new GUIStyle(GUI.skin.verticalScrollbar) { stretchHeight = true });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();

            Event evt = Event.current;
            switch(evt.rawType)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!eggListRect.Contains(evt.mousePosition))
                        return;
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        foreach (var egg in DragAndDrop.objectReferences)
                        {
                            if (egg is EggSO eggSO && !place.EggPool.Contains(eggSO))
                                place.EggPool.Add(eggSO);
                        }
                    }
                    break;
                default:
                    break;
            }

            so.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("<color=red>No Place Selected</color>"), 
                new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold });
            EditorGUILayout.EndVertical();
        }

    }

    private void Update()
    {
        if (lastFrameTime + 1f < Time.realtimeSinceStartup)
        {
            lastFrameTime = Time.realtimeSinceStartup;
            eggAnimStep++;
            Repaint();
        }
    }

    void RemoveEgg(EggSO egg)
    {
        place.EggPool.Remove(egg);
        selectedEgg = null;
    }

    Rect GetTextureCoords(Sprite s)
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