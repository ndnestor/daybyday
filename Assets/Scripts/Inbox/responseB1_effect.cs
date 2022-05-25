using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
The goal of these button effect programs (responseB1_effect.cs, responseB2_effect.cs,
and responseB3_effect.cs) is ONLY to indicate which button has been pressed.

The person that the player communicates with and the updated status of their
conversation will be determined in the main inbox program (inboxMessage.cs).

When the button is pressed, the method onSend() will run, communicating with the
responseSent() method in inboxMessage.cs which number button has been pressed.
*/

public class responseB1_effect : MonoBehaviour
{
    public inboxMessage messageLoader;
    public int responseB1 = 1;

    public void onSend() {
        messageLoader.responseSent(responseB1);
    }
}
