using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float pMoveSpeed;
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = pMoveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.position = new Vector3(rb.position.x, rb.position.y + moveSpeed, rb.position.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.position = new Vector3(rb.position.x, rb.position.y - moveSpeed, rb.position.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.position = new Vector3(rb.position.x + moveSpeed, rb.position.y, rb.position.z);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.position = new Vector3(rb.position.x - moveSpeed, rb.position.y, rb.position.z);
        }
    }
}
