using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Playlist", menuName = "Music/Playlist")]
public class Playlist : ScriptableObject {
    public string sceneName;
    public int priority;
    public List<AudioClip> songs;
}
