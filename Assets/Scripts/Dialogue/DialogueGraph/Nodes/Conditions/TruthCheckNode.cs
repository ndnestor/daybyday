using Game.Registry;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace Game.Dialogue.Nodes.Conditions {

	[NodeTint(COLOR_CONDITIONS)]
	[NodeWidth(304)]
	[CreateNodeMenu("Conditions/TruthCheck")]
	public class TruthCheckNode : DialogueNodeBase {

		public string argumentID;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public ConditionExit exitTrue;
		[Output(connectionType = ConnectionType.Override)] public ConditionExit exitFalse;

		public override void MoveNext() {

			DialogueGraph fmGraph = graph as DialogueGraph;

			string exitPortName = "exitTrue";
			int argumentValue = fmGraph.valueRegistry.Get(argumentID);
			if (argumentValue == 0) {
				exitPortName = "exitFalse";
			}

			MoveNext(exitPortName);
		}

		public override void OnEnter() {
			base.OnEnter();
		}
		//used to check if the value is true. 0 = false, anything else is true
	}
}
