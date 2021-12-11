using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    globalScore scorekeeper;
    public void PlayGame()
    {
        scorekeeper = globalScore.Instance;
        if (scorekeeper.returnBlasterTutorial() == 0) {
            SceneManager.LoadScene("Scene_Tutorial");
        } else {
            SceneManager.LoadScene("Scene_Game");
        }
    }
}
