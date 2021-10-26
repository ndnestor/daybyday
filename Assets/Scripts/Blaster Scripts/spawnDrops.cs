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

     // SPAWN HIERARCHY (Common --> Rare)
     /**
     1. Rapid
     2. Swift
     3. Spread
     4. Heal
     5. Shield
     **/

    void Awake() {
        healIncr = UnityEngine.Random.Range(1, 10);
        //healIncr = UnityEngine.Random.Range(18, 23);
        bulwarkIncr = UnityEngine.Random.Range(45, 60);
        swiftIncr = UnityEngine.Random.Range(25, 30);
        rapidIncr = UnityEngine.Random.Range(25, 30);
        spreadIncr = UnityEngine.Random.Range(15, 20);
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
             healIncr = UnityEngine.Random.Range(1, 10);
             //healIncr = UnityEngine.Random.Range(18, 23);
             healTimer = Time.time + healIncr;
         }
         if (bulwarkTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpBulwark = Instantiate(bulwark, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpBulwark.transform.position = new Vector3(randX, tmpBulwark.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             bulwarkIncr = UnityEngine.Random.Range(25, 30);
             bulwarkTimer = Time.time + bulwarkIncr;
         }
         if (swiftTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpSwift = Instantiate(swift, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpSwift.transform.position = new Vector3(randX, tmpSwift.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             swiftIncr = UnityEngine.Random.Range(13, 18);
             swiftTimer = Time.time + swiftIncr;
         }
         if (rapidTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpRapid = Instantiate(rapid, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpRapid.transform.position = new Vector3(randX, tmpRapid.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             rapidIncr = UnityEngine.Random.Range(10, 15);
             rapidTimer = Time.time + rapidIncr;
         }
         if (spreadTimer < Time.time) {
             float randX = UnityEngine.Random.Range(-4.4f, 4.4f);
             //Debug.Log(string.Format("{0:N2}", randX));
             GameObject tmpSpread = Instantiate(spread, new Vector3(0.0f, 7.0f, 0.0f), Quaternion.identity);
             tmpSpread.transform.position = new Vector3(randX, tmpSpread.transform.position.y, 0.0f);
             // ALTER FREQUENCY HERE
             spreadIncr = UnityEngine.Random.Range(15, 20);
             spreadTimer = Time.time + spreadIncr;
         }
     }
 }
