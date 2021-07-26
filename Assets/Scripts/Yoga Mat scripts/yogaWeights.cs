using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yogaWeights : MonoBehaviour
{
    public Rigidbody2D rb;
    float speed, transY;
    float moveTimer, moveIncr;
    void Start() {
        speed = 1;
        transY = UnityEngine.Random.Range(0, 10);
         if (transY < 5) {
             rb.velocity = -transform.up * speed;
         } else {
             rb.velocity = transform.up * speed;
         }
        moveIncr = 1.5f;
        moveTimer = Time.time + moveIncr;
    }
    void FixedUpdate() {
        if(moveTimer < Time.time) {
            switchDirec();
            moveTimer = Time.time + moveIncr;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall") {
            switchDirec();
        }
    }
    void switchDirec() {
        transY = UnityEngine.Random.Range(0, 10);
         if (transY < 5) {
             rb.velocity = -transform.up * speed;
         } else {
             rb.velocity = transform.up * speed;
         }
         moveTimer = Time.time + moveIncr;
    }
}
