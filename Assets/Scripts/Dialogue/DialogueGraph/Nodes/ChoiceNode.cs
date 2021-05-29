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


	[NodeWidth(304)]
	[CreateNodeMenu("Dialogue/Choice")]
	public class ChoiceNode : DialogueNodeBase {

		[TextArea] public string Dialogue;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override, dynamicPortList = true)] public List<Choice> choices = new List<Choice>();

		public bool ErasePrevious = true;

		[System.Serializable]
		public struct Choice {
			public string Name;
			[TextArea] public string text;
		}


		private int selectedIndex = 0;

		public void SetChoice(int i) {
			selectedIndex = i;
		}

		public override void MoveNext() {

			string exitPort = "exit";
			if (choices.Count != 0) {
				exitPort = "choices " + selectedIndex;
			}
			Debug.Log("Going to " + exitPort);

			if(selectedIndex > choices.Count - 1 || selectedIndex < 0) {
				Debug.LogWarning("Index choosen does not match our index! Exiting dialogue.");
				return;
			}

			if(choices.Count > 5) {
				Debug.LogWarning("There are too many choices in this dialogue! Exiting dialogue.");
				return;
			}

			MoveNext(exitPort);

		}

		public override void OnEnter() {
			base.OnEnter();
		}
	}
}