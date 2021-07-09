using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[EditorWindowTitle(title = "Evolution Tree")]
public class EvolutionEditor : EditorWindow
{
    float scrollBarPos;
    Vector2 infoPos;
    Vector2 treePos;
    GUIStyle invisibutton;
    PetScriptableObject growthTree;
    [SerializeField]
    GrowthStage selectedStage = null;
    [SerializeField]
    GrowthTransition selectedTransition = null;

    [MenuItem("Window/Evolution Tree")]
    public static void ShowWindow()
    {
        var window = GetWindow<EvolutionEditor>(typeof(EvolutionEditor));

        window.invisibutton = new GUIStyle();
        Texture2D bg = new Texture2D(1, 1);
        bg.SetPixel(0, 0, Color.clear);
        bg.Apply();
        window.invisibutton.normal.background = bg;
    }

    private void OnGUI()
    {
        if (Selection.objects.Length > 0 && Selection.objects[0].GetType() == typeof(PetScriptableObject))
            growthTree = (PetScriptableObject)Selection.objects[0];

        if(growthTree != null)
            if (growthTree.Stages == null || growthTree.Stages.Count == 0)
                growthTree.Stages = new List<GrowthStage>() { growthTree.InitialStage };

        float inspectorWidth = 350;
        EditorGUILayout.BeginHorizontal();
        infoPos = GUILayout.BeginScrollView(infoPos, GUILayout.Width(inspectorWidth));
        EditorGUI.DrawRect(new Rect(0, 0,inspectorWidth, position.height), new Color(45 / 255f, 45 / 255f, 45 / 255f));
        EditorGUI.DrawRect(new Rect(0, 0,inspectorWidth, 26), new Color(33 / 255f, 30 / 255f, 36 / 255f));
        GUILayout.Box("Inspector", new GUIStyle(GUI.skin.box) { fontStyle = FontStyle.Bold, stretchWidth = true });
        if (selectedStage != null && GetStageFromID(selectedStage.ID) == null)
            selectedStage = null;
        if (selectedStage != null && selectedStage.ID != null)
            DrawStageInfo();
        if (selectedTransition != null && selectedTransition.ID != null)
            DrawTransitionInfo();
        GUILayout.EndScrollView();

        treePos = GUILayout.BeginScrollView(treePos, GUILayout.ExpandWidth(true));
        GUILayout.Box("Evolution Tree", new GUIStyle(GUI.skin.box) { fontStyle = FontStyle.Bold, stretchWidth = true });
        GUILayout.EndScrollView();
        Rect treeRect = GUILayoutUtility.GetLastRect();
        EditorGUI.DrawRect(treeRect, new Color(45 / 255f, 45 / 255f, 45 / 255f));
        if (growthTree != null)
        {
            DrawStage(0, growthTree.InitialStage, treeRect, Vector2Int.one);
            DrawTreeChildren(1, growthTree.InitialStage, treeRect);
        }
        EditorGUI.DrawRect(new Rect(treeRect.x, 0, treeRect.width, 26), new Color(33 / 255f, 30 / 255f, 36 / 255f));
        GUI.Box(new Rect(treeRect.x, 0, treeRect.width, 26), "Evolution Tree", 
            new GUIStyle(GUI.skin.box) { fontStyle = FontStyle.Bold, stretchWidth = true, alignment = TextAnchor.MiddleCenter });
        treeRect.y += 20; treeRect.height -= 20;
        scrollBarPos = GUILayout.VerticalScrollbar(scrollBarPos, 100, 0, 1000, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndHorizontal();

        EditorGUI.DrawRect(new Rect(inspectorWidth, 0, 3, position.height), new Color(60 / 255f, 60 / 255f, 60 / 255f));
    }

    void DrawTreeChildren(int depth, GrowthStage startStage, Rect treeRect)
    {
        for (int i = 0; i < startStage.Transitions.Count; i++)
        {
            GrowthStage child = GetStageFromID(startStage.Transitions[i].Destination);
            if (child != null)
            {
                DrawStage(depth, child, treeRect, new Vector2Int(i + 1, startStage.Transitions.Count), startStage);
                DrawTransition(startStage.Display, child.Display, startStage.Transitions[i]);
                if (child.Transitions.Count > 0)
                {
                    DrawTreeChildren(depth + 1, child, treeRect);
                }
            }
            else
            {
                startStage.Transitions.RemoveAt(i);
                break;
            }
        }
    }

    void DrawStage(int depth, GrowthStage stage, Rect treeRect, Vector2Int ChildIndex, GrowthStage parent = null)
    {
        //offset from center
        float xOffset = 0;
        if(parent != null)
        {
            xOffset = parent.Display.x + parent.Display.width / 2 - treeRect.x - treeRect.width / 2;
        }

        Sprite icon = stage.Icon;
        if (icon)
        {
            Rect stageRect = new Rect(treeRect.x + treeRect.width / (1 + ChildIndex.y) * ChildIndex.x - (icon.rect.width * 3 / 2) + xOffset, 
                150 * depth - icon.rect.height * 3 / 2 - scrollBarPos + 50, icon.rect.width * 3, icon.rect.height * 3);
            if (selectedStage != null && selectedStage.ID == stage.ID)
                EditorGUI.DrawRect(new Rect(stageRect.x - 6, stageRect.y - 6, stageRect.width + 12, stageRect.height + 12), new Color(58 / 255f, 121 / 255f, 187 / 255f));
            if (GUI.Button(new Rect(stageRect.x - 5, stageRect.y - 5, stageRect.width + 10, stageRect.height + 10), ""))
            {
                selectedTransition = null;
                selectedStage = stage;
                SaveChanges();
            }
            GUI.DrawTextureWithTexCoords(stageRect, icon.texture, GetTextureCoords(stage.Icon));
            stage.Display = stageRect;
            stage.Depth = depth;
        }
        else
        {
            Rect stageRect = new Rect(treeRect.x + treeRect.width / (1 + ChildIndex.y) * ChildIndex.x - 50 + xOffset, 150 * depth - 16 - scrollBarPos + 60, 100, 32);
            if (selectedStage != null && selectedStage.ID == stage.ID)
                EditorGUI.DrawRect(new Rect(stageRect.x - 6, stageRect.y - 6, stageRect.width + 12, stageRect.height + 12), new Color(58 / 255f, 121 / 255f, 187 / 255f));
            if (GUI.Button(new Rect(stageRect.x - 5, stageRect.y - 5, stageRect.width + 10, stageRect.height + 10), "No Preview"))
            {
                selectedTransition = null;
                selectedStage = stage;
                SaveChanges();
            }
            if(stage != null)
            {
                stage.Display = stageRect;
                stage.Depth = depth;
            }
        }
    }

    void DrawTransition(Rect a, Rect b, GrowthTransition transition)
    {
        Rect ArrowRect = new Rect(x: b.x + b.width / 2, y: a.y + a.height + 5, width: a.x - b.x - b.width / 2 + a.width / 2 + 2, height: Mathf.Abs(a.y - b.y) - a.height - 10);
        Texture2D Arrow = new Texture2D((int)Mathf.Abs(ArrowRect.width), (int)Mathf.Abs(ArrowRect.height), TextureFormat.RGBA32, false);
        Color[] pixels = new Color[(int)Mathf.Abs(ArrowRect.width) * (int)Mathf.Abs(ArrowRect.height)];

        Color selectedCol = new Color(58 / 255f, 121 / 255f, 187 / 255f);
        Color bgCol = new Color(70 / 255f, 60 / 255f, 60 / 255f, 0f);

        for (int i = 0; i < pixels.Length; i++)
        {
            int x = i % (int)ArrowRect.width;
            int y = i / (int)ArrowRect.height;
            if (x == y || x + 1 == y || x - 1 == y)
                pixels[i] = selectedTransition != null && selectedTransition.ID == transition.ID ? selectedCol : Color.white;
            else
                pixels[i] = bgCol;
        }
        Arrow.SetPixels(pixels);
        Arrow.Apply(false);
        GUI.DrawTexture(ArrowRect, Arrow);
        if (GUI.Button(new Rect(ArrowRect.x + ( ArrowRect.width < 0 ? ArrowRect.width : -10), ArrowRect.y, Mathf.Abs(ArrowRect.width) + 20, ArrowRect.height), 
            "", invisibutton))
        {
            selectedStage = null;
            selectedTransition = transition;
            SaveChanges();
        }
    }

    void DrawStageInfo()
    {
        SerializedObject tree = new SerializedObject(this);
        tree.Update();
        SerializedProperty stage = tree.FindProperty(nameof(selectedStage));
        EditorGUI.BeginChangeCheck();
        EditorGUIUtility.labelWidth = 75;
        EditorGUILayout.LabelField("<color=white>Inspecting Stage</color>",
            new GUIStyle() { alignment = TextAnchor.MiddleCenter });
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("ID", selectedStage.ID);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.PropertyField(stage.FindPropertyRelative(nameof(GrowthStage.Icon)), new GUIContent("Icon"),
            GUILayout.Height(EditorGUIUtility.singleLineHeight));
        EditorGUILayout.PropertyField(stage.FindPropertyRelative(nameof(GrowthStage.Animator)), new GUIContent("Animator"));

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Transition"))
        {
            var newStage = new GrowthStage();
            growthTree.Stages.Add(newStage);
            selectedStage.Transitions.Add(new GrowthTransition(newStage.ID));
        }
        EditorGUI.BeginDisabledGroup(selectedStage.Depth <= 0);
        if (GUILayout.Button("Delete Stage"))
        {
            RemoveStageReferences();
            growthTree.Stages.Remove(selectedStage);
            selectedStage = null;
        }
        GUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();
        if (EditorGUI.EndChangeCheck())
        {
            SaveChanges();
            tree.ApplyModifiedProperties();
        }
        tree.Dispose();
    }

    void DrawTransitionInfo()
    {
        SerializedObject tree = new SerializedObject(this);
        tree.Update();
        SerializedProperty transition = tree.FindProperty(nameof(selectedTransition));
        EditorGUI.BeginChangeCheck();
        EditorGUIUtility.labelWidth = 75;
        EditorGUILayout.LabelField("<color=white>Inspecting Transition</color>", new GUIStyle() { alignment = TextAnchor.MiddleCenter });
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField( transition.FindPropertyRelative(nameof(GrowthTransition.ID)), new GUIContent("ID"));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.PropertyField(transition.FindPropertyRelative(nameof(GrowthTransition.Requirements)), new GUIContent("Requirements"));
        if (EditorGUI.EndChangeCheck())
        {
            SaveChanges();
            tree.ApplyModifiedProperties();
        }
        tree.Dispose();
    }

    GrowthStage GetStageFromID(string id)
    {
        foreach (var stage in growthTree.Stages)
        {
            if (stage.ID == id)
                return stage;
        }
        return null;
    }

    void RemoveStageReferences()
    {
        RemoveChildrenRecoursive(selectedStage);
        foreach (var stage in growthTree.Stages)
        {
            List<GrowthTransition> RemovalList = new List<GrowthTransition>();
            for (int i = 0; i < stage.Transitions.Count; i++)
            {
                if (stage.Transitions[i].Destination == selectedStage.ID)
                    stage.Transitions[i] = null;
            }
            stage.Transitions.RemoveAll(t => t == null);
        }
    }

    void RemoveChildrenRecoursive(GrowthStage parent)
    {
        foreach (var transition in parent.Transitions)
        {
            var child = GetStageFromID(transition.Destination);
            if (child.Transitions.Count > 0)
                RemoveChildrenRecoursive(child);
            growthTree.Stages.Remove(child);
        }
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

    public override void SaveChanges()
    {
        EditorUtility.SetDirty(growthTree);
        AssetDatabase.SaveAssets();
    }
}
