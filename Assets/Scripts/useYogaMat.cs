using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class useYogaMat : MonoBehaviour
{
    public InteractionHandler interactionHandler;
    public HighlightObject highlightObject;
    void Start()
    {
        interactionHandler.RegisterObject("Yoga Mat", openYogaMat);
    }

    void openYogaMat() {
        SceneManager.LoadScene("Yoga_menu");
    }
    
    void Update()
    {
        // Uses same conditions as highlight to call Interact(), runs waterBonsai()
        if (Input.GetKey("e") && highlightObject.triggerable) {
            interactionHandler.Interact("Yoga Mat");
        }
    }
}
