using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bookshelf : MonoBehaviour
{

	[SerializeField] private AudioClip bookshelfThemeSong;
	[SerializeField] private string sceneName;

	private void Start()
	{
		InteractionHandler.Instance.RegisterObject("Bookshelf", Interact, 4);
	}

	// Show the bookshelf selection screen
	private void Interact()
	{
		SceneLoader.Instance.LoadAsync(sceneName, LoadSceneMode.Additive, true);

		MusicPlayer.Instance.loopDelay = 0f;
		MusicPlayer.Instance.fadeDuration = 0f;
		MusicPlayer.Instance.StopMusic();
		MusicPlayer.Instance.QueueMusic(bookshelfThemeSong, true);
	}

}
