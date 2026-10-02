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
        DisableAll();
        GameDirector.ins.OnStateChange+=HandleStateChange;
        HandleStateChange(GameDirector.GameSate.calm);
    }

    private void HandleStateChange(GameDirector.GameSate sate)
    {
        DisableAll();
        switch (sate)
        {
           case GameDirector.GameSate.calm:
           CalmAmbient.Play(); 
           break;
           case GameDirector.GameSate.excalation:
            case GameDirector.GameSate.climax:
           escalatingambience.Play();
           break;
           case GameDirector.GameSate.boss:
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
