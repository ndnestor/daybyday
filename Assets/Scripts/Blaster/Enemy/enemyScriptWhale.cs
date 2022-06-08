using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScriptWhale : MonoBehaviour
{

    public float enemySpeed = 20f;
    public Rigidbody2D rigidEnemy;
    GlobalScore scorekeeper;
    private float despawnTime, despawnTimer;
    int passes;
    SpriteRenderer whaleSprite;
    Vector3 whalePosition;

    // Start is called before the first frame update
    void Start()
    {
        passes = 2; //1 for odd # of passes, 2 for 0 or even # of passes
        despawnTime = 5.0f;
        despawnTimer = Time.time + despawnTime;
        scorekeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
        rigidEnemy.velocity = transform.right * enemySpeed;
        whalePosition = transform.position;
        whaleSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update() {
        whalePosition = transform.position;
        // Direction controller
         if (whalePosition.x >= 11.0f) {
             passes = 1;
             if (scorekeeper.blasterLevel >= 2) {
                 rigidEnemy.velocity = -transform.right * enemySpeed;
                 whaleSprite.flipX = true;
             }
         } else if (whalePosition.x <= -11.0f) {
             passes = 2;
             if (scorekeeper.blasterLevel >= 2) {
                 rigidEnemy.velocity = transform.right * enemySpeed;
                 whaleSprite.flipX = false;
             }
         }

        // Despawns whale based on level conditions
        if (despawnTimer < Time.time) {
            if (scorekeeper.blasterLevel == 1) {
                Destroy(gameObject);
            }
            if (scorekeeper.blasterLevel == 2 && passes == 2) {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Whale") {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            if (passes == 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            }
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


        if (col.gameObject.tag == ("Star"))
        {
            Destroy(col.gameObject);
            if (passes == 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Rapid"))
        {
            Destroy(col.gameObject);
            if (passes == 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Spread"))
        {
            Destroy(col.gameObject);
            if (passes == 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Bulwark"))
        {
            Destroy(col.gameObject);
            if (passes == 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Swift"))
        {
            Destroy(col.gameObject);
            if (passes == 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            }
        }
    }

}
