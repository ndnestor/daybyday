using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inboxNewNotif : MonoBehaviour
{
    public GameObject notifMom, notifGrandma, notifProf, notifBuddy, notifBrie, notifMatt;
    public GameObject repsonseButtonsUI, responseButtonsSprite;
    public bool newMom, newGrandma, newProf, newBuddy, newBrie, newMatt;
    public bool newMessage;
    
    // For testing at this point, runs on start
    // Later, mod to run at day's beginning, bools private
    public void Start() {
        setNotifs();
    }
    public void setNotifs() {
        newMessage = false;
        notifMom.SetActive(false);
        notifGrandma.SetActive(false);
        notifProf.SetActive(false);
        notifBuddy.SetActive(false);
        notifBrie.SetActive(false);
        notifMatt.SetActive(false);
        repsonseButtonsUI.SetActive(false);
        responseButtonsSprite.SetActive(false);
        if (newMom == true) {
            notifMom.SetActive(true);
        }
        if (newGrandma == true) {
            notifGrandma.SetActive(true);
        }
        if (newProf == true) {
            notifProf.SetActive(true);
        }
        if (newBuddy == true) {
            notifBuddy.SetActive(true);
        }
        if (newBrie == true) {
            notifBrie.SetActive(true);
        }
        if (newMatt == true) {
            notifMatt.SetActive(true);
        }
        if (newMom || newGrandma || newProf || newBuddy || newBrie || newMatt) {
            newMessage = true;
        }
        if (newMessage == true) {
            repsonseButtonsUI.SetActive(true);
            responseButtonsSprite.SetActive(true);
        }
    }
}
