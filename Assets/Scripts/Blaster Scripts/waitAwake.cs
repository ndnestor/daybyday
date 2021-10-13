using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitAwake : MonoBehaviour
{
    public GameObject endMusicObj;
    public float audioWait;
    AudioSource endMusicSource;
    bool finalVol; // initialized to false
    void Start() {
        //Debug.Log("Endgame start time " + Time.time);
        endMusicSource = endMusicObj.GetComponent<AudioSource>();
        endMusicSource.enabled = false;
        //endMusicSource.volume = 0.1f;
    }
    void FixedUpdate(){
        if(Time.timeSinceLevelLoad >= audioWait && !endMusicSource.enabled) {
            //Debug.Log("Music start time " + Time.time);
            endMusicSource.enabled = true;
        }
    }
}
