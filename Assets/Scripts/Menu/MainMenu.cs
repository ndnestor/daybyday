using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Main Room");
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
