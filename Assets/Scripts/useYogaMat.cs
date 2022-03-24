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
        interactionHandler.RegisterObject("Yoga Mat", openYogaMat, 1);
    }

    void openYogaMat() {
        highlightObject.setTriggerFalse();
        SceneManager.LoadScene("Yoga_menu", LoadSceneMode.Additive);
        RoomRenderer.Instance.HideRoom();
    }
}
