using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScriptHole : MonoBehaviour
{

    public Rigidbody2D rigidEnemy;
    private float despawnTimer;
    public float despawnLength;
    public bool yesHoleLeft = false;
    public bool yesHoleRight = false;

    private Spawnscript spawner;

    // Start is called before the first frame update
    public void Start()
    {
        despawnTimer = Time.time + despawnLength;
        //Debug.Log(spawner.xHole);
    }

    void Update() {
        if (Time.time > despawnTimer) {
            Destroy(gameObject);
            yesHoleLeft = false;
            yesHoleRight = false;
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
