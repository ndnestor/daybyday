using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    [SerializeField] private float moveForce;
     
     void Update() {
         var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
         GetComponent<Rigidbody2D>().velocity = direction * moveForce * Time.deltaTime;
     }
}
