using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a Singleton instance
public class Tracking : MonoBehaviour
{
    private static Tracking instance = null;
    private Tracking() { }

    public readonly int MAX_TIME = 28;
    public int timeUsed = 0;
    public ArrayList objectUsage = new ArrayList();

    /*
     * Used for window lighting
     * Max and min brightness should be between 0 and 1 inclusive
     */
    private const float MAX_BRIGHTNESS = 1;
    private const float MIN_BRIGHTNESS = 0;
    [SerializeField] private SpriteRenderer windowPaneRenderer;
    [SerializeField] private SpriteRenderer windowLightRenderer;
    [SerializeField] private SpriteRenderer windowFrameHighlightRenderer;
    [SerializeField] private Gradient middayGradient;
    [SerializeField] private Gradient afternoonGradient;
    [SerializeField] private Gradient opacityGradient;

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
        UpdateLighting();

        return timeUsed;
    }

    /*
     * Darkens/brightens window by modifying a shader depending on the time of day
     * The brightness of the window is interpolated linearly between MIN_BRIGHTNESS and MAX_BRIGHTNESS
     */
    private void UpdateLighting()
    {
        float dayPercentage = (float)timeUsed / MAX_TIME;
        float halfDayPercentage;
        Color lightColor;
        if(dayPercentage < 0.5) {
            //Use midday gradient
            halfDayPercentage = (float)timeUsed / (MAX_TIME / 2);
            lightColor = middayGradient.Evaluate(halfDayPercentage);
		} else {
            //Use afternoon gradient
            halfDayPercentage = (float)(timeUsed - MAX_TIME / 2) / (MAX_TIME / 2);
            lightColor = afternoonGradient.Evaluate(halfDayPercentage);
        }
        windowFrameHighlightRenderer.color = new Color(lightColor.r, lightColor.g, lightColor.b, opacityGradient.Evaluate(dayPercentage).a);
        windowLightRenderer.color = new Color(lightColor.r, lightColor.g, lightColor.b, opacityGradient.Evaluate(dayPercentage).a);
        windowPaneRenderer.color = lightColor;
    }

    //Used for testing purposes. Should be deleted later
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightControl))
        {
            Debug.Log("Articifically added 1 unit of time");
            AddUsedTime(1);
		}
	}
}
