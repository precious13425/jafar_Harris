using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Strut_GameEvent : ScriptableObject
{
     event System.Action<floatV3> OnEventRaised;
     public float defaultvalue=10;
   
    public void OnRaise()=>OnEventRaised?.Invoke(new  floatV3{data_value=defaultvalue
    ,data_position=Vector3.zero});

    public void OnRaise(floatV3 data)=>OnEventRaised?.Invoke(data);

    public void Register(System.Action<floatV3> myevent)=>OnEventRaised+=myevent;
    public void UnRegister(System.Action<floatV3> myevent)=>OnEventRaised-=myevent;
}

public struct floatV3
{
   public float data_value;
   public Vector3 data_position;
}



