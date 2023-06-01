using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenComputer : MonoBehaviour
{
    [SerializeField] private AudioClip themeSong;

    private void Start()
    {
        InteractionHandler.Instance.RegisterObject("Computer", OpenComp);
    }

    private void OpenComp()
    {
        SceneLoader.Instance.LoadAsync("Computer", LoadSceneMode.Additive, true);
        
        MusicPlayer.Instance.StopMusic();
        MusicPlayer.Instance.QueueMusic(themeSong, true);
    }
}
