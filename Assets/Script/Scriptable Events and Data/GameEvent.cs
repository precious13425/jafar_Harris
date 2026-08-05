using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    
     event System.Action OnEventRaised;

    public void OnRaise()=>OnEventRaised?.Invoke();

    public void Register(System.Action myevent)=>OnEventRaised+=myevent;
    public void UnRegister(System.Action myevent)=>OnEventRaised-=myevent;

}
