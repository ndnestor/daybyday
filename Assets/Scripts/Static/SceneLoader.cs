using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private float overlayFadeTime;
    [SerializeField] private float overlayFadeTimeStep;
    [SerializeField] private Image overlayImage;
    
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadAsync(string sceneName, LoadSceneMode loadSceneMode, bool hideRoom = false, Action onFinishCallback = null, Action onLoadedCallback = null)
    {
        // TODO: Perhaps use events / delegates instead of callbacks
        StartCoroutine(ChangeOverlayColor(Color.black, () =>
        {
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            sceneLoadOperation.completed += operation =>
            {
                onLoadedCallback?.Invoke();
                if(hideRoom)
                    RoomRenderer.Instance.HideRoom(sceneName);
                StartCoroutine(ChangeOverlayColor(Color.clear, onFinishCallback));
            };
        }));
    }

    IEnumerator ChangeOverlayColor(Color targetColor, Action callback = null)
    {
        float timeSinceStart = 0;

        Color originalColor = overlayImage.color;
        while(overlayImage.color != targetColor)
        {
            overlayImage.color = Color.Lerp(originalColor, targetColor, timeSinceStart / overlayFadeTime);
            
            yield return new WaitForSeconds(overlayFadeTimeStep);
            
            timeSinceStart += overlayFadeTimeStep;
        }
        
        callback?.Invoke();
    }
}
