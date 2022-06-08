using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Game;
using Game.Dialogue;
using Game.Registry;

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
    [SerializeField] GameObject keybinds, arrow;
    [SerializeField] Button nextButton;
    [SerializeField] TextMeshProUGUI tutorialText;
    GlobalScore scorekeeper;
    [SerializeField] private DialogueGraph dialogueGraph;
    [SerializeField] private DialogueSystem dialogueSystem;
    private const string StringRegistryId = "Blaster Tutorial Dialogue";
    
    private StringRegistry stringRegistry;


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
    public void Start() {
        scorekeeper = GlobalScore.Instance;
        dialogueSystem = MainInstances.Get<DialogueSystem>();
        stringRegistry = MainInstances.Get<StringRegistry>();
        beginTutorial();
    }

    public void beginTutorial() {
        dialogueSystem.Present(dialogueGraph);
    }

}
