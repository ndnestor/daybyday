using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlatformHealth : MonoBehaviour
{
    
    public int maxHealth;
    public int currentHealth;
    public HealthBar_Platform healthBar;
    public AudioSource audioOuch;
    public fakeHealthBar_Plat bar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioOuch = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    
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
                Destroy(gameObject);
                Debug.Log("Dead");
                SceneManager.LoadScene("Scene_endGame");
            }
        }
        if (col.gameObject.tag == "Whale")
        {
            TakeDamage(20);
            audioOuch.Play();
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Dead");
                SceneManager.LoadScene("Scene_endGame");
            }
        }
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
