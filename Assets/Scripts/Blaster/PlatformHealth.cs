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
    GlobalScore globalScoreKeeper;
    public Text scoreText;
    scoreScript scoreScript;
    int score;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioOuch = GetComponent<AudioSource>();
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
    }
    
    /* void Update()
    {
        Example code to test HealthBar function with space key lower health
         if(Input.GetKeyDown(KeyCode.Space))
         {
            TakeDamage(20);
    }
    */

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
            audioOuch.Play();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        if (col.gameObject.tag == "Whale")
        {
            TakeDamage(20);
            audioOuch.Play();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die() {
        //Destroy(gameObject);
        Debug.Log("Dead");
        scoreScript = scoreText.GetComponent<scoreScript>();
        score = scoreScript.returnScore();
        globalScoreKeeper.UpdateBlasterScore(score);
        //SceneManager.LoadScene("Scene_endGame", LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync("Scene_Game");

        SceneLoader.Instance.LoadAsync("Scene_endGame", LoadSceneMode.Additive, onLoadedCallback: () =>
        {
            SceneManager.UnloadSceneAsync("Scene_Game");
        });
    }

    private void TakeDamage(int damage)
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
