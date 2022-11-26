using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance
    {
        get
        {
            if(instance == null)
                instance = new SceneLoader();
            return instance;
        }
        private set => instance = value;
    }

    private static SceneLoader instance;

    [SerializeField] private float overlayFadeTimeStep;
    [SerializeField] private Image overlayImage;
    [SerializeField] private AudioListener audioListener;

    private bool isBusy;
    
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadAsync(string sceneName, LoadSceneMode loadSceneMode, bool hideRoom = false, Action onFinishCallback = null, Action onLoadedCallback = null)
    {
        if(isBusy) return;
        isBusy = true;
        Movement2D.Instance.enabled = false;
        // TODO: Perhaps use events / delegates instead of callbacks
        StartCoroutine(ChangeOverlayColor(Color.black, 1, () =>
        {
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            sceneLoadOperation.completed += operation =>
            {
                AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
                foreach(AudioListener listener in audioListeners)
                    if(listener != audioListener)
                        Destroy(listener);

                onLoadedCallback?.Invoke();
                if(hideRoom)
                    RoomRenderer.Instance.HideRoom(sceneName);
                StartCoroutine(ChangeOverlayColor(Color.clear, 1, () =>
                {
                    isBusy = false;
                    Movement2D.Instance.enabled = true;
                    onFinishCallback?.Invoke();
                }));
            };
        }));
    }

    public void UnloadAsync(string sceneName)
    {
        if(isBusy) return;
        isBusy = true;
        StartCoroutine(ChangeOverlayColor(Color.black, 1, () =>
        {
            AsyncOperation sceneUnloadOperation = SceneManager.UnloadSceneAsync(sceneName);
            sceneUnloadOperation.completed += operation =>
            {
                StartCoroutine(ChangeOverlayColor(Color.clear, 1, () => { isBusy = false; }));
            };
        }));
    }

    public IEnumerator ChangeOverlayColor(Color targetColor, float overlayFadeTime, Action callback = null)
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
