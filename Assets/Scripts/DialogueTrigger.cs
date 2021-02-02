using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public void Update() {

        if (Input.GetKeyDown("e")) {
            TriggerDialogue();
        }

        if (Input.GetKeyDown("space")) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }

    }


    public void TriggerDialogue() {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }

}
