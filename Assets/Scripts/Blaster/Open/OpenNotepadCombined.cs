using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenNotepadCombined : MonoBehaviour
{
    public GameObject NotepadColor;
    public GameObject NotepadBW;
    // Start is called before the first frame update
    /**public void Start()
    {
         GameObject tmp = Instantiate(NotepadColor, new Vector3(8.09f, -0.43f, 0.0f), Quaternion.identity);
    }**/
    public Button notepadButton;
    public int openNotebook;
    //public SpriteRenderer rendColor;
    //public SpriteRenderer rendBW;

	void Start () {
        int openNotebook = 1;
        Debug.Log(openNotebook);
		Button btn = notepadButton.GetComponent<Button>();
        btn.onClick.AddListener(openWhich);
        GameObject tmpColor = Instantiate(NotepadColor, new Vector3(3.8f, -0.27f, 0.0f), Quaternion.identity);
		GameObject tmpBW = Instantiate(NotepadBW, new Vector3(3.8f, -0.27f, 0.0f), Quaternion.identity);
	}
    /** void openWhich() {
        if (openNotebook%2 == 0) {
            TaskOnClick_Color();
        } else {
            TaskOnClick_BW();
        }
    } */
    void openWhich() {
        if(openNotebook%2 == 0) {
            //rendColor.sortingOrder = 1;
            //rendBW.sortingOrder = 0;
            //Debug.Log(rendColor.sortingOrder);
            openNotebook += 1;
        } else {
            //rendColor.sortingOrder = 0;
            //rendBW.sortingOrder = 1;
            //Debug.Log(rendColor.sortingOrder);
            openNotebook += 1;
        }
    }

	void TaskOnClick_BW(){
		GameObject tmpBW = Instantiate(NotepadBW, new Vector3(3.8f, -0.27f, 0.0f), Quaternion.identity);
        if (openNotebook > 2) {
            Destroy(NotepadColor);
        }
        openNotebook += 1; // Makes even
        Debug.Log(openNotebook);
        Debug.Log("Made BW");
	}
    void TaskOnClick_Color(){
		GameObject tmpColor = Instantiate(NotepadColor, new Vector3(3.8f, -0.27f, 0.0f), Quaternion.identity);
        if (openNotebook > 2) {
            Destroy(NotepadBW);
        }
        openNotebook += 1; // Makes odd
        Debug.Log(openNotebook);
        Debug.Log("Made Color");
	}

    public int returnOpen() {
        return openNotebook;
    }
}
