using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloSlide : MonoBehaviour
{

    public float slideSpeed = 20f;
    public Rigidbody2D rigidHello;
    public float slideTimer;

    // Start is called before the first frame update

    void Update()
    {
        if (slideTimer < Time.time) {
            rigidHello.velocity = -transform.right * slideSpeed;
        }
    }

}