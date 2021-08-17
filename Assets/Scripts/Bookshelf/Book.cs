using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Book : MonoBehaviour
{

	[SerializeField] private string title;
	[SerializeField] private string author;
	[SerializeField] private string contents;

	[SerializeField] private GameObject openBookSprite;
	[SerializeField] private TMP_Text leftPageText;
	[SerializeField] private TMP_Text rightPageText;

	[SerializeField] private int maxLinesPerPage;

	public void OpenBook()
	{
		Debug.Log($"Opening book {title} by {author}");
		openBookSprite.SetActive(true);
		string remainingText = FillTextbox(leftPageText, 100, contents);
		FillTextbox(rightPageText, 100, remainingText);
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.B)) {
			OpenBook();
		}
	}

	private string FillTextbox(TMP_Text textbox, int charsPerIteration, string text) {
		string remainingText = text;
		
		const int loopLimit = 2000;
		int i = 0;

		while(remainingText.Length != 0) {
			
			// Add text to the textbox
			string textToAdd;
			if(charsPerIteration > remainingText.Length) {
				textToAdd = remainingText;
			} else {
				textToAdd = remainingText.Substring(0, charsPerIteration);
			}
			textbox.text += textToAdd;

			// Shave off text as needed
			textbox.ForceMeshUpdate();
			Debug.Log(textbox.textInfo.lineCount);

			bool overshot = false;
			if(textbox.textInfo.lineCount > maxLinesPerPage) {
				overshot = true;
				
				while(textbox.textInfo.lineCount > maxLinesPerPage) {

					// Remove the last character
					textbox.text = textbox.text.Substring(0, textbox.text.LastIndexOf(' '));
					textbox.ForceMeshUpdate();

					i++;
					if(i > loopLimit) {
						Debug.LogError("Reached loop limit");
						return remainingText;
					}
				}
			}

			if(overshot) {
				return remainingText;
			}

			remainingText = text.Substring(textbox.text.Length);

			i++;
			if(i > loopLimit) {
				Debug.LogError("Reached loop limit");
				return remainingText;
			}
		}
		return "";
	}
	
}
