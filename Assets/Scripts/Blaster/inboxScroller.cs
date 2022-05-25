using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class inboxScroller : MonoBehaviour
{
    public Rigidbody2D scrollInbox;
    float downSpeed;
    
    void Start()
    {
        downSpeed = 20.0f;
    }
    
    void Update()
    {
        // AYO TRY USING THE SPACE BAR IF YOU CAN LATER IT'S Input.GetKey(KeyCode.Space)
        if(Input.GetButtonDown("Scroll"))
        {
            Debug.Log("Down");
            scrollInbox.velocity = -transform.up * downSpeed;
        }
    }
}
