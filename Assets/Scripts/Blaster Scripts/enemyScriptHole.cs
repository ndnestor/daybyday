using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScriptHole : MonoBehaviour
{

    public Rigidbody2D rigidEnemy;
    bool holeOnLeft, holeOnRight;
    PlayerMovement rocketMovement;

    private Spawnscript spawner;

    public void blackHoleActive(bool left, bool right) {
        holeOnLeft = left;
        holeOnRight = right;
    }

    void Update() {
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
