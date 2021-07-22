using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class randMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    private float transX, transY;
    private float bounceTimer, bounceIncr;
    private bool playerInBounds;
    public GameObject circleCenter, playerDot;
    public float distance;
    public TextMeshProUGUI inOutText;
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
         distance = Vector3.Distance (circleCenter.transform.position, playerDot.transform.position);
         if (Time.time > bounceTimer) {
             changeDirection();
         }
          if (distance <= 1.22) {
             playerInBounds = true;
             Debug.Log("Player in bounds is TRUE");
         } else {
             playerInBounds = false;
             Debug.Log("Player in bounds is FALSE");
         }
         notifChange();
    }

    public void notifChange() {
        if (playerInBounds == true) {
            inOutText.text = "IN";
            inOutText.color = new Color (0.149f,0.325f,0.2235f, 1);
        } else {
            inOutText.text = "OUT";
            inOutText.color = new Color (0.616f,0.004f,0.004f, 1);
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
            /** if (playerInBounds == true) {
                playerInBounds = false;
            } else {
                playerInBounds = true;
            } */
            Physics2D.IgnoreCollision(playerDot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

}
