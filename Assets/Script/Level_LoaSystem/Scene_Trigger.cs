using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Trigger : MonoBehaviour
{

    //test
    [SerializeField]GameEvent gameEvent;
    [SerializeField]int fadeid;

    void Start()
    {
        gameEvent.Register(callScene0);
    }

    private void callScene0()
    {
        LoadSCeneFade(fadeid);
    }

    //direct loading without a load manager present
    public void LoadSceneDirect(int id)
    {
        SceneManager.LoadScene(id);
    }

     public void LoadSceneDirect(string id)
    {
        SceneManager.LoadScene(id);
    }


// loading properly with fade
   public void LoadSCeneFade(int id)
    {
        LevelLoad_System.FadeInScene(id);
    }
    public void LoadSCeneFade(string id)
    {
        LevelLoad_System.FadeInScene(id);
    }


    public void QuitGame()=>Application.Quit();
}
