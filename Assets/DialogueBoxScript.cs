using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MonoBehaviour
{
    public GameObject BoxImage;
    public Text dialogue;
    public string[] textLines;
    public int currentLine = 0;

    public void Start()
    {
        dialogue.text = textLines[0];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    public void NextLine()
    {
        currentLine++;
        if (currentLine == textLines.Length)
        {
            currentLine = 0;
            HideDialogueBox();
        }
        else
        {
            dialogue.text = textLines[currentLine];
        }
    }

    public void ShowDialogueBox()
    {
        BoxImage.SetActive(true);
    }

    public void HideDialogueBox()
    {
        BoxImage.SetActive(false);
    }
}
