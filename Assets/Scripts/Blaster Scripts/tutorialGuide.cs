using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
