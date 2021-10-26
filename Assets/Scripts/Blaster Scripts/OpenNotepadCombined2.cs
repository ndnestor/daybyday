using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace NotePad{
    public class OpenNotepadCombined2 : MonoBehaviour
    {
        //public GameObject notesBW, notesCol;
        public userNoteText inputWindow;
        //private GameObject tmpColor, tmpBW;
        public int openNote;
        public GameObject tmpColor, tmpBW;
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
            Button btn = notesButton.GetComponent<Button>();
            btn.onClick.AddListener(openNew);
            //tmpColor = Instantiate(notesCol, new Vector3(11.6f, 3.28f, 0.0f), Quaternion.identity);
            //tmpBW = Instantiate(notesBW, new Vector3(11.6f, 3.28f, 0.0f), Quaternion.identity);
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
                inputWindow.Show();
            } else {
                inputWindow.Hide();
            }
        }
        /**public void tempTest() {
            Debug.Log("Temp test successful");
        }*/

    }
}
