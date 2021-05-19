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
		else
		{
			registeredObjects.Add(objectName, objectAction);
			objectNeglection.Add(objectAction, true);
			return true;
		}
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
				switch(objectName) {
					case "Piano":
						// TODO: Finish this
				}
			}
		}
	}
}
