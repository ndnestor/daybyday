using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScriptWhale : MonoBehaviour
{

    public float enemySpeed = 20f;
    public Rigidbody2D rigidEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy.velocity = transform.right * enemySpeed;
        //gameObject.tag = "Enemy";
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Enemy") {
        //    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>(), true);
        //}
        if (col.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            rigidEnemy.velocity = transform.right * enemySpeed;
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
    }

}
