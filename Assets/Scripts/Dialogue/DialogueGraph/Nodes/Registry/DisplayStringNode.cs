using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue.Nodes.Registry
{
    [NodeTint(COLOR_MISC_2)]
    [NodeWidth(254)]
    [CreateNodeMenu("Registry/DisplayString")]
    public class DisplayStringNode : DialogueNodeBase
    {
        [Tooltip("Registry value to display in dialogue box")]
        public string RegistryValueName;
        public bool WaitForInput = true;
        public bool ErasePrevious = true;

        [Input(connectionType = ConnectionType.Override)] public Empty enter;
        [Output(connectionType = ConnectionType.Override)] public Empty exit;
		
        public override void MoveNext() {
            base.MoveNext();

        }

        public override void OnEnter() {
            base.OnEnter();
        }
    }
}
