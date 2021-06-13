
using Game.Registry;
using System.Diagnostics;
using UnityEngine;

namespace Game.Dialogue.Nodes {
	[NodeTint("#bd516a")]
	[CreateNodeMenu("Debug/Log")]
	public class LogNode : DialogueNodeBase {
		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		[TextArea] public string InfoText = "";
		public string argumentId = "";

		public override void MoveNext() {
			base.MoveNext();

		}


		public override void OnEnter() {
			base.OnEnter();

			DialogueGraph fmGraph = graph as DialogueGraph;

			UnityEngine.Debug.Log("[" + argumentId + "]" + "(" + fmGraph.valueRegistry.Get(argumentId) + ") " + InfoText);
		}
	}
}