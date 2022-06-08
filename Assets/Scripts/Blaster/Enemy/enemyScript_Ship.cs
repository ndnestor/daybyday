using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyScript_Ship : MonoBehaviour
{

    public float enemySpeed = 20f;
    public Rigidbody2D rigidEnemy;
    public GameObject enemBullet;
    private float shootTimer;
    public float shootIncr;
    private float shipShootPosition;
    private float despawnTime, despawnTimer;
    GlobalScore scorekeeper;
    private int passes;
    SpriteRenderer shipSprite;
    Vector3 shipPosition;

    void Start()
    {
        passes = 2; // 2 if 0 or even # of passes, 1 if odd # of passes
        despawnTime = 5.0f; //For single pass, despawns at this val; for double, check conditions after
        scorekeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
        rigidEnemy.velocity = -transform.right * enemySpeed;
        shipPosition = transform.position;
        despawnTimer = Time.time + despawnTime;
        shipSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public string returnDirec() {
        if (scorekeeper.blasterLevel >= 2) {
            if (passes == 2) {
                return "Left"; // Moving from right TO left
            } else {
                return "Right";
            }
        } else {
            return "Left";
        }
    }

    void Update()
    {
        shipPosition = transform.position;
        if (shootTimer < Time.time) {
             GameObject tmpBullet = Instantiate(enemBullet, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpBullet.transform.position = new Vector3(shipPosition.x-0.32f, shipPosition.y-0.93f, 0.0f);

             GameObject tmpBullet2 = Instantiate(enemBullet, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpBullet2.transform.position = new Vector3(shipPosition.x+0.1f, shipPosition.y-0.93f, 0.0f);

             GameObject tmpBullet3 = Instantiate(enemBullet, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpBullet3.transform.position = new Vector3(shipPosition.x+0.52f, shipPosition.y-0.93f, 0.0f);

             shootTimer = Time.time + shootIncr;
         }
         //Ship moves from right to left, so hits certain x coordinate to count pass
         if (shipPosition.x <= -18.0f) {
             passes = 1;
             if (scorekeeper.blasterLevel >= 2) {
                rigidEnemy.velocity = transform.right * enemySpeed;
                shipSprite.flipX = true;
             }
         } else if (shipPosition.x >= 18.0f) {
             passes = 2;
             if (scorekeeper.blasterLevel >= 2) {
                 rigidEnemy.velocity = -transform.right * enemySpeed;
                 shipSprite.flipX = false;
             }
         }
         if (despawnTimer < Time.time) {
            if(scorekeeper.blasterLevel == 1) {
                Destroy(gameObject);
            }
            if(scorekeeper.blasterLevel == 2 && passes == 2) {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Debug.Log("Hit enemy");
            // Continue movement
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == "Whale")
        {
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
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
        if (col.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == ("Star"))
        {
            Destroy(col.gameObject);
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Rapid"))
        {
            Destroy(col.gameObject);
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Spread"))
        {
            Destroy(col.gameObject);
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Bulwark"))
        {
            Destroy(col.gameObject);
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
            }
        }
        if (col.gameObject.tag == ("Swift"))
        {
            Destroy(col.gameObject);
            if (passes == 0 || passes == 2) {
                rigidEnemy.velocity = -transform.right * enemySpeed;
            } else {
                rigidEnemy.velocity = transform.right * enemySpeed;
            }
        }
    }

}
