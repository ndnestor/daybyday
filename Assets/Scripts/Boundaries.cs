using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    // Start is called before the first frame update
    void Start()
    {
        // get coordinates of main camera
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //objectWidth = transform.GetComponent<BoxCollider>().size.x / 2; //extents = size of width / 2
        //objectHeight = transform.GetComponent<BoxCollider>().size.y / 2;
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        // get position of either the min or max of the boundary of the screen
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPosition;
    }
}
