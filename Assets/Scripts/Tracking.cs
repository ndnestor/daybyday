using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a Singleton instance
public class Tracking
{
    private static Tracking instance = null;
    private Tracking() { }

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
}
