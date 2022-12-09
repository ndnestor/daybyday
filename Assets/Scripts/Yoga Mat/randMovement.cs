using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class randMovement : MonoBehaviour
{
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
    
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(playerDot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
        timeAtStart = Time.time;
        lastIntTime = 0;
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
        bounceIncr = 1.5f;
        playerInBounds = true;
        rb = GetComponent<Rigidbody2D>();
        rb.transform.position = new Vector3(-2.4f, 0.08f, 0.0f);
        transX = Random.Range(0, 10);
        transY = Random.Range(0, 10);
         if (transX < 5) {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed - transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed - transform.right * speed;
             }
         } else {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed + transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed + transform.right * speed;
             }
         }
         bounceTimer = timeAtStart + bounceIncr;
         scoreTime = timeAtStart;
         outTime = 10;
        scoreTime = 0;
        scoreTimeInt = 0;
    }

    private void Update(){
         gameTime = Time.time - timeAtStart;
         lastIntTime = Mathf.FloorToInt(gameTime);
         distance = Vector3.Distance (circleCenter.transform.position, playerDot.transform.position);
         if (gameTime > bounceTimer) {
             ChangeDirection();
         }
         if (distance <= 1.22) {
             playerInBounds = true;
         } else {
             playerInBounds = false;
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
    
    private void ChangeDirection() {
        transX = Random.Range(0, 10);
        transY = Random.Range(0, 10);
         if (transX < 5) {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed - transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed - transform.right * speed;
             }
         } else {
             if (transY < 5) {
                 rb.velocity = -transform.up * speed + transform.right * speed;
             } else {
                 rb.velocity = transform.up * speed + transform.right * speed;
             }
         }
         bounceTimer = gameTime + bounceIncr;
    }

    void OnCollisionEnter2D()
    {
        ChangeDirection();
    }

    void gameOver() {
        globalScoreKeeper.UpdateYogaScore(scoreTimeInt, 1);
        SceneManager.LoadScene("Yoga_gameOver", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Yoga_ex1");
    }

}
