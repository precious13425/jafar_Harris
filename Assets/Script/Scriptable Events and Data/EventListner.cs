using System;
using UnityEngine;
using UnityEngine.Events;

public class EventListner : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent OnRaised;

    void OnEnable()
    {
        gameEvent.Register(RaiseMyEvent);
    }

    private void RaiseMyEvent()=>OnRaised?.Invoke();
  
}
