using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace NotePad{
    public class OpenNotepadCombined2 : MonoBehaviour
    {
        public GameObject notesBW;
        public GameObject notesCol;
        public userNoteText inputWindow;
        private GameObject tmpColor;
        private GameObject tmpBW;
        public int openNote;
        public int OpenNote {
             get{
                 //Debug.Log(openNote);
                 return openNote;
            }
            set {
                openNote = value;
            }
        }
        public Button notesButton;
        public void Start () {
            //Debug.Log("Start called");
            //openNote = 1;
            //Debug.Log(openNote);
            Button btn = notesButton.GetComponent<Button>();
            btn.onClick.AddListener(openNew);
            tmpColor = Instantiate(notesCol, new Vector3(10.5f, 2.1f, 0.0f), Quaternion.identity);
            tmpBW = Instantiate(notesBW, new Vector3(10.5f, 2.1f, 0.0f), Quaternion.identity);
            tmpColor.SetActive(false);
            tmpBW.SetActive(false);
        }
        public void openNew() {
            openNote += 1;
            //Debug.Log("New opened, openNote now " + openNote);
            if (openNote >= 1) {
                tmpColor.SetActive(true);
                tmpBW.SetActive(true);
            }
            if (openNote%2 == 0) {
                //inputWindow.Show("The fitness gram pacer test is a multistage aerobic");
                inputWindow.Show();
            } else {
                inputWindow.Hide();
            }
        }
        public void tempTest() {
            //Debug.Log("Temp test successful");
        }

    }
}
