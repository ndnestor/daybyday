using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceBarController : MonoBehaviour
{
    float fallSpeed, jumpSpeed;
    public Rigidbody2D playerBar;
    public GameObject inRangeBox;
    float jumpTimer, jumpIncr;

    void Start() {
        fallSpeed = 1.0f;
        jumpSpeed = 1.5f;
        jumpIncr = 0.5f;
        playerBar.velocity = -transform.up * fallSpeed;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {
            barJump(jumpSpeed);
            print("Space bar pressed");
        } else {
            playerBar.velocity = -transform.up * fallSpeed;
        }
        /** if (jumpTimer > Time.time) {
            playerBar.velocity = transform.up * jumpSpeed;
        } else {
            playerBar.velocity = -transform.up * fallSpeed;
        } */
        if(Input.GetKey(KeyCode.W)) {
            barJump(jumpSpeed*2);
        }
        if(Input.GetKey(KeyCode.S)) {
            barJump(jumpSpeed*(-2));
        }
    }
    void barJump(float speed) {
        //jumpTimer = Time.time + jumpIncr;
        playerBar.velocity = transform.up * speed;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform") {
            Physics2D.IgnoreCollision(inRangeBox.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
