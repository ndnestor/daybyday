using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue.Nodes.Registry
{
    [NodeTint(COLOR_MISC_2)]
    [NodeWidth(350)]
    [CreateNodeMenu("Registry/StringSetter")]
    public class StringSetterNode : DialogueNodeBase
    {
        [Tooltip("Registry name to set")]
        public string RegistryName;
        [Tooltip("Registry value to set to")]
        [TextArea]public string RegistryValue;

        [Input(connectionType = ConnectionType.Override)] public Empty enter;
        [Output(connectionType = ConnectionType.Override)] public Empty exit;
		
        public override void MoveNext() {
            base.MoveNext();

        }

        public override void OnEnter() {
            base.OnEnter();
            
            var stringRegistry = ((DialogueGraph)graph).stringRegistry;
            
            stringRegistry.Set(RegistryName, RegistryValue);
        }
    }
}
