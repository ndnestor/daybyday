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
        
        StartCoroutine(Wait(scrollDelay, () => { autoScroll.isScrolling = true; }));
    }

    private void Update() {
        if (autoScroll.GetValue() == 0) {
            StartCoroutine(Wait(scrollDelay, () =>
            {
                SceneLoader.Instance.LoadAsync("Main Menu", LoadSceneMode.Single);
            }));
        }
    }

    private static IEnumerator Wait(float time, Action callback = null) {
        yield return new WaitForSeconds(time);
        
        callback?.Invoke();
    }
}
