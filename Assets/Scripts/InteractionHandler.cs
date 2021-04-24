using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{

	private Hashtable registeredObjects = new Hashtable();

    private static InteractionHandler Instance;

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
			Debug.LogWarning("Object " + objectName + " was already registerd");
			return false;
		}
		else
		{
			registeredObjects.Add(objectName, objectAction);
			return true;
		}
	}

	// Calls an interaction object's action give the name of it
	public bool Interact(string objectName)
	{
		System.Action action = (System.Action)registeredObjects[objectName];
		if(action != null)
		{
			action();
			return true;
		}
		Debug.LogWarning("Could not interact because the given object name is not registered");
		return false;
	}
}
