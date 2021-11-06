using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalScore : MonoBehaviour
{

    // help from https://www.sitepoint.com/saving-data-between-scenes-in-unity/

    public static globalScore Instance;
    public float weightHighScore, balanceHighScore; // yoga exercises
    public float weightRecentScore, balanceRecentScore; //yoga exercises
    public int blasterRecentScore, blasterHighScore, blasterLevel; // Blaster minigame
    public int activity;

    void Awake ()
       {
        if (Instance == null)
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
    
    public void updateYogaScore(float recent, int activityNum) {
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

    public int returnActivityNum() {
        return activity;
    }
    
    public float returnBalanceHigh() {
        return balanceHighScore;
    }
    public float returnBalanceRecent() {
        return balanceRecentScore;
    }

    public float returnWeightHigh() {
        return weightHighScore;
    }
    public float returnWeightRecent() {
        return weightRecentScore;
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
}
