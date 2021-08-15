using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{

	[SerializeField] private GameObject bookshelfSelectionScreen;

	private void Start()
	{
		InteractionHandler.Instance.RegisterObject("Bookshelf", Interact);
	}

	// Show the bookshelf selection screen
	private void Interact()
	{
		bookshelfSelectionScreen.SetActive(true);
	}

	private void Ext()
	{
		bookshelfSelectionScreen.SetActive(false);
	}
	
}
