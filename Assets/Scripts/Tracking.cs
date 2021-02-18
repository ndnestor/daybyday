using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a Singleton instance
public class Tracking
{
    private static Tracking instance = null;
    private Tracking() { }

    public readonly int MAX_TIME = 24;
    public int timeUsed = 0;
    public ArrayList objectUsage = new ArrayList();

    public static Tracking Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Tracking();
            }
            return instance;
        }
    }

    //Adds an object to an ArrayList in chronological order of use
    public void AddObject(GameObject obj)
    {
        int listLength = objectUsage.Count;
        objectUsage.Add(obj);
    }

    //Counts the amount of times an object (param obj) has been used
    public int CountObjectUses(GameObject obj)
    {
        int count = 0;

        foreach(GameObject a in objectUsage)
        {
            if(a == obj)
            {
                count++;
            }
        }

        return count;
    }

    /**Adds time units for each object used (Requires a check against MAX_TIME before call)
     * param additionalTime --> timeValue of the object
     * returns total time units used in the day
     */
    public int AddUsedTime(int additionalTime)
    {
        timeUsed += additionalTime;

        return timeUsed;
    }
}
