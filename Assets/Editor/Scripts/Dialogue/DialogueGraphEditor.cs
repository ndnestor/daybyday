using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using static XNodeEditor.NodeEditor;


namespace Game.Dialogue {
	[CustomNodeGraphEditor(typeof(DialogueGraph))]
	public class DialogueGraphEditor : NodeGraphEditor {
		public override string GetNodeMenuName(System.Type type) {
			if (type.Namespace.Contains("Dialogue.Nodes")) {
				return base.GetNodeMenuName(type);
			} else return null;
		}
	}

}
