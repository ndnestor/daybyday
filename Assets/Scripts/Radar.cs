using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public bool entered = false;
   void OnTriggerEnter (Collider other) {
    if (other.CompareTag ("Player")) {         
        entered=true;
    }
}


    void OnTriggerExit (Collider other) {
    if (other.CompareTag ("Player")) {       
        entered = false;
    }
}
}
