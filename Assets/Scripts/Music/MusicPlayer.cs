using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[Serializable]
public class SceneMusicPair
{
    public string sceneName;
    public AudioClip musicClip;
}

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<SceneMusicPair> sceneMusicMap = new List<SceneMusicPair>();
    [SerializeField] private float maxVolume = 1.0f;
    [SerializeField] [Range(0f, 100f)] private float progress;

    public AudioSource audioSource;
    public float fadeDuration = 1.0f;
    public float loopDelay = 3.0f;

    private float currentVolume = 0.0f;
    private Coroutine queueCoroutine;
    private Coroutine delayCoroutine;
    private bool isLooping = false;
    private bool isFading = false;

    public static MusicPlayer Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    private void Start()
    {
        // TODO: Consider that Start is only called once on this game object
        
        StopMusic();
        var currentSceneName = SceneManager.GetActiveScene().name;
        foreach (var pair in sceneMusicMap)
        {
            if (pair.sceneName != currentSceneName) continue;
            if (pair.musicClip == audioSource.clip) continue;
            audioSource.clip = pair.musicClip;
            PlayMusic(true);
            break;
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            progress = 0;
            return;
        }

        progress = audioSource.time / audioSource.clip.length * 100f;
    }

    public void PlayMusic(bool loop)
    {
        if (audioSource.isPlaying)
            return;
        
        print($"Playing {audioSource.clip.name}");
        
        isLooping = loop;

        isFading = true;
        StartCoroutine(FadeIn());
    }
    
    public void StopMusic()
    {
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
        }

        if (!audioSource.isPlaying)
            return;
        
        print($"Stopping {audioSource.clip.name}");

        isFading = true;
        StartCoroutine(FadeOut());
    }

    public void QueueMusic(AudioClip audioClip, bool loop)
    {
        if (queueCoroutine != null)
            StopCoroutine(queueCoroutine);

        print($"Queueing {audioClip.name}");
        queueCoroutine = StartCoroutine(QueueMusicCoroutine(audioClip, loop));
    }

    private IEnumerator QueueMusicCoroutine(AudioClip audioClip, bool loop)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
        }

        yield return new WaitForSeconds(loopDelay);

        queueCoroutine = null;

        audioSource.clip = audioClip;
        PlayMusic(loop);
    }

    private IEnumerator FadeIn()
    {
        float timer = 0.0f;
        audioSource.Play();
            
        if (isLooping)
        {
            delayCoroutine = StartCoroutine(PlayMusicWithDelay());
        }
        
        while (timer < fadeDuration)
        {
            currentVolume = Mathf.Lerp(0.0f, maxVolume, timer / fadeDuration);
            audioSource.volume = currentVolume;
            timer += Time.deltaTime;
            yield return null;
        }
        
        currentVolume = maxVolume;
        audioSource.volume = currentVolume;
        isFading = false;
    }

    private IEnumerator FadeOut()
    {
        float timer = 0.0f;
        float startVolume = audioSource.volume;
        
        while (timer < fadeDuration)
        {
            currentVolume = Mathf.Lerp(startVolume, 0.0f, timer / fadeDuration);
            audioSource.volume = currentVolume;
            timer += Time.deltaTime;
            yield return null;
        }
        
        audioSource.Stop();
        audioSource.volume = maxVolume;
        isFading = false;
    }

    private IEnumerator PlayMusicWithDelay()
    {
        yield return new WaitForSeconds(audioSource.clip.length + loopDelay);
        if (isLooping)
        {
            print("Playing again");
            QueueMusic(audioSource.clip, true);
        }
    }
}
