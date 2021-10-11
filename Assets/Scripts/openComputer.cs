using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openComputer : MonoBehaviour
{

    public InteractionHandler interactionHandler;
    public HighlightObject highlightObject;

    void Start()
    {
        interactionHandler.RegisterObject("Computer", openComp);
    }
    void Update()
    {
        if (Input.GetKey("e") && highlightObject.triggerable) {
            interactionHandler.Interact("Computer");
        }
    }
    void openComp() {
        highlightObject.setTriggerFalse();
        SceneManager.LoadScene("CompAll");
    }
}
