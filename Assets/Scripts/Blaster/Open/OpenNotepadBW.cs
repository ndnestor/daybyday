using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenNotepadBW : MonoBehaviour
{
    public GameObject NotepadBW;
    public Button notepadButton;

	void Start () {
		Button btn = notepadButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		 GameObject tmp = Instantiate(NotepadBW, new Vector3(4.25f, -0.43f, 0.0f), Quaternion.identity);
	}
}