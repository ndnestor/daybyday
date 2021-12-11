using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class tutorialGuide : MonoBehaviour
{
    /*
    TUTORIAL is called twice:
    (1) Before playing Blaster for the first time
    (2) When leveling up --> alternatively, could split into INTERMED and HARD~~
    
    (1) FIRST PLAY
        - Player movement
        - Firing
        - Introduce UI — health bar, platform health, inventory, score
        - Introduce enemies - not too much detail, leave something for player to figure out
            - Maybe just a popup with names? Or slideshow sort of thing
        - Introduce pickups
            - Same as enemies - popup/slideshow
            - Don't explain use but put names - will slightly hint
    (2) LEVELING
        - Leveling affects: spawn rate, drop rate, enemy damage, certain enemy behaviours
        — Don't get too into specifics over what rates and changes are
        - If sprites are updated, make player aware of this

    NOTES TO SELF, DELETE LATER:
    - This script is the brain of the tutorial scene. It'll probably need access and control of
    every asset present. This means we can pause for explanations, temporal control, etc.
    
    */

    //UI pop-ups
    [SerializeField] GameObject keybinds, arrow, nextButton;
    [SerializeField] TextMeshProUGUI tutorialText;
    globalScore scorekeeper;
    int arrowNumber;
    bool waiting;

    /**[System.Serializable]
    public struct TutorialArrowLocations{
        public Vector3 point_Score;
        public Vector3 point_Inventory;
        public Vector3 point_PlayerHealth;
        public Vector3 point_PlatformHealth;
    } On second thought I don't think structs are serializable?
    Find a less ugly way to deal with this later
    **/
    [SerializeField] Vector3 point_Score, point_Inventory, point_PlayerHealth, point_PlatformHealth;
    void Start() {
        scorekeeper = globalScore.Instance;
        waiting = false;
        if(scorekeeper.returnBlasterTutorial() == 0) {
            // First play-through tutorial covers, in order:
            // Movement, firing, UI, enemies and pickups (slide)
            scorekeeper.updateBlasterTutorial(1);
            movementGuide();
        }
    }
    void Update() {
        if (waiting) {
            StartCoroutine(waitForNextButton());
        }
    }

    IEnumerator waitForNextButton() {
        while(waiting) {
            yield return null;
        }
    }

    void nextButtonClicked() {
        waiting = false;
    }

    void movementGuide() {
        tutorialText.text = "Welcome to the Blaster tutorial!\nUse the arrow in the bottom right to continue.";
        keybinds.SetActive(true); //Assume keybinds self-animated indep of this script for now
        tutorialText.text = "These are your movement keybinds.\nUse A to move left and D to move right.";
    }

    /**void Update() {
        goToNext ? nextSlide();
    }
    void nextSlide() {
        goToNext = false;
    }
    void waitNextSlide() {
        if(goToNext)
    }*/

    //NOTE TO SELF: Until continue arrow implemented this is just going to be one big mess

    void UIGuide() {
        //arrow.transform.position = TutorialArrowLocations.point_Score;
        tutorialText.text = "This number represents your score. It will update for each enemy you defeat.";
        waiting = true;
        /**arrow.transform.position = TutorialArrowLocations.point_Inventory;
        tutorialText.text = "This is your inventory. It will show which power-ups are currently active.";
        waiting = true;
        arrow.transform.position = TutorialArrowLocations.point_PlayerHealth;
        tutorialText.text = "This is your health bar. It will decrease each time you are hit.";
        waiting = true;
        tutorialText.text = "You can replenish your health bar with healing power-ups.";
        waiting = true;
        arrow.transform.position = TutorialArrowLocations.point_PlatformHealth;
        tutorialText.text = "This is your platform's health bar. It will decrease when the platform is hit.";
        waiting = true;
        tutorialText.text = "Your platform's health bar can NOT be replenished.";
        waiting = true;*/
    }

}
