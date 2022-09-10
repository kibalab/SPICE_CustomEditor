using System;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace K13A.BehaviourEditor
{
    public class DetachedEditor : EditorWindow
    {
        public Action Render;
        void OnGUI () {
            if(Render == null) this.Close();
            
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.BeginVertical();
            Render.Invoke();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
    public static class EditorUtil
    {

        public static void DrawOptions(TitleOption[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                options[i].OnGUI(i);
            }
        }

        public static void DrawTitle(string Title, Action contant, ContentStyle style = null, TitleOption[] additionalOptions = null)
        {
            EditorGUILayout.LabelField("", GUI.skin.window, GUILayout.Height(25));
            EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), $"<size=15>    <b>{Title}</b></size>", GUI.skin.label);
            
            var options = TitleOptionsBuilder.Build( new TitleOption[]
            {
                new TitleOption(new GUIContent("Test1"), () =>
                {
                    var winbow = EditorWindow.GetWindow(typeof(DetachedEditor)) as DetachedEditor;
                    winbow.Render = contant;
                    winbow.titleContent = new GUIContent(Title);
                })
            });
            
            if(additionalOptions != null) options.AddRange(additionalOptions);
            
            DrawOptions(options.ToArray());
        }
        
        public static void DrawSubTitle(string Title)
        {
            EditorGUILayout.LabelField("", GUI.skin.window, GUILayout.Height(20), GUILayout.MaxWidth(GUI.skin.window.CalcSize(new GUIContent($"    {Title}    ")).x));
            EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), $"    <b>{Title}</b>", GUI.skin.label);
            GUI.Box(new Rect(
                GUILayoutUtility.GetLastRect().x + GUILayoutUtility.GetLastRect().width + 20, 
                GUILayoutUtility.GetLastRect().y + GUILayoutUtility.GetLastRect().height / 2, 
                 EditorGUIUtility.currentViewWidth - GUILayoutUtility.GetLastRect().x - GUILayoutUtility.GetLastRect().width - 40, 
                2), 
                "");
            GUILayout.Space(5);
        }
        
        public static bool DrawFoldoutTitle(bool b, string Title)
        {
            EditorStyles.foldout.richText = true;
            if (GUILayout.Button("", GUI.skin.window, GUILayout.Height(25)))
                b = !b;
            EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), $"<size=15>    <b>{Title}</b></size>", GUI.skin.label);
            return b;
        }
        
        public static void MenuBox(string Title, Action contant, ContentStyle style = null, TitleOption[] additionalOptions = null)
        {
            var origin_font = GUI.skin.label.font;
            
            EditorGUILayout.BeginVertical();
                DrawTitle(Title, contant, style, additionalOptions);
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                    if(style != null) 
                    {
                        GUI.skin.label.font = style.font;
                        if(style.UpperBorder) EditorGUILayout.Space(10);
                    }
                    EditorGUILayout.BeginVertical();
                        EditorGUILayout.Space(5);
                        contant.Invoke();
                        GUILayout.Space(5);
                    EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(5);
            
            GUI.skin.label.font = origin_font;
        }
        
        public static void SubMenuBox(string Title, Action contant, ContentStyle style = null)
        {
            var origin_font = GUI.skin.label.font;
            DrawSubTitle(Title);
            if(style != null) GUI.skin.label.font = style.font;
            
            EditorGUILayout.BeginVertical();
            contant.Invoke();
            EditorGUILayout.EndVertical();
            
            GUI.skin.label.font = origin_font;
        }
        
        public static bool FoldoutMenuBox(string Title, bool b, Action contant, ContentStyle style = null)
        {
            var origin_font = GUI.skin.label.font;
            b = DrawFoldoutTitle(b, Title);

            if (!b)
            {
                GUILayout.Space(5);
                return b;
            }
            
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                if (style != null)
                {
                    GUI.skin.label.font = style.font; 
                    if(style.UpperBorder) EditorGUILayout.Space(10);
                }
                EditorGUILayout.BeginVertical();
                    EditorGUILayout.Space(5);
                    contant.Invoke();
                    GUILayout.Space(5);
                EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            GUI.skin.label.font = origin_font;
            
            EditorGUILayout.Space(5);
            
            return b;
        }

        public static void Slider(ref float value, float leftValue, float rightValue, float warningValue, float ErrorValue, Action OnWarnEnter = null, Action OnErrorEnter = null)
        {
            var warnStyle = new GUIStyle(GUI.skin.box);
            warnStyle.margin = warnStyle.padding = warnStyle.border = new RectOffset(0, 0, 0, 0);
            
            GUILayout.Box(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(17));
            // For Fix on Editor Layout

            var lastRect = GUILayoutUtility.GetLastRect(); 
            lastRect.x += warningValue * (lastRect.width - 65);
            lastRect.width = lastRect.width - lastRect.x;
            lastRect.height = 8;
            lastRect.y += lastRect.height;
            GUI.Box(lastRect, CreateBakcgroundColor( Mathf.RoundToInt(lastRect.width), Mathf.RoundToInt(lastRect.height), new Color(0.5f, 0.5f, 0, 0.5f)), warnStyle);
            
            lastRect = GUILayoutUtility.GetLastRect();
            lastRect.x += ErrorValue * (lastRect.width - 65);
            lastRect.width = lastRect.width - lastRect.x;
            lastRect.height = 8;
            lastRect.y += lastRect.height;
            GUI.Box(lastRect, CreateBakcgroundColor( Mathf.RoundToInt(lastRect.width), Mathf.RoundToInt(lastRect.height), new Color(0.5f, 0.2f, 0.2f, 0.5f)), warnStyle);

            value = EditorGUI.Slider(GUILayoutUtility.GetLastRect(), value, leftValue, rightValue);

            if (value > ErrorValue)
            {
                if(OnErrorEnter != null) OnErrorEnter.Invoke();
            }
            else if (value > warningValue)
            {
                if(OnWarnEnter != null) OnWarnEnter.Invoke();
            }
        }

        public static GUIContent CreateBakcgroundColor(int w, int h, Color c)
        {
            var tex = new Texture2D(w*10, h*10);

            var pixels = new Color[w * 10 * h * 10];

            for (var i = 0 ; i< pixels.Length ; i++)
            {
                pixels[i] = c;
            }
            
            tex.SetPixels(pixels);
            
            tex.Apply();

            return new GUIContent(tex);
        }
    }
}