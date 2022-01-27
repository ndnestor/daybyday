using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bookshelf : MonoBehaviour
{

	[SerializeField] private string sceneName;

	private void Start()
	{
		InteractionHandler.Instance.RegisterObject("Bookshelf", Interact, 4);
	}

	// Show the bookshelf selection screen
	private void Interact()
	{
		SceneManager.LoadSceneAsync(sceneName);
	}

}
