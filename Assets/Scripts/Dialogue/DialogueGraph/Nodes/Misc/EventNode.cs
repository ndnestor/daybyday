﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Game.Dialogue.Nodes.Misc {
	[NodeTint(COLOR_MISC)]
	[NodeWidth(254)]
	[CreateNodeMenu("Misc/Event")]
	public class EventNode : DialogueNodeBase {

		//May be a string that is determined outside or it may be an actuall call (not sure howll that work tho)
		public EventSO eventFire;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		public override void MoveNext() {
			base.MoveNext();

		}

		public override void OnEnter() {
			eventFire.InvokeEvent?.Invoke();
			base.OnEnter();
		}
	}
}