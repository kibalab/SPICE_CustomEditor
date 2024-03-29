using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools.Constraints;

namespace K13A.BehaviourEditor
{
    
    public class TitleOption
    {
        #region Fields

        public GUIContent Content;
        public Action action;
        public GUIStyle Style;
        public int id;

        #endregion

        #region Properties

        public TitleOption(GUIContent Content, Action action, int id = default)
        {
            this.Content = Content;
            this.action = action;
            this.Style = new GUIStyle(GUI.skin.window);
            this.id = id;
            //this.Style.normal.background * 10;
            
            Style.contentOffset = Vector2.zero;
            Style.padding = new RectOffset(0, 0, 0, 0);
            Style.alignment = TextAnchor.MiddleCenter;
        }
        
        #endregion

        #region Functions

        public void OnGUI(int ID)
        {   
            var b = GUI.Button(new Rect(
                GUILayoutUtility.GetLastRect().x + GUILayoutUtility.GetLastRect().width - (ID+1) * 25, 
                GUILayoutUtility.GetLastRect().y + 3, 
                20, 
                18), "N", Style);

            if (b)
            {
                action.Invoke();;
            }
        }

        #endregion
    }
}