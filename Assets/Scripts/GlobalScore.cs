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
        
        blasterLevel = PersistentDataSaver.Instance.TryGet(nameof(blasterLevel), 1);
        blasterRecentScore = PersistentDataSaver.Instance.TryGet(nameof(blasterRecentScore), 0);
        blasterHighScore = PersistentDataSaver.Instance.TryGet(nameof(blasterHighScore), 0);

        weightRecentScore = PersistentDataSaver.Instance.TryGet(nameof(weightHighScore), 0);
        weightHighScore = PersistentDataSaver.Instance.TryGet(nameof(weightHighScore), 0);

        balanceRecentScore = PersistentDataSaver.Instance.TryGet(nameof(weightHighScore), 0);
        balanceHighScore = PersistentDataSaver.Instance.TryGet(nameof(balanceHighScore), 0);
    }
    
    public void UpdateYogaScore(float recent, int activityNum) {
        // Activity 1 = balance, Activity 2 = weightlifting
        activity = activityNum;
        if (activityNum == 1) {
            balanceRecentScore = recent;
            PersistentDataSaver.Instance.Set(nameof(balanceRecentScore), balanceRecentScore);
            if (recent >= balanceHighScore) {
                balanceHighScore = recent;
                PersistentDataSaver.Instance.Set(nameof(balanceHighScore), balanceHighScore);
            }
        } else if (activityNum == 2) {
            weightRecentScore = recent;
            PersistentDataSaver.Instance.Set(nameof(weightRecentScore), weightRecentScore);
            if (recent >= weightHighScore) {
                weightHighScore = recent;
                PersistentDataSaver.Instance.Set(nameof(weightHighScore), weightHighScore);
            }
        }
    }

    public void UpdateBlasterScore(int recent) {
        blasterRecentScore = recent;
        PersistentDataSaver.Instance.Set(nameof(blasterRecentScore), blasterRecentScore);
        if (recent >= blasterHighScore) {
            blasterHighScore = recent;
            PersistentDataSaver.Instance.Set(nameof(blasterHighScore), blasterHighScore);
        }
        if (blasterHighScore >= 50) {
            blasterLevel = 3;
        } else if (blasterHighScore >= 30) {
            blasterLevel = 2;
        }
        PersistentDataSaver.Instance.Set(nameof(blasterLevel), blasterLevel);
    }

    public void UpdateBlasterTutorial(bool completed) {
        if(!completed)
            blasterTutorialState = BlasterTutorialState.Incomplete;
        else
            blasterTutorialState = BlasterTutorialState.Complete;
    }
}
