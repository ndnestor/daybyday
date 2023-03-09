using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawnscript : MonoBehaviour {
    [SerializeField] private float minTimeUntilSquid, maxTimeUntilSquid;
    [SerializeField] private float minTimeUntilWhale, maxTimeUntilWhale;
    [SerializeField] private float minTimeUntilShip, maxTimeUntilShip;
    [SerializeField] private float minTimeUntilHole, maxTimeUntilHole;
    [SerializeField] private double difficultyFactor;

    public GameObject enemy, enem_Whale, enem_Hole, enem_Ship;
     private float spawnTimer, spawnIncr;
     private float spawnTimer_Whale, spawnIncr_Whale;
     private float spawnTimer_Hole, holeDespawnTimer, holeDespawnLength, spawnIncr_Hole, randomizerX;
     float xHole;
     bool nextHoleSpawnSet, nextHoleDespawnSet;
     private float spawnTimer_Ship, spawnIncr_Ship;
     enemyScriptHole holeScript;
     public PlayerMovement rocketMovement;
     GlobalScore scorekeeper;

    void Awake() {
        scorekeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
        spawnIncr = Random.Range(minTimeUntilSquid, maxTimeUntilSquid);
        spawnIncr_Whale = Random.Range(minTimeUntilWhale, maxTimeUntilWhale);
        spawnIncr_Ship = Random.Range(minTimeUntilShip, maxTimeUntilShip);
        spawnIncr_Hole = Random.Range(minTimeUntilHole, maxTimeUntilHole);
        
        //Debug.Log("Start time " + Time.time);
        
        spawnTimer = Time.time + spawnIncr;
        spawnTimer_Whale = Time.time + spawnIncr_Whale;
        spawnTimer_Ship = Time.time + spawnIncr_Ship;

        /* Rather than spawning a new black hole every time, Spawnscript refers
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
        holeDespawnLength = 5.0f;
        spawnTimer_Hole = Time.time + spawnIncr_Hole;
        randomizerX = Random.Range(0.0f, 1.0f);
        Debug.Log("Initial hole spawn time " + spawnTimer_Hole);
        holeScript = enem_Hole.GetComponent<enemyScriptHole>();
    }

     public void Update() {
         float spawnIntervalChangeFactor = (float)Math.Pow(difficultyFactor, Time.deltaTime);
         minTimeUntilSquid *= spawnIntervalChangeFactor;
         maxTimeUntilSquid *= spawnIntervalChangeFactor;
         minTimeUntilWhale *= spawnIntervalChangeFactor;
         maxTimeUntilWhale *= spawnIntervalChangeFactor;
         minTimeUntilShip *= spawnIntervalChangeFactor;
         maxTimeUntilShip *= spawnIntervalChangeFactor;
         minTimeUntilHole *= spawnIntervalChangeFactor;
         maxTimeUntilHole *= spawnIntervalChangeFactor;

         if (spawnTimer < Time.time) {
             float randX = Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmp = Instantiate(enemy, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity, transform);
             tmp.transform.position = new Vector3(randX, tmp.transform.position.y, 0.0f);
             spawnIncr = Random.Range(minTimeUntilSquid, maxTimeUntilSquid);

             spawnTimer = Time.time + spawnIncr;
         }
         if (spawnTimer_Whale < Time.time) {
             float randY = Random.Range(-0.2f, 3.2f);
             //Debug.Log(string.Format("{0:N2}", randY));
             GameObject tmp_Whale = Instantiate(enem_Whale,
                 new Vector3(-9.0f,
                     -2.0f,
                     0.0f),
                 Quaternion.identity,
                 transform);
             tmp_Whale.transform.position = new Vector3(tmp_Whale.transform.position.x, randY, 0.0f);
             spawnIncr_Whale = Random.Range(minTimeUntilWhale, maxTimeUntilWhale);

             spawnTimer_Whale = Time.time + spawnIncr_Whale;
         } 
         if (spawnTimer_Ship < Time.time) {
             float randY_ship = Random.Range(-0.2f, 3.2f);
             //Debug.Log(string.Format("{0:N2}", randY_ship));
             GameObject tmp_Ship = Instantiate(enem_Ship,
                 new Vector3(9.0f,
                     -2.0f,
                     0.0f),
                 Quaternion.identity,
                 transform);
             tmp_Ship.transform.position = new Vector3(tmp_Ship.transform.position.x, randY_ship, 0.0f);
             spawnIncr_Ship = Random.Range(minTimeUntilShip, maxTimeUntilShip);

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
             nextHoleDespawnSet = false;
             holeScript.blackHoleActive(false, false);
             rocketMovement.noBlackHole();
             if (!nextHoleSpawnSet && Time.time > 16.0f) {
                 spawnIncr_Hole = Random.Range(minTimeUntilHole, maxTimeUntilHole);

                 randomizerX = Random.Range(0.0f, 1.0f);
                 spawnTimer_Hole = Time.time + spawnIncr_Hole;
                 nextHoleSpawnSet = true;
             }
             enem_Hole.SetActive(false);
         }
         /* float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
        Debug.Log(string.Format("{0:N2}", randX));
        GameObject tmp = Instantiate(enemy, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
        tmp.transform.position = new Vector3(randX, tmp.transform.position.y, 0.0f); */
     }
     

     
     /* public Vector3 GetRandomPoint()
     {
         int xRandom = 0;
         xRandom = (int)Random.Range(-4.5, 4.5);
         return new Vector3(xRandom, 0.0f);
         Vector3 randPosition = new Vector3(Random.Range(-4.5f, 4.5f), 7.0f, 0);
         // Instantiate(prefab, randPosition, Quaternion.identity);
         return randPosition;
     } */
 
 }