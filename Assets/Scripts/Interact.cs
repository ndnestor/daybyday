using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float interactRange = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }
    }

    private void CheckInteraction()
    {
        //Debug.Log("Looking for Interactable");

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.up, interactRange);
        if(hit.collider != null)
        {
            if(hit.collider.GetComponent<IInteractable>() != null)
            {
                //Debug.Log("Interacting with " + hit.collider.name);
                hit.collider.GetComponent<IInteractable>().OnInteract();
                this.enabled = false;
                return;
            }
        }

        hit = Physics2D.Raycast(transform.position, Vector2.left, interactRange);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                //Debug.Log("Interacting with " + hit.collider.name);
                hit.collider.GetComponent<IInteractable>().OnInteract();
                this.enabled = false;
                return;
            }
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right, interactRange);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                //Debug.Log("Interacting with " + hit.collider.name);
                hit.collider.GetComponent<IInteractable>().OnInteract();
                this.enabled = false;
                return;
            }
        }

        hit = Physics2D.Raycast(transform.position, Vector2.down, interactRange);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                //Debug.Log("Interacting with " + hit.collider.name);
                hit.collider.GetComponent<IInteractable>().OnInteract();
                this.enabled = false;
                return;
            }
        }
    }
}
