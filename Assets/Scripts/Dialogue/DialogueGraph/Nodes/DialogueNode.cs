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

	public abstract class DialogueNodeBase : Node {

		public const string COLOR_EFFECTOR = "#61B547";
		public const string COLOR_MATH = "#B55CA7";
		public const string COLOR_CONDITIONS = "#5086B5";
		public const string COLOR_MISC = "#B5873E";
		public const string COLOR_MISC_2 = "#3878c2";

		public virtual void MoveNext() {
			MoveNext("exit");
		}


		public void MoveNext(string ExitPort) {

			DialogueGraph fmGraph = graph as DialogueGraph;

			if (fmGraph.current != this) {
				Debug.LogWarning("Node isn't active");
				return;
			}

			NodePort exitPort = GetOutputPort(ExitPort);

			if(exitPort == null) {
				Debug.Log("Invalid exit port '" + exitPort + "' for node " + name);
				return;
			}

			if (!exitPort.IsConnected) {
				fmGraph.current = null;
				return;
			}


			DialogueNodeBase node = exitPort.Connection.node as DialogueNodeBase;
			node.OnEnter();
		}

		public virtual void OnEnter() {
			DialogueGraph fmGraph = graph as DialogueGraph;
			fmGraph.current = this;
		}

		public override object GetValue(NodePort port) {
			return typeof(DialogueNodeBase);
		}

		[Serializable]
		public class Empty { }
		[Serializable]
		public class ConditionExit { }
		[Serializable]
		public class Effector { }
	}


	[NodeWidth(304)]
	[CreateNodeMenu("Dialogue/Text")]
	public class DialogueNode : DialogueNodeBase {



		[TextArea] public string Dialogue;

		public bool WaitForInput = true;
		public bool ErasePrevious = true;

		[Input(connectionType = ConnectionType.Override)] public Empty enter;
		[Output(connectionType = ConnectionType.Override)] public Empty exit;

		

		public override void MoveNext () {
			base.MoveNext();

		}

		public override void OnEnter() {
			base.OnEnter();
		}


	}





}

