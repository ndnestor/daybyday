using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInRoom : MonoBehaviour
{
    public GameObject mainRoom;
    public GameObject computerScreen;
    public GameObject connectedSprite;
    public Material highlight;
    public int timeValue;

    private Material defaultMat;
    private bool triggerable = false;

    Tracking tracker = Tracking.Instance;

    private void Start()
    {
        defaultMat = connectedSprite.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && triggerable)
        {
            tracker.AddObject(gameObject);
            if (tracker.TimeUsed + timeValue <= Tracking.MAX_TIME)
            {
                Debug.Log("Time units used: " + tracker.AddUsedTime(timeValue));
                UseComputer();
            }
            else
            {
                Debug.Log("Not enough time to use object! Time remaining: " + (24 - tracker.TimeUsed));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        triggerable = true;
        connectedSprite.GetComponent<SpriteRenderer>().material = highlight;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        triggerable = false;
        connectedSprite.GetComponent<SpriteRenderer>().material = defaultMat;
    }

    private void UseComputer()
    {
        triggerable = false;
        connectedSprite.GetComponent<SpriteRenderer>().material = defaultMat;

        mainRoom.SetActive(false);
        computerScreen.SetActive(true);
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.Confined;
    }


}
