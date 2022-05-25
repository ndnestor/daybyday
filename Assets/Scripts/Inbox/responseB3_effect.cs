using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class responseB3_effect : MonoBehaviour
{
    // See script responseB1_effect for brief overview on intention and use for
    // scripts responseB1_effect.cs, responseB2_effect.cs, and responseB3_effect.cs.
    
    public inboxMessage messageLoader;
    public int responseB3 = 3;

    public void onSend() {
        messageLoader.responseSent(responseB3);
    }
}
