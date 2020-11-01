using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public string[] objects;
    public int[] objectTracker;
    public int[] objectTimePass;
    public int numOfObjects = 3; // for now
    public static int currTimeUnit;
    public int i;
    public static int currDay;
    // Start is called before the first frame update
    void Start()
    {
        objects = new string[] {"Bed", "Computer", "Window"};
        objectTracker = new int[] {0, 0, 0};
        objectTimePass = new int[] {8, 4, 1};
        currTimeUnit = 6;
        currDay = 0;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            // indicates interacting with a bed
            i = 0;
            UpdateTime();
            objectTracker[i]++;
            TrackObjects();
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            // indicates interacting with the computer
            i = 1;
            UpdateTime();
            objectTracker[i]++;
            TrackObjects();
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            // indicates interacting with the fridge
            i = 2;
            UpdateTime();
            objectTracker[i]++;
            TrackObjects();
        }

        

    }

    void UpdateTime() 
    {
        if (currTimeUnit + objectTimePass[i] > 23) {
            //int deductor = 23 - currTimeUnit + 1;
            //currTimeUnit = 0 + (objectTimePass[i] - deductor);
            int overTime = currTimeUnit + objectTimePass[i];
            currTimeUnit = overTime - 23 - 1;
            currDay = 1;
        } else {
            currTimeUnit += objectTimePass[i];
        }

        if (currTimeUnit >= 6 && currTimeUnit < 18) {
                Debug.Log("Day; Current Time Now: " + currTimeUnit);
        } else {
                Debug.Log("Night, Current Time Now: " + currTimeUnit);
        }
        /*
        if (currDay == 1 && currTimeUnit >= 6) {
            Debug.Log("NEW DAY");
        }
        */

    }

    void TrackObjects()
    {
        int x = 0;
        int max = objectTracker[0];
        for (int j = 0; j < numOfObjects; j++) {
            if (max < objectTracker[j]) {
                max = objectTracker[j];
                x = j;
            }
            Debug.Log(objects[j] + " has " + objectTracker[j] + " uses.");
        }
        Debug.Log("The " + objects[x] + " is the most used object, with " + objectTracker[x] + " uses.");
    }
}
