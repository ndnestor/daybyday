using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;


//Node types
//Shop menu node
//Quest starter node

//Timer node? (Does something after this time has been passed either in game or what not?)

namespace Game.Dialogue.Nodes {

	[CreateNodeMenu("Entry")]
	public class EntryNode : DialogueNodeBase {
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		public override void MoveNext() {
			base.MoveNext();

		}


		public override void OnEnter() {
			base.OnEnter();
		}
	}
}