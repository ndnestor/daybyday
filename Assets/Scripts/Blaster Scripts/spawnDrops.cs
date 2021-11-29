using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class spawnDrops : MonoBehaviour {
     public GameObject heal;
     private float healTimer;
     private float healIncr;

     public GameObject bulwark;
     private float bulwarkTimer;
     private float bulwarkIncr;

     public GameObject swift;
     private float swiftTimer;
     private float swiftIncr;

     public GameObject rapid;
     private float rapidTimer;
     private float rapidIncr;

     public GameObject spread;
     private float spreadTimer;
     private float spreadIncr;
     globalScore scorekeeper;
     public int gameLevel;

     // SPAWN HIERARCHY (Common --> Rare)
     /**
     1. Rapid
     2. Swift
     3. Spread
     4. Heal
     5. Shield
     **/

    void Start() {
        scorekeeper = GameObject.Find("globalScoreObj").GetComponent<globalScore>();
    }

    void Awake() {
        gameLevel = scorekeeper.returnBlasterLevel();
        switch(gameLevel) {
            case 1:
                healIncr = UnityEngine.Random.Range(20, 30);
                bulwarkIncr = UnityEngine.Random.Range(60, 90);
                swiftIncr = UnityEngine.Random.Range(40, 60);
                rapidIncr = UnityEngine.Random.Range(40, 60);
                spreadIncr = UnityEngine.Random.Range(40, 60);
                break;
            case 2:
                healIncr = UnityEngine.Random.Range(40, 60);
                bulwarkIncr = UnityEngine.Random.Range(90, 240);
                swiftIncr = UnityEngine.Random.Range(60, 120);
                rapidIncr = UnityEngine.Random.Range(60, 120);
                spreadIncr = UnityEngine.Random.Range(60, 120);
                break;
            case 3:
                healIncr = UnityEngine.Random.Range(60, 120);
                bulwarkIncr = UnityEngine.Random.Range(120, 300);
                swiftIncr = UnityEngine.Random.Range(120, 240);
                rapidIncr = UnityEngine.Random.Range(120, 240);
                spreadIncr = UnityEngine.Random.Range(120, 240);
                break;
        }
        healTimer = Time.time + healIncr;
        bulwarkTimer = Time.time + bulwarkIncr;
        swiftTimer = Time.time + swiftIncr;
        rapidTimer = Time.time + rapidIncr;
        spreadTimer = Time.time + spreadIncr;
    }

     void Update() {
         if (healTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpHeal = Instantiate(heal, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpHeal.transform.position = new Vector3(randX, tmpHeal.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             healIncr = UnityEngine.Random.Range(20, 30);
             //healIncr = UnityEngine.Random.Range(18, 23);
             healTimer = Time.time + healIncr;
         }
         if (bulwarkTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpBulwark = Instantiate(bulwark, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpBulwark.transform.position = new Vector3(randX, tmpBulwark.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             bulwarkIncr = UnityEngine.Random.Range(60, 90);
             bulwarkTimer = Time.time + bulwarkIncr;
         }
         if (swiftTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpSwift = Instantiate(swift, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpSwift.transform.position = new Vector3(randX, tmpSwift.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             swiftIncr = UnityEngine.Random.Range(40, 60);
             swiftTimer = Time.time + swiftIncr;
         }
         if (rapidTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpRapid = Instantiate(rapid, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpRapid.transform.position = new Vector3(randX, tmpRapid.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             rapidIncr = UnityEngine.Random.Range(40, 60);
             rapidTimer = Time.time + rapidIncr;
         }
         if (spreadTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpSpread = Instantiate(spread, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpSpread.transform.position = new Vector3(randX, tmpSpread.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             spreadIncr = UnityEngine.Random.Range(40, 60);
             spreadTimer = Time.time + spreadIncr;
         }
     }
 }