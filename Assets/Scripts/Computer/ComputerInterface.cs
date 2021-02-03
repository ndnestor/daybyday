using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInterface : MonoBehaviour
{
    public GameObject mainRoom;
    public GameObject computerScreen;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitComputer();
        }
    }

    private void QuitComputer()
    {
        mainRoom.SetActive(true);
        computerScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
