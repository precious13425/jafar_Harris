using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu]
public class Gamdata_SO : ScriptableObject
{

    


    public bool isPlay{get;private set;}
   

    public void PauseGame()
    {
        isPlay=false;
         Cursor.lockState=CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPlay=true;
        Cursor.lockState=CursorLockMode.Locked;

    }

    
}
