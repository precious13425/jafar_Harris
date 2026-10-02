using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UrgentClickButton : MonoBehaviour
{
   [SerializeField] UnityEvent unityEventstart,DisableEvent;

    void OnEnable()
    {
        unityEventstart?.Invoke();
    }

    void OnDisable()
    {
        DisableEvent?.Invoke();
    }
}
