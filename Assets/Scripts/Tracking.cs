using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a Singleton instance
public class Tracking
{
    private static Tracking instance = null;
    private Tracking() { }

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

    public void Test()
    {
        Debug.Log("Success");
    }
}
