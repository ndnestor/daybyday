using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Drag the Character Controller 2D script into here;
    // Now any reference to "controller" will ref that script
    /** public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    // Update is called once per frame
    
    void Update()
    {
        // Gets user input for direction on horizontal axis
        // A or left arrow = -1, D or right arrow = 1
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }

    void FixedUpdate()
    {
        // Move our character
        // Ultimately, take input from Update and apply here

        // Move takes 3 inputs: horizontal, crouch, jump
        // Since bokki only uses horizontal, false to others
        // Time.fixedDeltaTime = time elapsed since function last called --> moves same amt every time called
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
    **/

    float speed, blackHoleDrag;
    int blackHoleDirection;
    private Rigidbody2D rb2d;
    [SerializeField] private int rocketHealth;
    private int maxHealth, enemyDamage;
    [SerializeField] private int starRestore;
    public GameObject bulwark;

    public float speedEffectTimer;
    private float speedEffectDuration;

    private float unSwift;
    [SerializeField] private float swiftTimer;
    private bool isSwift;
    public Text scoreText;
    scoreScript scoreScript;
    int score;
    public inventoryView inventory;
    globalScore scorekeeper;

    void Start()
    {
        scorekeeper = GameObject.Find("globalScoreObj").GetComponent<globalScore>();
        if (scorekeeper.returnBlasterLevel() == 1 || scorekeeper.returnBlasterLevel() == 2) {
            enemyDamage = 20;
        } else if (scorekeeper.returnBlasterLevel() == 3) {
            enemyDamage = 40;
        }
        speed = 50.0f;
        maxHealth = rocketHealth;
        blackHoleDrag = 0.0f;
        rb2d = GetComponent<Rigidbody2D> ();
        isSwift = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
        rb2d.AddForce(new Vector2(blackHoleDrag, 0.0f));
    }

    void Update()
    {
        if (isSwift == true) {
            if (Time.time >= unSwift) {
                speed = speed/2;
                isSwift = false;
            }
        }
        if (blackHoleDirection == 1) {
            blackHoleDrag = -20.0f;
        } else if (blackHoleDirection == 2) {
            blackHoleDrag = 20.0f;
        } else if (blackHoleDirection == 0) {
            blackHoleDrag = 0.0f;
        }
    }

    void playerDie() {
        Destroy(gameObject);
        Debug.Log("Dead");
        scoreScript = scoreText.GetComponent<scoreScript>();
        score = scoreScript.returnScore();
        scorekeeper.updateBlasterScore(score);
        SceneManager.LoadScene("Scene_endGame");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            rocketHealth = rocketHealth - enemyDamage;
            //Debug.Log(rocketHealth);
            if (rocketHealth <= 0)
            {
                playerDie();
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "enemBullet")
        {
            rocketHealth = rocketHealth - enemyDamage;
            //Debug.Log(rocketHealth);
            if (rocketHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Dead");
                
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Star")
        {
            if (rocketHealth < (maxHealth-starRestore))
            {
                rocketHealth = rocketHealth + starRestore;
                //Debug.Log(rocketHealth);
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Swift")
        {
            inventory.addToInventory("Swift");
            speed = speed*2;
            Destroy(col.gameObject);
            //speedEffectTimer = Time.time + speedEffectDuration;
            unSwift = Time.time + swiftTimer;
            isSwift = true;
        }
        if (col.gameObject.tag == "Bulwark")
        {
            inventory.addToInventory("Bulwark");
            Destroy(col.gameObject);
            Instantiate(bulwark);
        }
        if (col.gameObject.tag == "Rapid") {
            inventory.addToInventory("Rapid");
        }
        if (col.gameObject.tag == "Spread") {
            inventory.addToInventory("Spread");
        }
    }

    public void blackHolePresent(bool blackHole, int holeDirec) {
        blackHoleDirection = holeDirec;
    }
    public void noBlackHole() {
        blackHoleDrag = 0.0f;
    }

}
