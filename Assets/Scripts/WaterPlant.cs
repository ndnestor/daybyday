using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlant : MonoBehaviour
{
    public GameObject waterCan;
    public GameObject treeObject;
    public GameObject wateringObj;
    public bool interacting;
    public int level;
    public InteractionHandler interactionHandler;
    // Start is called before the first frame update
    void Start()
    {
        interactionHandler.RegisterObject("Bonsai Object", waterBonsai);
        //HighlightObject highlightObject = treeObject.GetComponent<HighlightObject>();
        //interacting = highlightObject.triggerable;
        level += 1;
    }

    void waterBonsai() {
        GameObject showCan = Instantiate(waterCan, new Vector3(0.93f, -1.58f, 0.0f), Quaternion.identity);
        GameObject showWater = Instantiate(wateringObj, new Vector3(-0.05f, -1.50f, 0.0f), Quaternion.identity);
    }

    int returnLevel() {
        return level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
