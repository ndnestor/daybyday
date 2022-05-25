using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class userNoteText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TMP_InputField inputField;
    public new RectTransform transform;
    public void Awake() {
        Hide();
        inputField.interactable = true;
    }
    public void Show() {
        text.text = inputField.text;
        Debug.Log(text.text);
        gameObject.SetActive(true);
    }
    void Update() {
        //transform.anchoredPosition = new Vector2(435.0f, -39f);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
