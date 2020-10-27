using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MonoBehaviour
{
    public GameObject DialogueBox;
    public Text text;
    [SerializeField] public Queue<string> dialogue = new Queue<string>();

    public Interact player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        //Debug.Log(dialogue.Peek());
        if (dialogue.Count > 0)
        {
            text.text = dialogue.Dequeue();

        }
        else
        {
            HideDialogueBox();
        }
    }

    IEnumerator TypeLine(string line)
    {
        text.text = "";
        foreach (char letter in line.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }

    private void ShowDialogueBox()
    {
        DialogueBox.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HideDialogueBox()
    {
        DialogueBox.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
    }

    public void StartDialogue(string[] _dialogue)
    {
        if (DialogueBox.activeSelf == true)
            return;
        dialogue.Clear();
        foreach(string line in _dialogue)
        {
            dialogue.Enqueue(line);
        }

        ShowDialogueBox();
        NextLine();
    }
}
