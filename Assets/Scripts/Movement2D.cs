using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Movement2D : MonoBehaviour
{
    public GameObject playerSprite;
    public Rigidbody2D rb;
    public float orderLineY;
    public float moveSpeed = 5f;

    private Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical") * .52f;

        if(Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if(rb.position.y >= orderLineY)
        {
            playerSprite.GetComponent<SpriteRenderer>().sortingOrder = 40;
        }
        else
        {
            playerSprite.GetComponent<SpriteRenderer>().sortingOrder = 60;
        }
    }
}