using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using FluidMidi;
using System.IO;

public class Piano : MonoBehaviour //NOTE: THIS SCRIPT IS DEPRECATED
{

    /*[SerializeField] private float gain;
    [SerializeField] private int startingOctave;
    [SerializeField] private int endingOctave;

    private const string notesPath = "MIDI Notes";
    private readonly Hashtable keyBindings = new Hashtable();
    private Synthesizer synthesizer;
    private char[] keyList = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' };

    private void Start()
    {
        synthesizer = GetComponent<Synthesizer>();

        for(int i = startingOctave; i < endingOctave + 1; i++)
        {
            for(int n = 2; n < 9; n++)
            {
                SongPlayer songPlayer = gameObject.AddComponent<SongPlayer>();
                songPlayer.playOnStart = false;
                songPlayer.songPath = Path.Combine(Application.streamingAssetsPath, notesPath, (char)((n % 7) + 65) + (i + ".mid"));
                songPlayer.synthesizer = synthesizer;
                songPlayer.gain = gain;

                foreach(char key in keyList)
                {
                    if(!keyBindings.ContainsKey(key))
                    {
                        keyBindings.Add(key, songPlayer);
                        break;
					}
				}
			}
		}

	}

    private void Update()
    {

        foreach(char key in keyBindings.Keys)
        {
            SongPlayer currSongPlayer = (SongPlayer)keyBindings[key];
            if(Input.GetKeyDown(key.ToString()))
            {
                print("Playing " + currSongPlayer.songPath);
                currSongPlayer.enabled = true;
                currSongPlayer.Play();
			}
            else if(!Input.GetKey(key.ToString()))
            {
                currSongPlayer.Stop();
                currSongPlayer.enabled = false;
			}
        }

    }*/
}
