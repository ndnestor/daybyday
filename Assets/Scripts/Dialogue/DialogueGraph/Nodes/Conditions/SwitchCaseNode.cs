using Game.Registry;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Conditions {

	[NodeTint(COLOR_CONDITIONS)]
	[NodeWidth(304)]
	[CreateNodeMenu("Conditions/SwitchCase")]
	public class SwitchCaseNode : DialogueNodeBase {

		public string argumentID;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public ConditionExit exitDefault;
		[Output(connectionType = ConnectionType.Override, dynamicPortList = true)] public List<int> cases = new List<int>();

		public override void MoveNext() {
			string exitPortName = "exitDefault";

			DialogueGraph fmGraph = graph as DialogueGraph;
			int argumentValue = fmGraph.valueRegistry.Get(argumentID);

			for (int i = 0; i < cases.Count; i++) {
				if (argumentValue == cases[i]) {
					exitPortName = "cases " + i;
					break;
				}
			}

			MoveNext(exitPortName);
		}

		public override void OnEnter() {
			base.OnEnter();
		}
		//used to check if the value is true. 0 = false, anything else is true
	}
}
