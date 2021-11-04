using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bookshelf : MonoBehaviour {

	[SerializeField] private string sceneName;

	private void Start()
	{
		InteractionHandler.Instance.RegisterObject("Bookshelf", Interact);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha8)) {
			Interact();
		} else if(Input.GetKeyDown(KeyCode.Alpha9)) {
			Exit();
		}
	}

	// Show the bookshelf selection screen
	private void Interact()
	{
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}

	// Hide the bookshelf selection screen
	private void Exit()
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}
	
}
