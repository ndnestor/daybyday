using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Dialogue {
    [CustomEditor(typeof(DialogueSystem))]
    public class DialogueSystemEditor : Editor
    {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var dialogueSystem = (DialogueSystem) target;

            if(GUILayout.Button("Manual Start Dialogue")) {
                dialogueSystem.PresentCurrent();
            }
        }
    }
}