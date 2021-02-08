using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInterface : MonoBehaviour
{
    public GameObject mainRoom;
    public GameObject computerScreen;

    public GameObject profilePage;
    public GameObject inboxPage;
    public GameObject assignmentPage;
    public GameObject libraryPage;
    public GameObject blasterPage;
    public GameObject helpPage;
    public GameObject settingsPage;

    public GameObject currentPage;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitComputer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }
    }

    private void QuitComputer()
    {
        mainRoom.SetActive(true);
        computerScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CastRay()
    {
        //Casts a ray and calls the ChangePage() method

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
        if (hit)
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            currentPage = ChangePage(hit.collider.gameObject.name);
        }
    }

    private GameObject ChangePage(string newName)
    {
        GameObject newPage = null;

        if(newName.Equals("Assignments Button"))
        {
            assignmentPage.SetActive(true);
            helpPage.SetActive(false);

            currentPage = assignmentPage;
        }

        return newPage;
    }
}
