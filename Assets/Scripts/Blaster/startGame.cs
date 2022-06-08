using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    GlobalScore scorekeeper;
    public void PlayGame()
    {
        scorekeeper = GlobalScore.Instance;
        if(scorekeeper.blasterTutorialState == GlobalScore.BlasterTutorialState.NotStarted)
        {
            SceneManager.LoadScene("Scene_Tutorial", LoadSceneMode.Additive);
        } else
        {
            SceneManager.LoadScene("Scene_Game", LoadSceneMode.Additive);
        }
    }
}
