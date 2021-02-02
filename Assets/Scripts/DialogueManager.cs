using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {

        animator.SetBool("IsOpen", true);

        Debug.Log("Dialogue Began");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }

        Debug.Log("left the loop");

        DisplayNextSentence();

    }

    public void DisplayNextSentence() {

        Debug.Log(sentences.Count);

        if (sentences.Count == 0) {
            Debug.Log("inside the if statement before ending");
            EndDialogue();
            return;
        }

        Debug.Log("go straight here??");

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence) {

        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }

    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of dialogue.");
    }

}
