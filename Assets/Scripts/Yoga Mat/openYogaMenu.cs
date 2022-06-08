using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openYogaMenu : MonoBehaviour
{
    public void openYogaMatMenu() {
        SceneLoader.Instance.LoadAsync("Yoga_menu", LoadSceneMode.Additive, onLoadedCallback: () =>
        {
            SceneManager.UnloadSceneAsync("Yoga_gameOver");
        });
    }
}
