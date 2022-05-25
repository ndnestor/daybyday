using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> playlist = new List<AudioClip>();
    [SerializeField] private bool autoPlay;
    [SerializeField] private bool loop;
    
    private AudioSource audioSource;
    private int nextSongIndex;
    private bool playing;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if(autoPlay)
            Play();
    }

    public void Play()
    {
        NextSong();
        playing = true;
    }
    
    private void NextSong()
    {
        audioSource.clip = playlist[nextSongIndex];
        audioSource.Play();

        nextSongIndex++;
        if(nextSongIndex == playlist.Count)
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
}
