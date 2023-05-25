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
    [SerializeField] private float loopDelay = 3.0f;

    public float fadeDuration = 1.0f;
    public AudioSource audioSource;

    private float currentVolume = 0.0f;
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
        StopMusic();
        var currentSceneName = SceneManager.GetActiveScene().name;
        foreach (var pair in sceneMusicMap)
        {
            if (pair.sceneName != currentSceneName) continue;
            if (pair.musicClip == audioSource.clip) continue;
            audioSource.clip = pair.musicClip;
            print($"Playing music for scene '{currentSceneName}'");
            PlayMusic(true);
            break;
        }
    }

    public void PlayMusic(bool loop)
    {
        if (audioSource.isPlaying)
            return;

        isLooping = loop;
        isFading = true;
        StartCoroutine(FadeIn());
    }

    public void StopMusic()
    {
        if (!audioSource.isPlaying)
            return;
        
        isFading = true;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0.0f;
        audioSource.Play();
        
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
        
        if (isLooping)
        {
            StartCoroutine(PlayMusicWithDelay());
        }
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
            audioSource.Play();
        }
    }
}
