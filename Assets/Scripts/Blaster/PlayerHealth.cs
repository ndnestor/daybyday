using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int currentHealth;
    //public HealthBar healthBar;
    public AudioSource audioOof;
    public fakeHealthBar bar;

    void Start()
    {
        currentHealth = maxHealth;
        bar.SetMaxHealth(maxHealth);
        audioOof = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
            audioOof.Play();
        }
        if (col.gameObject.tag == "Whale")
        {
            TakeDamage(20);
            audioOof.Play();
        }
        if (col.gameObject.tag == "enemBullet")
        {
            TakeDamage(20);
            audioOof.Play();
        }
        if (col.gameObject.tag == "Star")
        {
            TakeDamage(-10);
        }
    }
    void TakeDamage(int damage)
    {
        //Debug.Log("Damage is: " + damage);
        currentHealth -= damage;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            Debug.Log("Health is" + currentHealth);
        } else {
            if(damage < 0) {
            bar.healthBarSetHeal();
            }
        }
        if(damage > 0) {
            bar.healthBarSet();
        }
        //Debug.Log("Current health: " + currentHealth);
    }
}
