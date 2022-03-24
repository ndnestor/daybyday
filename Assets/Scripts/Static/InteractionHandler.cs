using System;
using System.Collections.Generic;
using Game;
using Game.Dialogue;
using Game.Registry;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
	private readonly Dictionary<string, Action> registeredObjects = new Dictionary<string, Action>();

	// Used for objects' neglected appearance
	[SerializeField] private Sprite neglectedPianoSprite;
	[SerializeField] private Sprite neglectedYogaMatSprite;
	[SerializeField] private Sprite neglectedComputerSprite;
	[SerializeField] private Sprite neglectedBookshelfSprite;
	[SerializeField] private Sprite neglectedWindowSprite;
	[SerializeField] private Sprite neglectedWindowLightSprite;
	
	[SerializeField] private Sprite normalPianoSprite;
	[SerializeField] private Sprite normalYogaMatSprite;
	[SerializeField] private Sprite normalComputerSprite;
	[SerializeField] private Sprite normalBookshelfSprite;
	[SerializeField] private Sprite normalWindowSprite;
	[SerializeField] private Sprite normalWindowLightSprite;
	
	[SerializeField] private SpriteRenderer pianoRenderer;
	[SerializeField] private SpriteRenderer yogaMatRenderer;
	[SerializeField] private SpriteRenderer computerRenderer;
	[SerializeField] private SpriteRenderer bookshelfRenderer;
	[SerializeField] private SpriteRenderer windowRenderer;
	[SerializeField] private SpriteRenderer windowLightRenderer;

	[SerializeField] private DialogueGraph objectPromptDialogue;

	private readonly Dictionary<string, bool> objectNeglection = new Dictionary<string, bool>();
	private DialogueSystem dialogueSystem;
	private ValueRegistry valueRegistry;
	private StringRegistry stringRegistry;

	public static InteractionHandler Instance;

	private void Awake()
	{
		Instance = this;
		valueRegistry = MainInstances.Get<ValueRegistry>();
	}

	private void Start()
	{
		stringRegistry = MainInstances.Get<StringRegistry>();
		dialogueSystem = MainInstances.Get<DialogueSystem>();
	}

	// All interactable objects should call this method as the start
	// Returns false if object has already been registered
	public bool RegisterObject(string objectName, Action objectAction, int timeConsumption)
	{
		if(registeredObjects.ContainsKey(objectName))
		{
			Debug.LogWarning("Object " + objectName + " was already registered");
			return false;
		}
		
		Debug.Log("Registering object '" + objectName + "'");
		valueRegistry.Set($"Used {objectName}", 0);
		registeredObjects.Add(objectName, () =>
		{
			objectAction();
			Tracking.Instance.AddUsedTime(timeConsumption);
		});
		objectNeglection.Add(objectName, true);
		return true;
	}

	// Calls an interaction object's action given the name of it
	public bool Interact(string objectName)
	{
		Action action = registeredObjects[objectName];
		if(action != null)
		{
			stringRegistry.Set("Interaction Prompt", objectName);
			dialogueSystem.Present(objectPromptDialogue, () =>
			{
				if(valueRegistry.Get("Confirmed Interaction") == 1)
				{
					action();
					objectNeglection[objectName] = false;
					valueRegistry.Set($"Used {objectName}", 1);
				}
			});
			
			return true;
		}
		Debug.LogWarning($"Could not interact because '{objectName}' is not registered");
		return false;
	}

	// This should be called at the beginning of every day
	public void UpdateNeglectedSprites()
	{
		foreach(string objectName in objectNeglection.Keys)
		{
			// Ignore bonsai tree when checking if objects are neglected
			// since the bonsai tree works uniquely
			if(objectName == "Bonsai Tree")
				continue;
			
			if(objectNeglection[objectName])
			{
				switch(objectName)
				{
					case "Piano":
						pianoRenderer.sprite = neglectedPianoSprite;
						break;
					case "Yoga Mat":
						yogaMatRenderer.sprite = neglectedYogaMatSprite;
						break;
					case "Computer":
						computerRenderer.sprite = neglectedComputerSprite;
						break;
					case "Bookshelf":
						bookshelfRenderer.sprite = neglectedBookshelfSprite;
						break;
					case "Window":
						windowRenderer.sprite = neglectedWindowSprite;
						windowLightRenderer.sprite = neglectedWindowLightSprite;
						break;
				}
			}
			else
			{
				switch(objectName)
				{
					case "Piano":
						pianoRenderer.sprite = normalPianoSprite;
						break;
					case "Yoga Mate":
						yogaMatRenderer.sprite = normalYogaMatSprite;
						break;
					case "Computer":
						computerRenderer.sprite = normalComputerSprite;
						break;
					case "Bookshelf":
						bookshelfRenderer.sprite = normalBookshelfSprite;
						break;
					case "Window":
						windowRenderer.sprite = normalWindowSprite;
						windowLightRenderer.sprite = normalWindowLightSprite;
						break;
				}
			}
		}
	}
}
