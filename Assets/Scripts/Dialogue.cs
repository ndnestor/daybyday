using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour, IInteractable
{
    public DialogueBoxScript dialogueManager;
    public string[] dialogue;

    public void Start()
    {
        dialogueManager = FindObjectOfType<DialogueBoxScript>();
    }

    public void OnInteract()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
