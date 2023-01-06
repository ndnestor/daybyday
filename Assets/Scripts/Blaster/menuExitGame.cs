using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuExitGame : MonoBehaviour
{
    public void ExitGame()
    {
        try
        {
            SceneManager.UnloadSceneAsync("Scene_startMenu");
        } catch
        {
            SceneManager.UnloadSceneAsync("Scene_endGame");
        }
    }
}
