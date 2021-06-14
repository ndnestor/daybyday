using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketBounds : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Ship")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Whale")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "enemBullet")
        {
            Destroy(col.gameObject);
        }
    }
}
