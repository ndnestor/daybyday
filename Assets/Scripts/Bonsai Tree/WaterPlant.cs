using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlant : MonoBehaviour
{
    [HideInInspector] public int level = 1;
    [HideInInspector] public int day = 1;

    public GameObject waterCan;
    public GameObject wateringObj;
    private bool wasWatered;

    // Start is called before the first frame update
    private void Start()
    {
        InteractionHandler.Instance.RegisterObject("Bonsai Tree", WaterBonsai, 1);
    }

    private IEnumerator WaterBonsai()
    {
        var waterCanInstance = Instantiate(waterCan, new Vector3(0.93f, -1.58f, 0.0f), Quaternion.identity);
        var wateringObjInstance = Instantiate(wateringObj, new Vector3(-0.05f, -1.50f, 0.0f), Quaternion.identity);
        wasWatered = true;
        
        Movement2D.Instance.SetPlayerControl(false);
        yield return new WaitUntil(() => !waterCanInstance && !wateringObjInstance);
        Movement2D.Instance.SetPlayerControl(true);
    }

    // This should be called at beginning of every day
    // Either run through InteractionHandler or by same method
    public void DayUpdate()
    {
        if(wasWatered)
        {
            level += 1;
            if (level > 3) {
                level = 3;
            }
        } else
        {
            level -= 1;
            if(level < 1)
            {
                level = 1;
            }
        }
        day += 1;
        wasWatered = false;
    }
}
