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

	[NodeTint("#7d0c0c")]
	[NodeWidth(304)]
	[CreateNodeMenu("Comment")]
	public class CommentNode : DialogueNodeBase {



		[TextArea] public string Comment;


		public override void MoveNext() {
			Debug.LogError("Exiting a comment, INVALID");
		}

		public override void OnEnter() {
			Debug.LogError("Entering a comment, INVALID");
		}


	}





}

