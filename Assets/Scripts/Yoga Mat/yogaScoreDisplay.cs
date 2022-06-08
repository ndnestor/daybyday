using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class yogaScoreDisplay : MonoBehaviour
{
    GlobalScore globalScoreKeeper;
    public float recent, high;
    int activityNum;
    public TextMeshProUGUI highScoreText, recentScoreText;
    void Start()
    {
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<GlobalScore>();
        activityNum = globalScoreKeeper.activity;
        if (activityNum == 1) {
            recent = globalScoreKeeper.balanceRecentScore;
            high = globalScoreKeeper.balanceHighScore;
        }
        if (activityNum == 2) {
            recent = globalScoreKeeper.weightRecentScore;
            high = globalScoreKeeper.weightHighScore;
        }
        highScoreText.text = "High Score: " + high;
        recentScoreText.text = "Recent Score: " + recent;
    }
}
