using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLoopSwitcher : MonoBehaviour
{

    public AudioSource CalmAmbient;
    public AudioSource escalatingambience;
    public AudioSource climaxambience;


    void Start()
    {
        TaskManager.ins.OnStateChange+=HandleStateChange;
        DisableAll();
    }

    private void HandleStateChange(TaskManager.GameSate sate)
    {
        DisableAll();
        switch (sate)
        {
           case TaskManager.GameSate.calm:
           CalmAmbient.Play(); 
           break;
           case TaskManager.GameSate.excalation:
           escalatingambience.Play();
           break;
            case TaskManager.GameSate.climax:
           climaxambience.Play();
           break;
        }
    }

    private void DisableAll()
    {
       CalmAmbient.Stop();
       escalatingambience.Stop();
       climaxambience.Stop();

    }
}
