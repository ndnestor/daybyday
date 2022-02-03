using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulwarkDewpawn : MonoBehaviour
{
    public float despawnTime;
    public float despawnIncr;
    void Start()
    {
        despawnTime = Time.time + despawnIncr;
    }
    
    void Update()
    {
        if (Time.time >= despawnTime) {
            Destroy(gameObject);
            Debug.Log("despawn bulwark");
        }
    }
}
