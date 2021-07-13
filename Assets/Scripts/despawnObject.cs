using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    private float despawnTimer;
    public float despawnTime;
    void Start()
    {
        despawnTimer = Time.time + despawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (despawnTimer < Time.time) {
            Destroy(gameObject);
        }
    }
}
