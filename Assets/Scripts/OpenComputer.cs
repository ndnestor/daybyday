using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenComputer : MonoBehaviour
{

    private void Start()
    {
        InteractionHandler.Instance.RegisterObject("Computer", OpenComp);
    }

    private void OpenComp()
    {
        //SceneManager.LoadScene("CompAll", LoadSceneMode.Additive);
        //RoomRenderer.Instance.HideRoom();

        SceneLoader.Instance.LoadAsync("CompAll", LoadSceneMode.Additive, true);
    }
}
