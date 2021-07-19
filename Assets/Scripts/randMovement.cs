using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    private float transX, transY;
    private float bounceTimer, bounceIncr;
    private bool playerInBounds;
    void Start()
    {
        bounceIncr = 1.5f;
        playerInBounds = true;
        rb = GetComponent<Rigidbody2D> ();
        rb.transform.position = new Vector3(-2.4f, 0.08f, 0.0f);
        transX = UnityEngine.Random.Range(0, 10);
        transY = UnityEngine.Random.Range(0, 10);
         if (transX < 5) {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed - transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed - transform.right*speed;
             }
         } else {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed + transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed + transform.right*speed;
             }
         }
         bounceTimer = Time.time + bounceIncr;
    }

    void FixedUpdate(){
         //rb.velocity = transform.up * speed - transform.right*speed;
         Vector3 pos = transform.position;
         if (Time.time > bounceTimer) {
             changeDirection();
         }
    }

    
    void changeDirection() {
        Debug.Log("Changing direc");
        transX = UnityEngine.Random.Range(0, 10);
        transY = UnityEngine.Random.Range(0, 10);
         if (transX < 5) {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed - transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed - transform.right*speed;
             }
         } else {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed + transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed + transform.right*speed;
             }
         }
         bounceTimer = Time.time + bounceIncr;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            if (playerInBounds == true) {
                playerInBounds = false;
            } else {
                playerInBounds = true;
            }
        }
        Debug.Log("Player in bounds is: " + playerInBounds);
    }
}
