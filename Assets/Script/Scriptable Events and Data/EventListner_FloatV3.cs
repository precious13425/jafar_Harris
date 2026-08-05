using UnityEngine;
using UnityEngine.Events;

public class EventListner_FloatV3 : MonoBehaviour
{
    //spawns a prefab at the designated position
    public Strut_GameEvent gameEvent;
    public UnityEvent  OnRaised;
    public floatV3 dataval{get; private set;}

    
    void OnEnable()
    {
       gameEvent.Register(RaiseMyEvent);
    }

    private void RaiseMyEvent(floatV3 v)
    {
        dataval=v;
       OnRaised?.Invoke();
    }

  
}
