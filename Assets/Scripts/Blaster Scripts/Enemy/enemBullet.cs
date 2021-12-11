using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class enemBullet : MonoBehaviour
{
    float bulletSpeed = 10.0f;
    float bulletHorz = 6.0f; //2.5f;
    public Rigidbody2D rb;
    public float despawnTime;
    private float despawnTimer;
    //globalScore scorekeeper;
    enemyScript_Ship ship;

    void Start() {
        ship = GameObject.Find("enemy_Ship(Clone)").GetComponent<enemyScript_Ship>();
        switch (ship.returnDirec()) {
            case "Left":
                rb.velocity = -transform.up * bulletSpeed - transform.right * bulletHorz;
                break;
            case "Right":
                rb.velocity = -transform.up * bulletSpeed + transform.right * bulletHorz;
                break;
        }
        despawnTimer = Time.time + despawnTime;
    }

    void Update() {
        if (despawnTimer < Time.time) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "PlatformHealth")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
