using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTrigger : MonoBehaviour
{
    public GameObject connectedSprite;
    public Material highlight;

    private Material defaultMat;

    void Start()
    {
        defaultMat = connectedSprite.GetComponent<SpriteRenderer>().material;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            connectedSprite.GetComponent<SpriteRenderer>().material = highlight;
        }
        Debug.Log("Collided with " + col.gameObject.name);
    }

    void OnTriggerExit(Collider col)
    {
        connectedSprite.GetComponent<SpriteRenderer>().material = defaultMat;
    }
}
