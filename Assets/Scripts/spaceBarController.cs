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
        jumpSpeed = 1.2f;
        jumpIncr = 0.5f;
        playerBar.velocity = -transform.up * fallSpeed;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            barJump();
            print("Space bar pressed");
        }
        if (jumpTimer > Time.time) {
            playerBar.velocity = transform.up * jumpSpeed;
        } else {
            playerBar.velocity = -transform.up * fallSpeed;
        }
    }
    void barJump() {
        jumpTimer = Time.time + jumpIncr;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform") {
            Physics2D.IgnoreCollision(inRangeBox.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
