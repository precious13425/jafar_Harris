using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameDirector:Jf_Singleton<GameDirector>{
   
     #region  gamestate
    public GameSate gameSate;
   public bool isactive=true; 

    public enum  GameSate
    {
        calm,excalation,climax,boss,finished
    }

   [SerializeField]Jf_Utils.Jf_timer timer;

    public UnityEvent Calm,Excalating,Climax,End,GameFinished;
    public event Action onGameFinished;

      [SerializeField]DialoguePanel taskUi;

    #endregion

    public float maxkillcount;
    float counter;

    public void SetKillCount(float val)
    {
        maxkillcount=val;
        UpdateUI();
    }


    void Start()
    {
        NewRun();
        _EnemyManager.OnEnemyKilled+=HandleTask;
        counter=0;
    }

    private void HandleTask(int obj)
    {
        counter++;
        if (counter >= maxkillcount)
        {
            counter=0;
            CallNextState();
        }
        UpdateUI();
    }

    private void CallNextState()
    {
        var nextstate=gameSate;

       switch (gameSate)
        {
            case GameSate.calm:
               nextstate=GameSate.excalation;
               Excalating?.Invoke();
            break;

            case GameSate.excalation:
               nextstate=GameSate.climax;
               Climax?.Invoke();
            break;

             case GameSate.climax:
               nextstate=GameSate.boss;
               End?.Invoke();
            break;

            case GameSate.boss:
            nextstate=GameSate.finished;
            KilledBossEnemy();
            break;
        }
        gameSate=nextstate;
        OnStateChange?.Invoke(gameSate);
    }


    public System.Action<GameSate>OnStateChange;

    
    private void UpdateUI()
    {
       taskUi.SetDetail($"{counter}/{maxkillcount}");
    }

 
    void Update()
    {
        if(!isactive)
        return;
        
        switch (gameSate)
        {
            case GameSate.calm:
                if (timer.UpdateTimer_bool(Time.deltaTime))
                {
                   CallNextState();
                   
                   
                }
            break;
            
        }
    }

    internal void KilledBossEnemy()
    {
     Invoke(nameof(CallEndGame),1);
    }

    internal void CallEndGame()
    {
       GameFinished?.Invoke();
       onGameFinished?.Invoke();
    }

    public void NewRun()
    {
        Calm?.Invoke();
        gameSate=GameSate.calm;
        Gamdata_SO.isaggro=true;
        timer=new Jf_Utils.Jf_timer(10);


    }

    void OnDestroy()
    {
        _EnemyManager.OnEnemyKilled-=HandleTask;

    }

}