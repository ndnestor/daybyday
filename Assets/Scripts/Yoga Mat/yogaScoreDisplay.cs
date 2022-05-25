using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class yogaScoreDisplay : MonoBehaviour
{
    globalScore globalScoreKeeper;
    public float recent, high;
    int activityNum;
    public TextMeshProUGUI highScoreText, recentScoreText;
    void Start()
    {
        globalScoreKeeper = GameObject.Find("globalScoreObj").GetComponent<globalScore>();
        activityNum = globalScoreKeeper.returnActivityNum();
        if (activityNum == 1) {
            recent = globalScoreKeeper.returnBalanceRecent();
            high = globalScoreKeeper.returnBalanceHigh();
        }
        if (activityNum == 2) {
            recent = globalScoreKeeper.returnWeightRecent();
            high = globalScoreKeeper.returnWeightHigh();
        }
        highScoreText.text = "High Score: " + high;
        recentScoreText.text = "Recent Score: " + recent;
    }
}
