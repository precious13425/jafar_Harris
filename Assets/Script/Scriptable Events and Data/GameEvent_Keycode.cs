using UnityEngine;

[CreateAssetMenu]
public class GameEvent_Keycode : ScriptableObject
{
    
     event System.Action<KeyCode> OnEventRaised;


    public void OnRaise(KeyCode gy)=>OnEventRaised?.Invoke(gy);

    public void Register(System.Action<KeyCode> myevent)=>OnEventRaised+=myevent;
    public void UnRegister(System.Action<KeyCode> myevent)=>OnEventRaised-=myevent;

}
