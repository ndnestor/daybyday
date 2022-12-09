using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private bool autoPlay;
    [SerializeField] private bool loop;
    [Range(0, 1)] [SerializeField] private float maximumVolume;
    [Range(0, 1)] [SerializeField] private float defaultVolumePercentage;
    
    private Playlist currPlaylist;
    private AudioSource audioSource;
    private int nextSongIndex;
    private bool playing;
    
    public List<Playlist> playlists = new List<Playlist>();

    private void Start()
    {
        DontDestroyOnLoad(this);
    
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = defaultVolumePercentage * maximumVolume;

        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        if(autoPlay)
            Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        playlists.Sort((playlistA, playlistB) => playlistB.priority - playlistA.priority);
        
        foreach(var playlist in playlists)
            if (playlist.sceneName == scene.name) {
                currPlaylist = playlist;
                nextSongIndex = 0;
                NextSong();
                return;
            }
    }

    public void Play()
    {
        NextSong();
        playing = true;
    }
    
    private void NextSong()
    {
        audioSource.clip = currPlaylist.songs[nextSongIndex];
        audioSource.Play();

        nextSongIndex++;
        if(nextSongIndex == currPlaylist.songs.Count)
            nextSongIndex = 0;
    }

    private void Update()
    {
        if(!playing || audioSource.isPlaying) return;

        if(loop)
            NextSong();
        else
            Stop();
    }

    public void Stop()
    {
        playing = false;
        audioSource.Stop();
    }

    private void ChangeVolume(float volumePercentage)
    {
        audioSource.volume = volumePercentage * maximumVolume;
    }
}
