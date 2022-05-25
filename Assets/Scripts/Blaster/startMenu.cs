using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public void openMenu()
    {
        SceneManager.LoadSceneAsync("Scene_startMenu", LoadSceneMode.Additive);
    }
}
