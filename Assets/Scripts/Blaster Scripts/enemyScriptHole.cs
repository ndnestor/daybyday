using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScriptHole : MonoBehaviour
{

    public Rigidbody2D rigidEnemy;
    bool holeOnLeft, holeOnRight;
    public PlayerMovement rocketMovement;

    private Spawnscript spawner;

    public void blackHoleActive(bool left, bool right) {
        holeOnLeft = left;
        holeOnRight = right;
        //Debug.Log("Black hole left " + left + " black hole right " + right);
    }

    public void Update() {
        // blackHolePresent method from PlayerMovement class
        // takes bool for whether a hole is present (T/F)
        // takes int for side (1 = left, 2 = right)
        // If none present, (false, 0)
        if (holeOnLeft) {
            rocketMovement.blackHolePresent(true, 1);
        } else if (holeOnRight) {
            rocketMovement.blackHolePresent(true, 2);
        } else if (!holeOnRight && !holeOnLeft) {
            rocketMovement.blackHolePresent(false, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy") {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>(), true);
        }
        if (col.gameObject.tag == "PlatformHealth")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
