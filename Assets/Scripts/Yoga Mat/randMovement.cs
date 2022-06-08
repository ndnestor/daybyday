using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class randMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    private float transX, transY;
    private float bounceTimer, bounceIncr;
    private bool playerInBounds;
    public GameObject circleCenter, playerDot;
    public float distance;
    public TextMeshProUGUI inOutText, scoreTimerText;
    private float scoreTime;
    private int outTime;
    GlobalScore globalScoreKeeper;
    public float scoreTimeInt;
    float timeAtStart, gameTime;
    int lastIntTime, nextIntTime;
    void Start()
    {
        timeAtStart = Time.time;
        lastIntTime = 0;
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
        bounceIncr = 1.5f;
        playerInBounds = true;
        rb = GetComponent<Rigidbody2D> ();
        rb.transform.position = new Vector3(-2.4f, 0.08f, 0.0f);
        transX = UnityEngine.Random.Range(0, 10);
        transY = UnityEngine.Random.Range(0, 10);
         if (transX < 5) {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed - transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed - transform.right*speed;
             }
         } else {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed + transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed + transform.right*speed;
             }
         }
         bounceTimer = timeAtStart + bounceIncr;
         scoreTime = timeAtStart;
         outTime = 10;
        scoreTime = 0;
        scoreTimeInt = 0;
    }

    void FixedUpdate(){
         gameTime = Time.time - timeAtStart;
         lastIntTime = Mathf.FloorToInt(gameTime);
         //rb.velocity = transform.up * speed - transform.right*speed;
         Vector3 pos = transform.position;
         distance = Vector3.Distance (circleCenter.transform.position, playerDot.transform.position);
         if (gameTime > bounceTimer) {
             changeDirection();
         }
         if (distance <= 1.22) {
             playerInBounds = true;
             Debug.Log("Player in bounds is TRUE");
         } else {
             playerInBounds = false;
             Debug.Log("Player in bounds is FALSE");
         }
         notifChange();
         scoreTime = gameTime;
         if (Mathf.FloorToInt(scoreTime) >= lastIntTime) {
             scoreTimeInt = Mathf.FloorToInt(scoreTime);
             scoreTimerText.text = scoreTimeInt.ToString();
         }
         if (outTime == 0) {
             gameOver();
         }
    }

    public void notifChange() {
        if (playerInBounds == true) {
            inOutText.color = new Color (0.149f,0.325f,0.2235f, 1);
        } else {
            if (gameTime >= lastIntTime && gameTime >= nextIntTime) {
                outTime -= 1;
                nextIntTime = lastIntTime + 1;
            }
            inOutText.color = new Color (0.616f,0.004f,0.004f, 1);
        }
        inOutText.text = outTime.ToString();
    }

    
    void changeDirection() {
        Debug.Log("Changing direc");
        transX = UnityEngine.Random.Range(0, 10);
        transY = UnityEngine.Random.Range(0, 10);
         if (transX < 5) {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed - transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed - transform.right*speed;
             }
         } else {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed + transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed + transform.right*speed;
             }
         }
         bounceTimer = gameTime + bounceIncr;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(playerDot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void gameOver() {
        globalScoreKeeper.UpdateYogaScore(scoreTimeInt, 1);
        SceneManager.LoadScene("Yoga_gameOver", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Yoga_ex1");
    }

}
