using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomRenderer : MonoBehaviour
{
    [SerializeField] private string roomSceneName;

    public static RoomRenderer Instance;

    private List<GameObject> hiddenGameObjects = new List<GameObject>();
    private bool isHidden;

    private void Awake()
    {
        Instance = this;
    }

    private void ShowRoom()
    {
        foreach(GameObject go in hiddenGameObjects)
            go.SetActive(true);
        
        hiddenGameObjects.Clear();
        
        isHidden = false;
    }

    public void HideRoom()
    {
        isHidden = true;

        foreach(GameObject go in FindObjectsOfType<GameObject>())
        {
            // ReSharper disable once StringLiteralTypo
            if(go.scene.name == "DontDestroyOnLoad" || go == gameObject)
                continue;
            if(!go.activeInHierarchy)
                continue;
            
            go.SetActive(false);
            hiddenGameObjects.Add(go);
        }
    }

    private void Update()
    {
        
        // This is a poor way to do this but given the time constraint
        // this is just how it has to be lol
        if(!isHidden) return;
        if(SceneManager.sceneCount == 1 &&
           SceneManager.GetSceneAt(0).name == roomSceneName)
            ShowRoom();
    }

}
