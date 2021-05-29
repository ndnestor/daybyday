using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Branch {
	[NodeTint(COLOR_MATH)]
	[NodeWidth(254)]
	[CreateNodeMenu("Branch/Combiner")]
	public class CombinerNode : DialogueNodeBase {
		[Serializable]
		public struct CombinerEntery { }

		[Input(connectionType = ConnectionType.Multiple)] public CombinerEntery enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		public override void MoveNext() {
			base.MoveNext();
		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}
	