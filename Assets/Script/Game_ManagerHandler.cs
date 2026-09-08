using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_ManagerHandler : MonoBehaviour
{
   public Player_SO player_SO;
   public Gamdata_SO gamdata_SO;
   public GameEvent RestartGame;

    public static Game_ManagerHandler ins;


   
     void Awake()
    {
        if(ins!=null && ins != this)
        {
            Destroy(this);
            return;
        }
        ins=this;
       
    }

    void Start()
    {
        if(RestartGame)
        RestartGame.Register(Restart);
    }




    private void Restart()
    {
        LevelLoad_System.FadeInScene(SceneManager.GetActiveScene().buildIndex);
    }


     public void QuitGame()
    {
        LevelLoad_System.FadeInScene(0);
    }


}
