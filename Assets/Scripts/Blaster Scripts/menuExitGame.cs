using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuExitGame : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.LoadScene("Scenes/Main Room");
    }
}
