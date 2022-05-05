using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookshelfExitButton : MonoBehaviour
{

	[SerializeField] private Sprite disabledSprite;
	[SerializeField] private Sprite enabledSprite;
	[SerializeField] private float cooldown;
	
	private float activeTime;
	private Button button;
	private Image image;
	
	private void Start()
	{
		activeTime = 0;

		image = GetComponent<Image>();
		image.sprite = disabledSprite;

		button = GetComponent<Button>();
		button.interactable = false;
	}

	private void Update()
	{
		activeTime += Time.deltaTime;

		if(activeTime >= cooldown)
		{
			image.sprite = enabledSprite;
			button.interactable = true;
		}
	}

	public void ExitBookshelf()
	{
		print("clicked");
		SceneManager.UnloadSceneAsync("Bookshelf");
	}
	
}
