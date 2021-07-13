using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_healScript : MonoBehaviour
{

    public float dropSpeed = 3f;
    public Rigidbody2D rigidDrop;

    // Start is called before the first frame update
    void Start()
    {
        rigidDrop.velocity = -transform.up * dropSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlatformHealth")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }

}

