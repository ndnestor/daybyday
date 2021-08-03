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
    float timeAtStart, gameTime;
    int lastIntTime, nextIntTime;
    SpriteRenderer playerSprite;

    globalScore globalScoreKeeper;

    //Note that range detection and scale will both change over days
    // Right now halfRange manually set to 5; change as nec via code
    float halfRange;

    void Start() {
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<globalScore>();
        moveIncr = 1.5f;
        speed = 0;
        inBounds = true;
        playerSprite = player.GetComponent<SpriteRenderer>();
        halfRange = 1f;
        outTime = 10;
        inOutText.text = " ";
        scoreTime = 0;
        scoreTimeInt = 0;
        timeAtStart = Time.time;
        gameTime = Time.time - timeAtStart;
        startWait = timeAtStart + 3.0f;
        lastIntTime = 0;
    }
    void FixedUpdate() {
        gameTime = Time.time - timeAtStart;
        lastIntTime = Mathf.FloorToInt(gameTime);
        // Start timer countdown
        if (gameTime >= 4.0) {
            startTimer.text = " ";
        } else if (gameTime >= 3.0) {
            startTimer.text = "START";
        } else if (gameTime >= 2.0) {
            startTimer.text = "Starting in 1";
        } else if (gameTime >= 1.0) {
            startTimer.text = "Starting in 2";
        } else if (gameTime >= 0.0) {
            startTimer.text = "Starting in 3";
        }

        if (gameTime <= 5.0) {
            inOutText.text = " ";
        }

        if (startWait < gameTime) {
            speed = 1;
        } else {
            rb.transform.position = new Vector3(-7.492371f, 0.0f, 0.0f);
        }
        if(moveTimer < gameTime) {
            switchDirec();
            moveTimer = gameTime + moveIncr;
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
        scoreTime = gameTime;
        //Debug.Log("Score time " + scoreTime);
        if (Mathf.FloorToInt(scoreTime) >= lastIntTime) {
            scoreTimeInt = Mathf.FloorToInt(scoreTime);
            scoreTimerText.text = scoreTimeInt.ToString();
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
         moveTimer = gameTime + moveIncr;
    }

    void notifChange() {
        if (inBounds == true) {
            playerSprite.color = new Color (0.149f,0.325f,0.2235f, 1);
            inOutText.color = new Color (0.149f,0.325f,0.2235f, 1);
        } else {
            playerSprite.color = new Color (0.723f,0.992f,0.867f, 1);
            if (gameTime >= lastIntTime && gameTime > 5.0f && gameTime >= nextIntTime) {
                outTime -= 1;
                nextIntTime = lastIntTime + 1;
            }
            inOutText.color = new Color (0.616f,0.004f,0.004f, 1);
        }
        if (gameTime > 5.0f) {
            inOutText.text = outTime.ToString();
        }
    }
    
    void gameOver() {
        globalScoreKeeper.updateYogaScore(scoreTimeInt, 2);
        SceneManager.LoadScene("Yoga_gameOver");
    }
}
