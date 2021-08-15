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
		openBookSprite.SetActive(true);
		string remainingText = FillTextbox(leftPageText, 100, contents);
		FillTextbox(rightPageText, 100, remainingText);
	}

	private string FillTextbox(TMP_Text textbox, int charsPerIteration, string text) {
		string remainingText = text;

		while(remainingText.Length != 0) {
			// Add text to the textbox
			string textToAdd = remainingText.Substring(0, charsPerIteration);
			textbox.text += textToAdd;
			
			// Shave off text as needed
			const int loopLimit = 10000;
			int i = 0;
			while(textbox.textInfo.lineCount > maxLinesPerPage) {

				// Remove the last character
				textbox.text.Remove(textbox.text.Length - 1);
				i++;
				if(i > loopLimit) {
					Debug.LogError("Reached loop limit");
					break;
				}
			}

			return remainingText;
		}
		return "";
	}
	
}
