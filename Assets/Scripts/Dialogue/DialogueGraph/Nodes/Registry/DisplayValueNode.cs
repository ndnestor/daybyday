using System.Collections;
using System.Collections.Generic;
using Game.Dialogue.Nodes;
using UnityEngine;
using XNode;

namespace Game.Dialogue.Nodes.Registry
{
	[NodeTint(COLOR_MISC)]
	[NodeWidth(254)]
	[CreateNodeMenu("Registry/DisplayValue")]
	public class DisplayValueNode : DialogueNodeBase
	{
		[Tooltip("Registry value to display in dialogue box")]
		public string RegistryValueName;
		public bool WaitForInput = true;
		public bool ErasePrevious = true;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;
		
		public override void MoveNext() {
			base.MoveNext();

		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}