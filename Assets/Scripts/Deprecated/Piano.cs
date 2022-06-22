using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{

	private void Start()
	{
		InteractionHandler.Instance.RegisterObject("Piano", () => { /* Intentionally empty */ });
	}

}
