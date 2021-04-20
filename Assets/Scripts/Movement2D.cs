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

    [SerializeField] private double moveToDistThreshold;
    [SerializeField] private Transform bedDestination;

    private bool isPlayerControlled = true;
    private Vector2 movement;


    // Update is called once per frame
    private void Update()
    {
        if(isPlayerControlled)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical") * .52f;
        }

        if(Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //NOTE: This part is used for testing. Should be removed at some point before release
        if(Input.GetKeyDown(KeyCode.M)) {
            print("Moving");
            SetPlayerControl(false);
            IEnumerator moveToCoroutine = MoveTo(bedDestination.position);
            StartCoroutine(moveToCoroutine);
        }
    }

    private void FixedUpdate()
    {
        // NOTE from Nathan: Fixed update should stricly only handle physics tasks. You can get odd behavior otherwise.
        // In this case, it probably doesn't really matter since nothing too crazy is going on in this method
        // but it is worth noting. See here: https://forum.unity.com/threads/the-truth-about-fixedupdate.231637/
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

    // Prevent or allow player to control the character
    // TODO: Consider turning isPlayerControlled into a property to replace this method for elegance
    private void SetPlayerControl(bool canControl) {
        if(canControl)
        {
            movement = Vector2.zero;
            isPlayerControlled = true;
        }
        else
        {
            movement = Vector2.zero;
            isPlayerControlled = false;
        }
    }

    // Send the character to the specified destination forcefully
    // WARN from Nathan: This doesn't really work with the current implementation of the movement variable
    // TODO: Fix
    private IEnumerator MoveTo(Vector2 destination)
    {
        bool completedX = false;
        bool completedY = false;
        while(!(completedY && completedX))
        {
            if(!completedX)
            {
                if(transform.position.x < destination.x)
                {
                    // TODO: Instead of using Vector2.right, interpolate over time to get smooth motion
                    movement = Vector2.right;
                }
                else
                {
                    movement = Vector2.left;
                }
                if(Mathf.Abs(transform.position.x - destination.x) < moveToDistThreshold)
                {
                    completedX = true;
			    }
			}

            if(!completedY)
            {
                if(transform.position.y < destination.y)
                {
                    movement.y = 1;
                }
                else
                {
                    movement.y = -1;
                }
                if(Mathf.Abs(transform.position.y - destination.y) < moveToDistThreshold)
                {
                    completedY = true;
                }
            }
            yield return null;
		}
        SetPlayerControl(true);
	}
}