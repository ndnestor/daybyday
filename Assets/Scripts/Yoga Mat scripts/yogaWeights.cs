using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class yogaWeights : MonoBehaviour
{
    public Rigidbody2D rb;
    float speed, transY;
    float moveTimer, moveIncr, startWait;
    public TextMeshProUGUI startTimer, inOutText, scoreTimerText;
    public GameObject rangeDetector;
    public GameObject player;
    float distance, outTime;
    public float scoreTime, scoreTimeInt;
    bool inBounds;
    SpriteRenderer playerSprite;

    globalScore globalScoreKeeper;

    //Note that range detection and scale will both change over days
    // Right now halfRange manually set to 5; change as nec via code
    float halfRange;

    void Start() {
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<globalScore>();
        moveIncr = 1.5f;
        startWait = Time.time + 3.0f;
        speed = 0;
        inBounds = true;
        playerSprite = player.GetComponent<SpriteRenderer>();
        halfRange = 1f;
        outTime = 10;
        inOutText.text = " ";
        scoreTime = 0;
        scoreTimeInt = 0;
    }
    void FixedUpdate() {
        // Start timer countdown
        if (Time.time >= 4.0) {
            startTimer.text = " ";
        } else if (Time.time >= 3.0) {
            startTimer.text = "START";
        } else if (Time.time >= 2.0) {
            startTimer.text = "Starting in 1";
        } else if (Time.time >= 1.0) {
            startTimer.text = "Starting in 2";
        } else if (Time.time >= 0.0) {
            startTimer.text = "Starting in 3";
        }

        if (Time.time <= 5.0) {
            inOutText.text = " ";
        }

        if (startWait < Time.time) {
            speed = 1;
        } else {
            rb.transform.position = new Vector3(-7.492371f, 0.0f, 0.0f);
        }
        if(moveTimer < Time.time) {
            switchDirec();
            moveTimer = Time.time + moveIncr;
        }

        distance = Vector3.Distance(rangeDetector.transform.position, player.transform.position);
        if (distance <= halfRange) {
            inBounds = true;
            Debug.Log("Player in bounds");
        } else {
            inBounds = false;
            Debug.Log("Player out of bounds");
        }
        //Debug.Log(distance);
        notifChange();
        if (outTime == 0) {
            gameOver();
        }
        scoreTime = Time.time;
        if (scoreTime%1 == 0) {
            scoreTimerText.text = scoreTime.ToString();
            scoreTimeInt = scoreTime;
         }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall") {
            switchDirec();
        }
    }

    void switchDirec() {
        transY = UnityEngine.Random.Range(0, 10);
         if (transY < 5) {
             rb.velocity = -transform.up * speed;
         } else {
             rb.velocity = transform.up * speed;
         }
         moveTimer = Time.time + moveIncr;
    }

    void notifChange() {
        if (inBounds == true) {
            playerSprite.color = new Color (0.149f,0.325f,0.2235f, 1);
            inOutText.color = new Color (0.149f,0.325f,0.2235f, 1);
        } else {
            playerSprite.color = new Color (0.723f,0.992f,0.867f, 1);
            if (Time.time % 1 == 0 && Time.time > 5.0f) {
                outTime -= 1;
            }
            inOutText.color = new Color (0.616f,0.004f,0.004f, 1);
        }
        if (Time.time > 5.0f) {
            inOutText.text = outTime.ToString();
        }
    }
    
    void gameOver() {
        globalScoreKeeper.updateYogaScore(scoreTimeInt, 2);
        SceneManager.LoadScene("Yoga_gameOver");
    }
}
