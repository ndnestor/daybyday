using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Inline {
	[NodeTint(COLOR_MATH)]
	[NodeWidth(254)]
	[CreateNodeMenu("Inline/Pause")]
	public class PauseNode : DialogueNodeBase {

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		public float PauseTime = 1;

		public override void MoveNext() {
			base.MoveNext();
		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}
