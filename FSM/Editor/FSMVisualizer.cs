using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Misuno
{
    public class FSMVisualizer : EditorWindow
    {
        private IFSMOwner fsmOwner;
        private Object showingGo;

        [MenuItem("Misuno/FSM Viewer")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(FSMVisualizer));
        }

        public void OnGUI()
        {
//            EditorGUILayout.BeginHorizontal();
            showingGo = EditorGUILayout.ObjectField("FSM Owner", showingGo, typeof(Object), true);
//            EditorGUILayout.EndHorizontal();

            if (showingGo == null)
                return;

            var go = (GameObject)showingGo;
            if (go == null)
                return;

            fsmOwner = go.GetComponent<IFSMOwner>();

            if (fsmOwner == null)
            {
                GUILayout.Label("No FSM on this game object");
                return;
            }

            GUILayout.Label("There is a FSM on this game object");

            FSM fsm = fsmOwner.GetRootFsm();

            fsm.Draw(new Vector2(10f, 40f));

        }
    }
}