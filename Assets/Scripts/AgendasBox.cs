using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgendasBox : MonoBehaviour {
    [SerializeField] private GameObject door;

    public void OnAnimationStart()
    {
        door.SetActive(false);
    }
    
    public void OnAnimationFinish()
    {
        door.SetActive(true);
        gameObject.SetActive(false);
    }
}
