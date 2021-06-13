using JetBrains.Annotations;
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
	[CreateNodeMenu("Branch/WeightedRandom")]
	public class WeightedRandomBranchNode : DialogueNodeBase {
		[Serializable]
		public struct CombinerEntery { }

		[Input(connectionType = ConnectionType.Override)] public CombinerEntery enter;
		[Output(connectionType = ConnectionType.Override, dynamicPortList = true)] public List<int> exits = new List<int>();


		public override void MoveNext() {


			DialogueGraph fmGraph = graph as DialogueGraph;

			if (fmGraph.current != this) {
				Debug.LogWarning("Node isn't active");
				return;
			}



			int totalWeight = 0;

			for(int i = 0; i < exits.Count; i++) {
				totalWeight += exits[i];
			}

			int randomWeight = Random.Range(0, totalWeight + 1);

			string exitName = "exits 0";

			for(int i = 0; i < exits.Count; i++) {
				randomWeight -= exits[i];

				if(randomWeight <= 0) {
					exitName = "exits " + i;
					break;
				}
			}

			var exitPort = this.GetOutputPort(exitName);

			

			if (exitPort == null) {
				Debug.Log("Invalid exit port '" + exitPort + "' for node " + name);
			}

			if (!exitPort.IsConnected) {
				fmGraph.current = null;
				return;
			}


			int index = Random.Range(0, exitPort.ConnectionCount);

			var exitNode = exitPort.GetConnection(index);


			DialogueNodeBase node = exitNode.node as DialogueNodeBase;
			node.OnEnter();
		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}
