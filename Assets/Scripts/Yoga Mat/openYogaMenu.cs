using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openYogaMenu : MonoBehaviour
{
    public void openYogaMatMenu() {
        SceneManager.LoadScene("Yoga_menu", LoadSceneMode.Additive);
    }
}
