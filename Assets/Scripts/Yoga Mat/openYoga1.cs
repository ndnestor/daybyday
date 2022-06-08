using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openYoga1 : MonoBehaviour
{
    public void openExerciseOne() {
        SceneLoader.Instance.LoadAsync("Yoga_ex1", LoadSceneMode.Additive, onLoadedCallback: () =>
        {
            SceneManager.UnloadSceneAsync("Yoga_menu");
        });
    }

}
