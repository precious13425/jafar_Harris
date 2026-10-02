using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu]
public class Gamdata_SO : ScriptableObject
{

    public Pooltype Coinsaved;
    [SerializeField]Player_SO playerT;
    public static bool isaggro;

    public  bool isPlay{get;private set;}

    public System.Action OnBulletChange,OnCoinChange;
   
    public bool isnewgame;

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

    public void SetNewgtame(bool val)=>isnewgame=val;
    internal void newGameRun()
    {
      
    }

    internal void SaveCoin()
    {
      //Coinsaved.AddValue(runcoin);
    }

    internal void SpendCoin(int payCost)
    {
        Coinsaved.Remove_Value(payCost);
        OnCoinChange?.Invoke();

    }
}
