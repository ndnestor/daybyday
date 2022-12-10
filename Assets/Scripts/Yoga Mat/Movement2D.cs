using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public GameObject playerSprite;
    public SpriteRenderer playerSpriteRenderer;
    public Rigidbody2D rb;
    public float orderLineY;
    public float moveSpeed = 5f;
    public Animator animator;
    
    [Range(0, 1)]
    [SerializeField] private float ySpeedFactor;
    [SerializeField] private double moveToDistThreshold;

    private static readonly int IsWalkingId = Animator.StringToHash("IsWalking");
    private bool isPlayerControlled = true;
    private Vector2 movement;
    private IEnumerator moveToCoroutine;

    public static readonly int IsSleepingId = Animator.StringToHash("IsSleeping");
    public static Movement2D Instance;

    private void Awake()
    {
        Instance = this;
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
	}
    
    private void OnEnable()
    {
        movement = Vector2.zero;
        if (moveToCoroutine != null)
            StartCoroutine(moveToCoroutine);
    }

    // Update is called once per frame
	private void Update()
    {
        if(isPlayerControlled)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical") * ySpeedFactor;
        }

        // NOTE: Not sure why multiplying by Time.deltaTime does not produce expected results
        rb.MovePosition(rb.position + movement * moveSpeed);

        if(rb.position.y >= orderLineY)
        {
            playerSprite.GetComponent<SpriteRenderer>().sortingOrder = 40;
        }
        else
        {
            playerSprite.GetComponent<SpriteRenderer>().sortingOrder = 60;
        }
        
        // NOTE from Bridge: Following if statements control direction character faces
        // Also added line to get SpriteRenderer component in Awake
        // Should affect only sprite renderer
        if(movement.x < 0 && !playerSpriteRenderer.flipX)
            playerSpriteRenderer.flipX = true;
        else if(movement.x > 0 && playerSpriteRenderer.flipX)
            playerSpriteRenderer.flipX = false;

        animator.SetBool(IsWalkingId, movement.x != 0);
    }

    // Prevent or allow player to control the character
    // TODO: Consider turning isPlayerControlled into a property to replace this method for elegance
    public void SetPlayerControl(bool canControl) {
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

    // Used to initialize and run MoveToCoroutine
    public void MoveTo(Vector2 destination, Action callback = null) { 
        moveToCoroutine = MoveToCoroutine(destination, callback);
        StartCoroutine(moveToCoroutine);
    }

    public void MoveTo(Vector2 destination, Func<IEnumerator> callback) {
        MoveTo(destination, () => StartCoroutine(callback()));
    }

    // Send the character to the specified destination forcefully
    // WARN from Nathan: This doesn't really work with the current implementation of the movement variable
    // TODO: Fix
    private IEnumerator MoveToCoroutine(Vector2 destination, Action callback)
    {   
        SetPlayerControl(false);
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
                else if(transform.position.x > destination.x)
                {
                    movement = Vector2.left;
                }
                if(Mathf.Abs(transform.position.x - destination.x) < moveToDistThreshold)
                {
                    completedX = true;
                    movement = Vector2.zero;
			    }
			}

            if(!completedY)
            {
                if(transform.position.y < destination.y)
                {
                    movement.y = ySpeedFactor;
                }
                else if(transform.position.y > destination.y)
                {
                    movement.y = -ySpeedFactor;
                }
                if(Mathf.Abs(transform.position.y - destination.y) < moveToDistThreshold)
                {
                    completedY = true;
                }
            }
            yield return null;
		}
        SetPlayerControl(true);
        callback?.Invoke();
    }
}