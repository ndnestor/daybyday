using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewDay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Tracker.currDay == 1 && Tracker.currTimeUnit >= 6) {
            Debug.Log("NEXT DAY");
            LoadNextDay();
        }
    }

    public void LoadNextDay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
