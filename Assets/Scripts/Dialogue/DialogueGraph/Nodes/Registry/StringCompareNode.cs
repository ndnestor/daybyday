using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue.Nodes.Registry
{
    [NodeTint(COLOR_MISC_2)]
    [NodeWidth(350)]
    [CreateNodeMenu("Registry/StringCompare")]
    public class StringCompareNode : DialogueNodeBase
    {
        [Tooltip("Registry name to test")]
        public string RegistryName;
        
        [Tooltip("Registry name to compare against")]
        public string OtherRegistry;

        [Input(connectionType = ConnectionType.Override)] public Empty enter;
        [Output(connectionType = ConnectionType.Override)] public Empty exitTrue;
        [Output(connectionType = ConnectionType.Override)] public Empty exitFalse;
        public override void MoveNext() {

            DialogueGraph fmGraph = graph as DialogueGraph;

            string exitPortName = "exitTrue";
            string argumentValue = fmGraph.stringRegistry.Get(RegistryName);
            if (!argumentValue.Equals(fmGraph.stringRegistry.Get(OtherRegistry))) {
                exitPortName = "exitFalse";
            }

            MoveNext(exitPortName);
        }

        public override void OnEnter() {
            base.OnEnter();
        }
    }
}