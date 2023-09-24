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
	[SerializeField] private TMP_InputField pageNumberInput;
	[SerializeField] private TMP_Text rightPageNumber;
	[SerializeField] private TMP_Text closedBookAuthorText;
	[SerializeField] private TMP_Text openBookAuthorText;
	[SerializeField] private TMP_Text[] titleTexts;
	
	[SerializeField] private int maxLinesPerPage;
	[TextArea(25,30)]
	[SerializeField] private List<string> pageContents;
	[SerializeField] private bool generatePageContents;

	private const string newLineToken = "\\n";

	private GameObject[] closedBooks;
	private int pageNumber = 0;

	private void Start()
	{
		openBookAuthorText.text = author;
		closedBookAuthorText.text = $"By {author}";
		foreach (var bookTitleText in titleTexts)
		{
			bookTitleText.text = title;
		}
		
		FormatPageContents();

		closedBooks = GameObject.FindGameObjectsWithTag("ClosedBook");
	}

	public void OpenBook()
	{
		Debug.Log($"Opening book {title} by {author}");
		
		openBookSprite.SetActive(true);
		foreach (var closedBook in closedBooks) {
			closedBook.SetActive(false);
		}
		
		if(generatePageContents)
		{
			Debug.Log("Generating pages");
			pageContents = new List<string>();
			
			SavePageContents();
			SetPage(1);
		} else
		{
			NextPage();
		}
	}

	public void CloseBook()
	{
		Debug.Log("Closing book");
		openBookSprite.SetActive(false);
		foreach (var closedBook in closedBooks) {
			closedBook.SetActive(true);
		}
	}

	private void SavePageContents()
	{
		string remainingText = contents;

		while(remainingText.Length != 0)
		{
			
			// Increment page number
			pageNumber++;
			
			// Save left page contents
			remainingText = FillTextBox(leftPageText, 100, remainingText);
			pageContents.Add(leftPageText.text);

			// Increment page number
			pageNumber++;
			
			// Save right page contents
			remainingText = FillTextBox(rightPageText, 100, remainingText);
			pageContents.Add(rightPageText.text);

		}
	}

	private void NextPage()
	{

		if(pageNumber + 1 > pageContents.Count)
		{
			return;
		}

		// Increment the page number
		pageNumber++;

		// Return if the visible pages do not change
		if(pageNumber % 2 == 0)
		{
			return;
		}
		
		UpdatePageNumbers(pageNumber);

		// Set the text boxes appropriately
		Debug.Log("Going to next page");
		leftPageText.text = pageContents[pageNumber - 1];
		rightPageText.text = pageContents[pageNumber];
	}

	private void PrevPage()
	{

		if(pageNumber < 2)
		{
			return;
		}

		// Decrement the page
		pageNumber--;
		
		// Return if visible pages do not change
		if(pageNumber % 2 == 1)
		{
			return;
		}
		
		UpdatePageNumbers(pageNumber - 1);
		
		// Set the text boxes appropriately
		Debug.Log("Going to previous page");
		leftPageText.text = pageContents[pageNumber - 2];
		rightPageText.text = pageContents[pageNumber - 1];
	}

	public void NextPageButtonPress()
	{
		NextPage();
		if(pageNumber % 2 == 0)
		{
			NextPage();
		}
	}

	public void PrevPageButtonPress()
	{
		PrevPage();
		if(pageNumber % 2 == 1)
		{
			PrevPage();
		}
	}

	private void SetPage(int newPageNumber)
	{
		
		// Check if page number is valid
		if(newPageNumber < 1 || newPageNumber > pageContents.Count)
		{
			return;
		}

		while(newPageNumber != pageNumber)
		{
			if(newPageNumber < pageNumber)
			{
				PrevPage();
			} else
			{
				NextPage();
			}
		}

	}

	public void SetPage()
	{
		SetPage(int.Parse(pageNumberInput.text));
	}

	private void UpdatePageNumbers(int leftPageNumber) {
		
		// Update page numbers
		pageNumberInput.text = leftPageNumber.ToString();
		rightPageNumber.text = (leftPageNumber + 1).ToString();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			OpenBook();
		} else if(Input.GetKeyDown(KeyCode.N))
		{
			PrevPage();
		} else if(Input.GetKeyDown(KeyCode.M))
		{
			NextPage();
		}
	}
	
	private void FormatPageContents()
	{
		// Add new line characters
		for(int i = 0; i < pageContents.Count; i++) {
			pageContents[i] = pageContents[i].Replace(newLineToken, "\n");
		}

		contents = contents.Replace(newLineToken, "\n");
	}
	
	private string FillTextBox(TMP_Text textBox, int charsPerIteration, string text)
	{
		string remainingText = text;
		int charsAdded = 0;

		textBox.text = "";

		while(remainingText.Length != 0)
		{
			
			// Add text to the textBox
			string textToAdd;
			if(charsPerIteration > remainingText.Length)
			{
				textToAdd = remainingText;
			} else
			{
				textToAdd = remainingText.Substring(0, charsPerIteration);
			}
			textBox.text += textToAdd;
			charsAdded += textToAdd.Length;
			
			// Shave off text as needed
			textBox.ForceMeshUpdate();

			bool overshot = false;
			if(textBox.textInfo.lineCount > maxLinesPerPage)
			{
				overshot = true;
				
				while(textBox.textInfo.lineCount > maxLinesPerPage)
				{
					
					// Remove the last character
					int delimiterIndex = textBox.text.LastIndexOf(' ');
					charsAdded -= textBox.text.Length - delimiterIndex;
					textBox.text = textBox.text.Substring(0, delimiterIndex);
					textBox.ForceMeshUpdate();
					
				}
				
				// Remove extra white space from the start
				charsAdded++;

			}

			remainingText = text.Substring(charsAdded);
			
			if(overshot)
			{
				return remainingText;
			}
		}
		return "";
	}
	
}
