using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Misc {
	[NodeTint(COLOR_EFFECTOR)]
	[NodeWidth(254)]
	[CreateNodeMenu("Misc/TextEffect")]
	public class TextEffectNode : DialogueNodeBase {

		public float TextSpeed;

		public TextEffect effect;
		public enum TextEffect {
			normal, wavey
		}

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