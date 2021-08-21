using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public GameObject connectedSprite;
    public Material highlight;

    private Material defaultMat;
    public bool triggerable = false;

    private void Start()
    {
        defaultMat = connectedSprite.GetComponent<SpriteRenderer>().material;
    }

    public void setTriggerFalse() {
        triggerable = false;
    }

    private void Update()
    {
        if (Input.GetKey("e") && triggerable)
        {
            connectedSprite.GetComponent<SpriteRenderer>().material = defaultMat;
            Debug.Log("Interacted with " + gameObject.name);
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
}
