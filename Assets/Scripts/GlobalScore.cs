using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{

    // help from https://www.sitepoint.com/saving-data-between-scenes-in-unity/

    public static GlobalScore Instance;
    public float weightHighScore, balanceHighScore; // yoga exercises
    public float weightRecentScore, balanceRecentScore; //yoga exercises
    public int blasterRecentScore, blasterHighScore, blasterLevel; // Blaster minigame
    public int activity;

    public BlasterTutorialState blasterTutorialState;

    public enum BlasterTutorialState
    {
        NotStarted,
        Incomplete,
        Complete
    }


    void Awake ()
       {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
        blasterLevel = 1;
      }
    
    public void UpdateYogaScore(float recent, int activityNum) {
        // Activity 1 = balance, Activity 2 = weightlifting
        activity = activityNum;
        if (activityNum == 1) {
            balanceRecentScore = recent;
            if (recent >= balanceHighScore) {
                balanceHighScore = recent;
            }
        } else if (activityNum == 2) {
            weightRecentScore = recent;
            if (recent >= weightHighScore) {
                weightHighScore = recent;
            }
        }
    }

    public void updateBlasterScore(int recent) {
        blasterRecentScore = recent;
        if (recent >= blasterHighScore) {
            blasterHighScore = recent;
        }
        if (blasterHighScore >= 50) {
            blasterLevel = 3;
        } else if (blasterHighScore >= 30) {
            blasterLevel = 2;
        }
    }

    public void UpdateBlasterTutorial(bool completed) {
        //blasterTutorial starts at 0
        // = 1 after completing initial tutorial, =2 after completing leveled tutorial
        if(!completed) {
            blasterTutorialState = BlasterTutorialState.Incomplete;
        } else
        {
            blasterTutorialState = BlasterTutorialState.Complete;
        }
    }
}
