using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openYoga2 : MonoBehaviour
{
    public void openExerciseTwo() {
        SceneManager.UnloadSceneAsync("Yoga_menu");
        SceneManager.LoadScene("Yoga_ex2", LoadSceneMode.Additive);
    }

}
