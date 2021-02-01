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

    private Material defaultMat;
    private bool triggerable = false;

    private void Start()
    {
        defaultMat = connectedSprite.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && triggerable)
        {
            UseComputer();
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
