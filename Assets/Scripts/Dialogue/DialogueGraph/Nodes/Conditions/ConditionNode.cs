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
	[CreateNodeMenu("Conditions/OneArgument")]
	public class ConditionNode : DialogueNodeBase {



		public string argumentID;
		[NodeEnum]
		public Condition condition;
		public int value;


		[Input(connectionType = ConnectionType.Override)] public Empty enter;


		[Output(connectionType = ConnectionType.Override)] public ConditionExit exitTrue;
		[Output(connectionType = ConnectionType.Override)] public ConditionExit exitFalse;
		public enum Condition {
			LESS, GREATER, LESS_EQUAL, GREAT_EQUAL, NOT_EQUAL, EQUAL
		}


		public override void MoveNext() {
			string exitPortName = "exitTrue";

			bool boolValue;

			DialogueGraph fmGraph = graph as DialogueGraph;

			int argumentValue = fmGraph.valueRegistry.Get(argumentID);

			switch (condition) {
				case Condition.LESS:
					boolValue = argumentValue < value;
					break;
				case Condition.GREATER:
					boolValue = argumentValue > value;
					break;
				case Condition.LESS_EQUAL:
					boolValue = argumentValue <= value;
					break;
				case Condition.GREAT_EQUAL:
					boolValue = argumentValue <= value;
					break;
				case Condition.NOT_EQUAL:
					boolValue = argumentValue != value;
					break;
				case Condition.EQUAL:
					boolValue = argumentValue == value;
					break;
			}

			MoveNext(exitPortName);
		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}