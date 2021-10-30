using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Spawnscript : MonoBehaviour {
     public GameObject enemy, enem_Whale, enem_Hole, enem_Ship;
     private float spawnTimer, spawnIncr;
     private float spawnTimer_Whale, spawnIncr_Whale;
     private float spawnTimer_Hole, holeDespawnTimer, holeDespawnLength, spawnIncr_Hole, randomizerX;
     float xHole;
     bool nextHoleSpawnSet, nextHoleDespawnSet;
     private float spawnTimer_Ship, spawnIncr_Ship;
     enemyScriptHole holeScript;

    void Awake() {
        Debug.Log("Start time " + Time.time);
        spawnIncr = UnityEngine.Random.Range(0.5f, 5.0f);
        spawnTimer = Time.time + spawnIncr;

        spawnIncr_Whale = UnityEngine.Random.Range(30.0f, 40.0f);
        spawnTimer_Whale = Time.time + spawnIncr_Whale;

        /** Rather than spawning a new black hole every time, Spawnscript refers
        to a single hole that already exists in the scene— turns on/off and alters X coordinate.
        The main reason for this is that the enemy hole behaviour influences player movement,
        so it needs to reference the playerMovement script on the rocket.
        
        If spawnScript references the prefab, can't drag the rocket object into the black hole's
        inspector (scriptableObject independent of scene, reference to rocket doesn't exist
        outside of this scene).
        Short of using GameObject.Find or GameObject.FindWithTag, not sure how else to work around
        So instead of referencing black hole prefab, will just modify an object in scene

        "Spawn" AND "despawn" will both be controlled here rather than in obj. script
        */
        holeDespawnLength = 10.0f;
        //spawnIncr_Hole = UnityEngine.Random.Range(45.0f, 240.0f);
        spawnIncr_Hole = UnityEngine.Random.Range(15.0f, 20.0f);
        spawnTimer_Hole = Time.time + spawnIncr_Hole;
        randomizerX = UnityEngine.Random.Range(0.0f, 1.0f);
        Debug.Log("Initial hole spawn time " + spawnTimer_Hole);
        holeScript = enem_Hole.GetComponent<enemyScriptHole>();

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
             Debug.Log("Spawning black hole");
             enem_Hole.SetActive(true);
             nextHoleSpawnSet = false;
             if (!nextHoleDespawnSet) {
                holeDespawnTimer = Time.time + holeDespawnLength;
                nextHoleDespawnSet = true;
             }
             //float randomizerX = UnityEngine.Random.Range(0.0f, 1.0f);
             if (randomizerX < 0.5) {
                 holeScript.blackHoleActive(true, false);
                 xHole = -3.48f;
             }
             else {
                 xHole = 3.48f;
                 holeScript.blackHoleActive(false, true);
             }
             //Debug.Log(string.Format("{0:N2}", randX));
             //GameObject tmp_Hole = Instantiate(enem_Hole, new Vector3(0.0f, -2.72f, 0.0f), Quaternion.identity);
             enem_Hole.transform.position = new Vector3(xHole, -2.72f, 0.0f);
         }
         if (Time.time > holeDespawnTimer) {
             holeScript.blackHoleActive(false, false);
             nextHoleDespawnSet = false;
             if (!nextHoleSpawnSet && Time.time > 22.0f) {
                 Debug.Log("Resetting hole spawn");
                 //spawnIncr_Hole = UnityEngine.Random.Range(45.0f, 240.0f);
                 spawnIncr_Hole = UnityEngine.Random.Range(15.0f, 20.0f);
                 randomizerX = UnityEngine.Random.Range(0.0f, 1.0f);
                 spawnTimer_Hole = Time.time + spawnIncr_Hole;
                 Debug.Log("Next hole spawn time " + spawnTimer_Hole);
                 nextHoleSpawnSet = true;
             }
             enem_Hole.SetActive(false);
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