﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ComputerInterface : MonoBehaviour
{
    [SerializeField] private AudioClip blasterThemeSong;
    
    public GameObject profilePage;
    public GameObject inboxPage;
    public GameObject assignmentPage;
    //public GameObject libraryPage;
    //public GameObject blasterPage;
    public GameObject helpPage;
    //public GameObject settingsPage;

    public GameObject currentPage;

    // Canvas objects for Assignments page
    //public GameObject assmtButton1, assmtButton2, userTypeBox, notesButton;
    // Canvas objects for Inbox page
    public GameObject inboxAssets;
    public GameObject assignmentAssets;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitComputer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    public void QuitComputer()
    {
        SceneLoader.Instance.UnloadAsync("Computer");
        
        Tracking.Instance.QueueRoomTheme();
        MusicPlayer.Instance.StopMusic();
    }

    void CastRay()
    {
        //Casts a ray and calls the ChangePage() method

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
        if (hit)
        {
            currentPage = ChangePage(hit.collider.gameObject.name);
        }
    }

    private GameObject ChangePage(string newName)
    {
        GameObject newPage = null;

        if (newName.Equals("Profile Button"))
        {
            hideAllTabs();
            profilePage.SetActive(true);
        }
        else if (newName.Equals("Inbox Button"))
        {
            hideAllTabs();
            inboxPage.SetActive(true);
            inboxAssets.SetActive(true);
        }
        else if (newName.Equals("Assignments Button"))
        {
            hideAllTabs();
            assignmentPage.SetActive(true);
            //assmtButton1.SetActive(true);
            //assmtButton2.SetActive(true);
            //userTypeBox.SetActive(true);
            //notesButton.SetActive(true);
            assignmentAssets.SetActive(true);
        }
        else if (newName.Equals("Blaster Button"))
        {
            MusicPlayer.Instance.QueueMusic(blasterThemeSong, true);
            MusicPlayer.Instance.StopMusic();
            
            Tracking.Instance.AddUsedTime(2);
            SceneLoader.Instance.LoadAsync("Scene_Game", LoadSceneMode.Additive, onLoadedCallback: () => {
                SceneManager.UnloadSceneAsync("Computer");
                print("UNLOADING COMPUTER");
            });
        }
        else if (newName.Equals("Help Button"))
        {
            hideAllTabs();
            helpPage.SetActive(true);
        }
        currentPage = assignmentPage;

        return newPage;
    }

    private void hideAllTabs()
    {
        profilePage.SetActive(false);
        inboxPage.SetActive(false);
            inboxAssets.SetActive(false);
        assignmentPage.SetActive(false);
            //assmtButton1.SetActive(false);
            //assmtButton2.SetActive(false);
            //userTypeBox.SetActive(false);
            //notesButton.SetActive(false);
            assignmentAssets.SetActive(false);
        //libraryPage.SetActive(false);
        //blasterPage.SetActive(false); 
        helpPage.SetActive(false);
        //settingsPage.SetActive(false);
    }
}
