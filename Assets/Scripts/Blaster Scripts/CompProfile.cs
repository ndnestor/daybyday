using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompProfile : MonoBehaviour
{
    public void LaunchBlaster()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }
    public void LaunchInbox()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
}

