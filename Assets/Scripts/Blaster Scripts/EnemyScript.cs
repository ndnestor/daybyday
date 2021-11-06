using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float enemySpeed = 20f;
    public Rigidbody2D rigidEnemy;
    public int rocketHealth;

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy.velocity = -transform.up * enemySpeed;
        //gameObject.tag = "Enemy";
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlatformHealth")
        {
            Destroy(gameObject);
        }
        //if (col.gameObject.tag == "Enemy") {
        //    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>(), true);
        //}
        if (col.gameObject.tag == "Whale")
        {
            Destroy(gameObject);
            Debug.Log("Hit whale");
        }
        if (col.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
            Debug.Log("Hit ship");
        }

        // Destroys pickups and continues vel on collision
        if (col.gameObject.tag == ("Star"))
        {
            Destroy(col.gameObject);
            rigidEnemy.velocity = -transform.up * enemySpeed;
        }
        if (col.gameObject.tag == ("Rapid"))
        {
            Destroy(col.gameObject);
            rigidEnemy.velocity = -transform.up * enemySpeed;
        }
        if (col.gameObject.tag == ("Spread"))
        {
            Destroy(col.gameObject);
            rigidEnemy.velocity = -transform.up * enemySpeed;
        }
        if (col.gameObject.tag == ("Bulwark"))
        {
            Destroy(col.gameObject);
            rigidEnemy.velocity = -transform.up * enemySpeed;
        }
        if (col.gameObject.tag == ("Swift"))
        {
            Destroy(col.gameObject);
            rigidEnemy.velocity = -transform.up * enemySpeed;
        }
    }

}
