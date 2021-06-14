using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompProfile : MonoBehaviour
{
    public void LaunchBlaster()
    {
        SceneManager.LoadScene(0);
    }
    public void LaunchInbox()
    {
        SceneManager.LoadScene(3);
    }
}

