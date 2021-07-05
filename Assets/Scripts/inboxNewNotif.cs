using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inboxNewNotif : MonoBehaviour
{
    public GameObject notifMom, notifGrandma, notifProf, notifBuddy, notifBrie, notifMatt;
    public bool newMom, newGrandma, newProf, newBuddy, newBrie, newMatt;
    
    // For testing at this point, runs on start
    // Later, mod to run at day's beginning, bools private
    public void Start() {
        setNotifs();
    }
    public void setNotifs() {
        notifMom.SetActive(false);
        notifGrandma.SetActive(false);
        notifProf.SetActive(false);
        notifBuddy.SetActive(false);
        notifBrie.SetActive(false);
        notifMatt.SetActive(false);
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
    }
}
