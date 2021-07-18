using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        rb.transform.position = new Vector3(-2.4f, 0.08f, 0.0f);
    }

    void FixedUpdate(){
         rb.velocity = transform.up * speed - transform.right*speed;
         Vector3 pos = transform.position;
         if (pos.x < -7.3 || pos.x > 2.2) {
             Debug.Log("Exceed x");
             speed = 0;
         }
         if (pos.y < -3.4 || pos.y > 3.6) {
             Debug.Log("Exceed y");
             speed = 0;
         }

         
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        /**if (col.gameObject.tag == "Wall")
        {
            Debug.Log("Hit border");
            //Vector2 Bounce = new Vector2 (Random.Range(-1, 1), Random.Range(-1, 1));
            //rb.AddForce (Bounce);
        }*/
    }
}
