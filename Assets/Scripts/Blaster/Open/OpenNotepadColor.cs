using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenNotepadColor : MonoBehaviour
{
    public GameObject NotepadColor;
    // Start is called before the first frame update
    /**public void Start()
    {
         GameObject tmp = Instantiate(NotepadColor, new Vector3(8.09f, -0.43f, 0.0f), Quaternion.identity);
    }**/
    public Button notepadButton;

	void Start () {
		Button btn = notepadButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		GameObject tmp = Instantiate(NotepadColor, new Vector3(8.09f, -0.43f, 0.0f), Quaternion.identity);
	}
}
