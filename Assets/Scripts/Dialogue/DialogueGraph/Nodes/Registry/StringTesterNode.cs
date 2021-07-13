using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue.Nodes.Registry
{
    [NodeTint(COLOR_MISC_2)]
    [NodeWidth(350)]
    [CreateNodeMenu("Registry/StringTester")]
    public class StringTesterNode : DialogueNodeBase
    {
        [Tooltip("Registry name to test")]
        public string RegistryName;
        
        [Tooltip("String to compare against")]
        [TextArea] public string String;

        [Input(connectionType = ConnectionType.Override)] public Empty enter;
        [Output(connectionType = ConnectionType.Override)] public Empty exitTrue;
        [Output(connectionType = ConnectionType.Override)] public Empty exitFalse;
        public override void MoveNext() {

            DialogueGraph fmGraph = graph as DialogueGraph;

            string exitPortName = "exitTrue";
            string argumentValue = fmGraph.stringRegistry.Get(RegistryName);
            if (!argumentValue.Equals(String)) {
                exitPortName = "exitFalse";
            }

            MoveNext(exitPortName);
        }

        public override void OnEnter() {
            base.OnEnter();
        }
    }
}
