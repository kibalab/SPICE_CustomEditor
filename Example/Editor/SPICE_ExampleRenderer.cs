using System.Collections;
using System.Collections.Generic;
using K13A.BehaviourEditor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SPICE_ExampleScript))]
public class SPICE_ExampleRenderer : Editor
{
    public SPICE_ExampleScript origin;

    public static List<TitleOption> Options = new List<TitleOption>();

    public float value;

    public override void OnInspectorGUI()
    {
        origin = (SPICE_ExampleScript)target;

        GUI.skin.label.richText = true;
        GUILayout.Space(20);
        var titleStyle = new GUIStyle();
        titleStyle.normal.background = null;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Box(origin.Logo, titleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(100));
        GUILayout.Space(20);



        EditorUtil.MenuBox(origin.MenuBoxTitle, () =>
        {
            origin.MenuBoxTitle = EditorGUILayout.TextField("Menu Box Title", origin.MenuBoxTitle);
            
            GUILayout.Space(20);
            
            EditorUtil.SubMenuBox(origin.SubMenuBoxTitle, () =>
            {
                origin.SubMenuBoxTitle = EditorGUILayout.TextField("Sub Menu Box Title", origin.SubMenuBoxTitle);
            });
            
            EditorUtil.Slider(ref value, 0, 1, 0.5f, 0.8f);
        });
        
        GUILayout.Space(20);

        EditorUtil.MenuBox("Menu Option Buttons", () =>
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                Options.Add(new TitleOption(new GUIContent($"new Option{Options.Count}"), () =>
                {
                    var winbow = EditorWindow.GetWindow(typeof(DetachedEditor)) as DetachedEditor;
                    winbow.Render = () =>
                    {
                        EditorGUILayout.LabelField($"new Function");
                    };
                    winbow.titleContent = new GUIContent($"new Option");
                }));
            }
            if (GUILayout.Button("-"))
            {
                Options.RemoveAt(Options.Count - 1);
            }
            EditorGUILayout.EndHorizontal();
        }, null, Options.ToArray());

        GUILayout.Space(20);
        
        origin.isFold = EditorUtil.FoldoutMenuBox("Foldout Menu Box", origin.isFold,() =>
        {
            EditorUtil.SubMenuBox("Sub Menu Box", () =>
            {
                EditorGUILayout.LabelField($"is Foldout Box!");
            });
        });
    }
}
