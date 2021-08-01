using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{

	private readonly Hashtable registeredObjects = new Hashtable();
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

	private Hashtable objectNeglection = new Hashtable(); // Key: string | Value: bool

	[HideInInspector] public static InteractionHandler Instance;

	private void Awake()
	{
		Instance = this;
	}

	// All interactable objects should call this method as the start
	// Returns false if object has already been registered
	public bool RegisterObject(string objectName, System.Action objectAction)
	{
		if(registeredObjects.ContainsKey(objectName))
		{
			Debug.LogWarning("Object " + objectName + " was already registered");
			return false;
		}
		Debug.Log("Registering object '" + objectName + "'");
		registeredObjects.Add(objectName, objectAction);
		objectNeglection.Add(objectName, true);
		return true;
	}

	// Calls an interaction object's action given the name of it
	public bool Interact(string objectName)
	{
		System.Action action = (System.Action)registeredObjects[objectName];
		if(action != null)
		{
			action();
			objectNeglection[objectName] = false;
			return true;
		}
		Debug.LogWarning("Could not interact because the given object name is not registered");
		return false;
	}

	// This should be called at the beginning of every day
	public void UpdateNeglectedSprites()
	{
		foreach(string objectName in objectNeglection.Keys)
		{
			if((bool)objectNeglection[objectName])
			{
				// Ignore bonsai tree when checking if objects are neglected
				// since the bonsai tree works uniquely
				if(objectName == "Bonsai Tree")
				{
					continue;
				}
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
					default:
						Debug.LogError($"Invalid object name provided: {objectName}");
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
					default:
						Debug.LogError($"Invalid object name provided: {objectName}");
						break;
				}
			}
		}
	}
}
