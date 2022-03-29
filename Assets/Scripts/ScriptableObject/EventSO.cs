using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EventSO", menuName = "SO/Event")]
public class EventSO : ScriptableObject
{

    public delegate void InvokeEventHandler();
    public InvokeEventHandler InvokeEvent;

    public void Invoke(){
        InvokeEvent?.Invoke();
    }

    public void Add(InvokeEventHandler invokeEventHandler){
        InvokeEvent += invokeEventHandler;
    }

    public void Remove(InvokeEventHandler invokeEventHandler){
        try{
            InvokeEvent -= invokeEventHandler;
        }catch(NullReferenceException e){

        }
    }
}
