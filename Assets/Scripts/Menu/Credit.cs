using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private string nameValue;
    [SerializeField] private string titlesValue;

    [SerializeField] private TMP_Text nameTextField;
    [SerializeField] private TMP_Text titlesTextField;

    private void Start()
    {
        nameTextField.text = nameValue;
        titlesTextField.text = titlesValue;
    }
}
