using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomRenderer : MonoBehaviour
{
    [SerializeField] private Camera roomCamera;
    [SerializeField] private AudioListener roomAudioListener;

    [HideInInspector] public static RoomRenderer Instance;

    private bool isHidden;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowRoom()
    {
        roomCamera.enabled = true;
        roomAudioListener.enabled = true;
        isHidden = false;
    }

    public void HideRoom()
    {
        roomCamera.enabled = false;
        roomAudioListener.enabled = false;
        isHidden = true;
    }

}
