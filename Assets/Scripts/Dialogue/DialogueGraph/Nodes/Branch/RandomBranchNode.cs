using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;
using Random = UnityEngine.Random;

namespace Game.Dialogue.Nodes.Branch {
	[NodeTint(COLOR_MATH)]
	[NodeWidth(254)]
	[CreateNodeMenu("Branch/Random")]
	public class RandomBranchNode : DialogueNodeBase {
		[Serializable]
		public struct CombinerEntery { }

		[Input(connectionType = ConnectionType.Override)] public CombinerEntery enter;
		[Output(connectionType = ConnectionType.Multiple)] public Empty exit;

		public override void MoveNext() {


			DialogueGraph fmGraph = graph as DialogueGraph;

			if (fmGraph.current != this) {
				Debug.LogWarning("Node isn't active");
				return;
			}

			var exitPort = this.GetOutputPort("exit");



			if (exitPort == null) {
				Debug.Log("Invalid exit port '" + exitPort + "' for node " + name);
				fmGraph.current = null;
				return;
			}

			if (!exitPort.IsConnected) {
				fmGraph.current = null;
				return;
			}

			int index = Random.Range(0, exitPort.ConnectionCount);

			var exitNode = exitPort.GetConnection(index);

			if(exitNode == null) {
				Debug.Log("Could not find ending for random branch");
				fmGraph.current = null;
				return;
			}
			DialogueNodeBase node = exitNode.node as DialogueNodeBase;
			node.OnEnter();
		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}
