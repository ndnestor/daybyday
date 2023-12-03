using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour {
    [SerializeField] private AutoScroll autoScroll;
    [SerializeField] private float scrollDelay;
    
    private void Start() {
        autoScroll.isScrolling = false;
        autoScroll.SetValue(1);
        
        StartCoroutine(Wait(scrollDelay));
    }

    private void Update() {
        if (autoScroll.GetValue() == 0) {
            SceneLoader.Instance.LoadAsync("MainMenu", LoadSceneMode.Single);
        }
    }

    private IEnumerator Wait(float time) {
        yield return new WaitForSeconds(time);

        autoScroll.isScrolling = true;
    }
}
