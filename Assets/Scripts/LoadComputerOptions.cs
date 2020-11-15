using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadComputerOptions : MonoBehaviour
{
    public void LoadHomework() 
    {
        SceneManager.LoadScene("ExampleHW");
    }

    public void LoadBokkiBlaster() 
    {
        SceneManager.LoadScene("ExampleGame");
    }
}
