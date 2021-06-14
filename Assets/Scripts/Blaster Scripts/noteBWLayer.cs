using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteBWLayer : MonoBehaviour
{
    public SpriteRenderer rendBW;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Set sorting order of BW to 1");
        rendBW.sortingOrder = 1;
    }
}
