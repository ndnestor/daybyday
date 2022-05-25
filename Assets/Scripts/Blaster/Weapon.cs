using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint_L;
    public Transform firePoint_R;
    public GameObject bulletPrefab;
    public GameObject spreadBulletPrefab_R;
    public GameObject spreadBulletPrefab_L;
    private bool isSpread;
    public float unSpread;
    private float spreadTimer;

    public float reloadTime;
    private float shootTime;
    private bool isRapid;
    private float rapidTimer;
    public float unRapid;

    public AudioSource bulletNoise;

    // Update is called once per frame
    void Update()
    {
        // AYO TRY USING THE SPACE BAR IF YOU CAN LATER IT'S Input.GetKey(KeyCode.Space)
        if (isSpread == true) {
            if(Input.GetButtonDown("Fire1") && shootTime <= Time.time)
            {
                SpreadShoot();
            }
            if(Time.time >= spreadTimer) {
                isSpread = false;
            }
        } else {
            if(Input.GetButtonDown("Fire1") && shootTime <= Time.time)
            {
                Shoot();
            }
        }
        if (isRapid == true && Time.time >= rapidTimer)
        {
            isRapid = false;
            reloadTime = reloadTime * 3;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spread")
        {
            isSpread = true;
            Debug.Log("Spread true");
            Destroy(col.gameObject);
            //speedEffectTimer = Time.time + speedEffectDuration;
            spreadTimer = Time.time + unSpread;
        }
        if (col.gameObject.tag == "Rapid")
        {
            isRapid = true;
            Destroy(col.gameObject);
            reloadTime = reloadTime / 3;
            rapidTimer = Time.time + unRapid;
            //Debug.Log("Time is " + Time.time);
            //Debug.Log("Rapid timer is " + rapidTimer);
        }
    }
    
    void Shoot ()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Debug.Log("Shoot");
        shootTime = Time.time + reloadTime;
        bulletNoise.Play();
    }
    void SpreadShoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(spreadBulletPrefab_R, firePoint_R.position, firePoint.rotation);
        Instantiate(spreadBulletPrefab_L, firePoint_L.position, firePoint.rotation);
        Debug.Log("Spread shoot");
        shootTime = Time.time + reloadTime;
        bulletNoise.Play();
    }
}
