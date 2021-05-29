using Game.Registry;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Math {
	[NodeWidth(304)]
	[NodeTint(COLOR_MATH)]
	[CreateNodeMenu("Math/Setter")]
	public class SetterNode : DialogueNodeBase {


		public string argumentId;
		[NodeEnum]
		public SetterType type = SetterType.SET;
		public int value;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		public enum SetterType {
			SET, ADD, SUBTRACT, MULTIPLY, DIVIDE, MODULUS
		}

		public override void MoveNext() {
			base.MoveNext();
		}

		public override void OnEnter() {

			base.OnEnter();
			var valueRegistry = ((DialogueGraph)graph).valueRegistry;
			switch (type) {
				case SetterType.SET:

					valueRegistry.Set(argumentId, value);

					break;
				case SetterType.ADD:

					valueRegistry.Set(argumentId, valueRegistry.Get(argumentId) + value);

					break;
				case SetterType.SUBTRACT:

					valueRegistry.Set(argumentId, valueRegistry.Get(argumentId) - value);

					break;
				case SetterType.MULTIPLY:

					valueRegistry.Set(argumentId, valueRegistry.Get(argumentId) * value);

					break;
				case SetterType.DIVIDE:

					valueRegistry.Set(argumentId, valueRegistry.Get(argumentId) / value);

					break;
				case SetterType.MODULUS:

					valueRegistry.Set(argumentId, valueRegistry.Get(argumentId) % value);

					break;
			}
			//Set argumentID;
		}
	}
}
