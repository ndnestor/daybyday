using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidMidi;

public class Piano : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            print("Playing sound");
            GetComponent<SongPlayer>().enabled = true;
            GetComponent<SongPlayer>().Play();
        }
        else if(!Input.GetKey(KeyCode.J))
        {
            GetComponent<SongPlayer>().Stop();
            GetComponent<SongPlayer>().enabled = false;
        }
    }
}
