
using Game.Registry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Misc {
	[NodeTint(COLOR_EFFECTOR)]
	[NodeWidth(254)]
	[CreateNodeMenu("Misc/Agent")]
	public class AgentNode : DialogueNodeBase {

		public string AgentName;
		[NodeEnum]
		public AgentEmotions Emotion;


		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Effector exit;


		public override void MoveNext() {
			base.MoveNext();

		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}