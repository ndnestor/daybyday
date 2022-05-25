using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSlide : MonoBehaviour
{

    public float slideSpeed;
    public Rigidbody2D rigidHello;
    public float slideTimer;

    // Start is called before the first frame update

    // Slide out with Hello
    void Update()
    {
        if (slideTimer < Time.time) {
            rigidHello.velocity = -transform.right * slideSpeed;
        }
        if (rigidHello.position.x <= 0)
        {
            slideSpeed = 0;
        }
    }

}