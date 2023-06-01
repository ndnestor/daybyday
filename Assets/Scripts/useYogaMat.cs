using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class useYogaMat : MonoBehaviour
{
    [SerializeField] private AudioClip themeSong;
    
    public HighlightObject highlightObject;
    void Start()
    {
        InteractionHandler.Instance.RegisterObject("Yoga Mat", OpenYogaMat, 1);
    }

    void OpenYogaMat() {
        SceneLoader.Instance.LoadAsync("Yoga_menu", LoadSceneMode.Additive, true);

        MusicPlayer.Instance.StopMusic();
        MusicPlayer.Instance.QueueMusic(themeSong, true);
    }
}
