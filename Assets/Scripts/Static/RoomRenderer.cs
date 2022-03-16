using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomRenderer : MonoBehaviour
{
    [SerializeField] private Camera roomCamera;
    [SerializeField] private AudioListener roomAudioListener;

    [HideInInspector] public static RoomRenderer Instance;

    private const int MainRoomLayer = 5;
    private bool isHidden;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(!isHidden && SceneManager.sceneCount > 1)
        {
            HideRoom();
        } else if(isHidden && SceneManager.sceneCount == 1)
        {
            ShowRoom();
        }
        /*if(!isHidden && Camera.allCamerasCount > 2)
        {
            foreach(Camera camera in Camera.allCameras)
            {
                Debug.Log(camera.name);
            }
            return;
            HideRoom();
        }
        if(isHidden && Camera.allCamerasCount == 2)
        {
            ShowRoom();
        }*/
    }

    public void ShowRoom()
    {
        //Destroy(Camera.main);
        roomCamera.enabled = true;
        roomAudioListener.enabled = true;
        isHidden = false;
    }

    public void HideRoom()
    {
        roomCamera.enabled = false;
        roomAudioListener.enabled = false;
        Camera.main.cullingMask = 1 << MainRoomLayer;
        isHidden = true;
    }

}
