using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Dialogue;
using UnityEngine;

public class Door : MonoBehaviour {

	[SerializeField] private DialogueGraph dialogueGraph;
	
	private DialogueSystem dialogueSystem;
	
	private void Start() {
		dialogueSystem = MainInstances.Get<DialogueSystem>();

		InteractionHandler.Instance.RegisterObject("Room and Door", () =>
		{
			Movement2D.Instance.SetPlayerControl(false);
			dialogueSystem.Present(dialogueGraph, () => Movement2D.Instance.SetPlayerControl(true));
		}, 0);
	}

}
