using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Spawnscript : MonoBehaviour {
     public GameObject enemy;
     public GameObject enem_Whale;
     public GameObject enem_Hole;
     public GameObject enem_Ship;
     private float spawnTimer;
     private float spawnIncr;
     private float spawnTimer_Whale;
     private float spawnIncr_Whale;
     private float spawnTimer_Hole;
     private float spawnIncr_Hole;
     private float spawnTimer_Ship;
     private float spawnIncr_Ship;

     private float xHole;

    void Awake() {
        spawnIncr = UnityEngine.Random.Range(0.5f, 5.0f);
        spawnTimer = Time.time + spawnIncr;

        spawnIncr_Whale = UnityEngine.Random.Range(30.0f, 40.0f);
        spawnTimer_Whale = Time.time + spawnIncr_Whale;

        spawnIncr_Hole = UnityEngine.Random.Range(45.0f, 240.0f);
        spawnTimer_Hole = Time.time + spawnIncr_Hole;

        spawnIncr_Ship = UnityEngine.Random.Range(55.0f, 120.0f);
        spawnTimer_Ship = Time.time + spawnIncr_Ship;
    }

     public void Update() {
         if (spawnTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmp = Instantiate(enemy, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmp.transform.position = new Vector3(randX, tmp.transform.position.y, 0.0f);
             spawnIncr = UnityEngine.Random.Range(0.5f, 5.0f);
             spawnTimer = Time.time + spawnIncr;
         }
         if (spawnTimer_Whale < Time.time) {
             float randY = UnityEngine.Random.Range(-0.2f, 3.2f);
             //Debug.Log(string.Format("{0:N2}", randY));
             GameObject tmp_Whale = Instantiate(enem_Whale, new Vector3(-9.0f, -2.0f, 0.0f), 
             Quaternion.identity);
             tmp_Whale.transform.position = new Vector3(tmp_Whale.transform.position.x, randY, 0.0f);
             spawnIncr_Whale = UnityEngine.Random.Range(30.0f, 40.0f);
             spawnTimer_Whale = Time.time + spawnIncr_Whale;
         } 
         if (spawnTimer_Ship < Time.time) {
             float randY_ship = UnityEngine.Random.Range(-0.2f, 3.2f);
             //Debug.Log(string.Format("{0:N2}", randY_ship));
             GameObject tmp_Ship = Instantiate(enem_Ship, new Vector3(9.0f, -2.0f, 0.0f), 
             Quaternion.identity);
             tmp_Ship.transform.position = new Vector3(tmp_Ship.transform.position.x, randY_ship, 0.0f);
             spawnIncr_Ship = UnityEngine.Random.Range(55.0f, 120.0f);
             spawnTimer_Ship = Time.time + spawnIncr_Ship;
         } 
         if (spawnTimer_Hole < Time.time) {
             float randomizerX = UnityEngine.Random.Range(0.0f, 1.0f);
             if (randomizerX < 0.5) {
                 xHole = -3.48f;
             }
             else {
                 xHole = 3.48f;
             }
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmp_Hole = Instantiate(enem_Hole, new Vector3(0.0f, -2.72f, 0.0f), 
             Quaternion.identity);
             tmp_Hole.transform.position = new Vector3(xHole, tmp_Hole.transform.position.y, 0.0f);
             spawnIncr_Hole = UnityEngine.Random.Range(45.0f, 240.0f);
             spawnTimer_Hole = Time.time + spawnIncr_Hole;
         } 
         /** float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
        Debug.Log(string.Format("{0:N2}", randX));
        GameObject tmp = Instantiate(enemy, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
        tmp.transform.position = new Vector3(randX, tmp.transform.position.y, 0.0f); **/
     }
     

     
     /** public Vector3 GetRandomPoint()
     {
         int xRandom = 0;
         xRandom = (int)Random.Range(-4.5, 4.5);
         return new Vector3(xRandom, 0.0f);
         Vector3 randPosition = new Vector3(Random.Range(-4.5f, 4.5f), 7.0f, 0);
         // Instantiate(prefab, randPosition, Quaternion.identity);
         return randPosition;
     } **/
 
 }