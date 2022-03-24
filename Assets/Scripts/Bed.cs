using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{

    private void Start()
    {
        InteractionHandler.Instance.RegisterObject("Bed", () => { /* Intentionally empty */}, Tracking.MAX_TIME);
    }

}
