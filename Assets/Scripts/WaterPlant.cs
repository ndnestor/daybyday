using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlant : MonoBehaviour
{
    public GameObject waterCan;
    public GameObject treeObject;
    public GameObject wateringObj;
    public int level;
    public int day;
    public InteractionHandler interactionHandler;
    public HighlightObject highlightObject;
    private bool wasWatered;
    // Start is called before the first frame update
    void Start()
    {
        interactionHandler.RegisterObject("Bonsai Object", waterBonsai);
    }

    void waterBonsai() {
        Debug.Log("Bonsai interaction: watering");
        GameObject showCan = Instantiate(waterCan, new Vector3(0.93f, -1.58f, 0.0f), Quaternion.identity);
        GameObject showWater = Instantiate(wateringObj, new Vector3(-0.05f, -1.50f, 0.0f), Quaternion.identity);
        wasWatered = true;
    }

    int returnLevel() {
        return level;
    }

    // This should be called at beginning of every day
    // Either run through InteractionHandler or by same method
    public void dayUpdate() {
        if (wasWatered == true) {
            level += 1;
            if (level > 3) {
                level = 3;
            }
        } else {
            level -= 1;
            if (level < 1) {
                level = 1;
            }
        }
        day += 1;
        wasWatered = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Uses same conditions as highlight to call Interact(), runs waterBonsai()
        if (Input.GetKey("e") && highlightObject.triggerable) {
            interactionHandler.Interact("Bonsai Object");
        }
    }
}
