using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class EventExecutor : MonoBehaviour {

    public EventSO eventSO;

    public UnityEvent UnityEventExecute;
    private void Awake(){

        eventSO.Remove(OnEventExecute);
        eventSO.Add(OnEventExecute);


    }

    private void OnDestroy(){
        eventSO.Remove(OnEventExecute);
    }


    private void OnEventExecute(){
        UnityEventExecute.Invoke();
    }
}