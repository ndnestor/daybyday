using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]

public class AutoScroll : MonoBehaviour {
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float reactivateTime;
    
    public bool isScrolling;

    private float lastSetScrollbarValue;
    private float timeUntilReactivate;

    private void Start() {
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    private void OnScrollbarValueChanged(float newValue) {
        if (Math.Abs(newValue - lastSetScrollbarValue) > 0.005)
            timeUntilReactivate = reactivateTime;
    }
    
    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.mouseScrollDelta != Vector2.zero)
            timeUntilReactivate = reactivateTime;
        
        if (isScrolling && timeUntilReactivate <= 0) {
            SetValue(scrollbar.value + scrollSpeed * Time.deltaTime);
        } else {
            timeUntilReactivate -= Time.deltaTime;
        }
    }

    public void SetValue(float value) {
        lastSetScrollbarValue = Mathf.Clamp(value, 0, 1);
        scrollbar.value = lastSetScrollbarValue;
    }

    public float GetValue() {
        return lastSetScrollbarValue;
    }
}
