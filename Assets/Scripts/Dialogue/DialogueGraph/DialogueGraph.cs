using Game.Dialogue.Nodes;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using XNode;
using Game.Registry;
using Game;

namespace Game.Dialogue {
	[CreateAssetMenu(fileName = "Dialogue Graph", menuName = "Graphs/Dialogue Graph")]
	public class DialogueGraph : NodeGraph {
		public DialogueNodeBase current;
		public DialogueNodeBase entry;

		public DialogueSystem currentSystem;

		public int CurrentChoiceIndex;

		[HideInInspector]
		public ValueRegistry valueRegistry;
		[HideInInspector]
		public StringRegistry stringRegistry;
		[HideInInspector]
		public AgentRegistry agentRegistry;

		public void Continue() {

			//Debug.Log("Moving from node " + current.name);

			if (current == null) {
				//We are done
				valueRegistry = null;
				agentRegistry = null;
				stringRegistry = null;
				//Dereference
				return;
			}

			current.MoveNext();
			//Debug.Log("To node " + current.name);
		}



		public void Play(DialogueSystem system) {
			if (!Verify()) {
				Debug.Log("Failed to play DialogueGraph: " + name);
				return;
			}

			//Get dependencies

			valueRegistry = MainInstances.Get<ValueRegistry>();
			agentRegistry = MainInstances.Get<AgentRegistry>();
			stringRegistry = MainInstances.Get<StringRegistry>();

			currentSystem = system;
			current = entry;

			current.MoveNext();

			//We started the graph

		}

		[ContextMenu("Verify")]
		public bool Verify() {

			//Check if only one entry
			int entryNodes = 0;

			foreach (var node in this.nodes) {

				if (node is EntryNode) {
					entryNodes++;
					entry = node as EntryNode;
				}


			}

			bool verifed = true;

			if (entryNodes != 1) {
				Debug.LogError("DialogueGraph '" + name + "' only supports one entry node! You have " + entryNodes);
				verifed = false;
			}

			//Need to verify that you will never infinite loop without a halt. Aka thing -> combiner -> truth check -> back to that combiner. If the truth check or anything that doesn't halt does not pass an halt before returning to the same 

			//Need to verify that there is always an exit condition, that you will never be looping at any stage of the nodes.

			//Need to verify if all choices have exits
			
			return verifed;

		}
	}

}
