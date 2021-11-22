using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlatformHealth : MonoBehaviour
{
    
    public int maxHealth;
    public int currentHealth;
    //public HealthBar_Platform healthBar;
    public fakeHealthBar healthBar;
    public AudioSource audioOuch;
    public fakeHealthBar_Plat bar;
    globalScore globalScoreKeeper;
    public Text scoreText;
    scoreScript scoreScript;
    int score;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioOuch = GetComponent<AudioSource>();
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<globalScore>();
    }
    
    /* void Update()
    {
        Example code to test HealthBar function with space key lower health
         if(Input.GetKeyDown(KeyCode.Space))
         {
            TakeDamage(20);
    }
    */

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
            audioOuch.Play();
            if (currentHealth <= 0)
            {
                platformDie();
            }
        }
        if (col.gameObject.tag == "Whale")
        {
            TakeDamage(20);
            audioOuch.Play();
            if (currentHealth <= 0)
            {
                platformDie();
            }
        }
    }

    void platformDie() {
        Destroy(gameObject);
        Debug.Log("Dead");
        scoreScript = scoreText.GetComponent<scoreScript>();
        score = scoreScript.returnScore();
        globalScoreKeeper.updateBlasterScore(score);
        SceneManager.LoadScene("Scene_endGame");
    }

    void TakeDamage(int damage)
    {
        Debug.Log("Damage to platform is: " + damage);
        currentHealth -= damage;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        //healthBar.SetHealth(currentHealth);
        Debug.Log("Health bar health " + currentHealth);
        bar.healthBarSet();
    }
}
