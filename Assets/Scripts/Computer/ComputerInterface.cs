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
        mainRoom.active = true;
        computerScreen.active = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
