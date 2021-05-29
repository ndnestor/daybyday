using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Dialogue;
using UnityEngine;


/// <summary>
/// Used to activate Dialogue System when testing. This should not be in the game.
/// </summary>
public class DialogueActivator : MonoBehaviour {

    public DialogueGraph Dialogue;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            MainInstances.Get<DialogueSystem>().Present(Dialogue);
        }
    }
}
