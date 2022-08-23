using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Dialogue;
using UnityEngine;

public class Window : MonoBehaviour {

	[SerializeField] private DialogueGraph[] dialogueGraphs;
	[SerializeField] private float timeConsumption;

	private DialogueSystem dialogueSystem;

	private const int springStart = 1;
	private const int springEnd = 3;
	private const int summerStart = 4;
	private const int summerEnd = 6;
	private const int fallStart = 7;
	private const int fallEnd = 9;
	private const int winterStart = 10;
	private const int winterEnd = 12; // TODO: Ask if this is the end date
	private const int nightHour = 10; // Inclusive

	private void Start()
	{
		InteractionHandler.Instance.RegisterObject("Window", Interact, 1);
		dialogueSystem = MainInstances.Get<DialogueSystem>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			Interact();
		}
	}

	private void Interact()
	{
		int messageIndex = 0;
		int currDay = Tracking.Instance.DayNum;

		if(Between(currDay, springStart, springEnd))
		{
			messageIndex = 0;
		} else if(Between(currDay, summerStart, summerEnd))
		{
			messageIndex = 2;
		} else if(Between(currDay, fallStart, fallEnd))
		{
			messageIndex = 4;
		} else if(Between(currDay, winterStart, winterEnd))
		{
			messageIndex = 6;
		} else
		{
			throw new NotImplementedException("The current day is outside the bounds of the window's supported days");
		}

		if(Tracking.Instance.TimeUsed >= nightHour)
		{
			messageIndex++;
		}

		dialogueSystem.Present(dialogueGraphs[messageIndex]);
		Tracking.Instance.AddUsedTime(timeConsumption);
		// TODO: Add CAS as well
	}

	private static bool Between(int value, int lowerBound, int upperBound)
	{
		return lowerBound <= value && value <= upperBound;
	}
}
