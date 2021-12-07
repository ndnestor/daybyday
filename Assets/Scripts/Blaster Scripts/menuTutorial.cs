using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuTutorial : MonoBehaviour
{
    public void PlayTutorial()
    {
        SceneManager.LoadScene("Scene_Tutorial");
    }
}
