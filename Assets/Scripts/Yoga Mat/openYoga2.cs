using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openYoga2 : MonoBehaviour
{
    public void openExerciseTwo() {
        SceneLoader.Instance.LoadAsync("Yoga_ex2", LoadSceneMode.Additive, onLoadedCallback: () =>
        {
            SceneManager.UnloadSceneAsync("Yoga_menu");
        });
    }

}
