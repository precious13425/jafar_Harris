using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    #region variables
    taskSystem taskSystem;
   [SerializeField] Gamdata_SO gamedata_SO;
    public Enemy_SO starttask;
    public float amount=4;
    public System.Action OnChange;

    public DialoguePanel taskUi;
   [SerializeField] SPawnerHandler enemyspawner;


 #region  gamestate
    public GameSate gameSate;
    public enum  GameSate
    {
        calm,excalation,climax,end
    }

    public UnityEvent Calm,Excalating,Climax,End;

 private void ChangeGamestate(GameSate data)
    {
       gameSate=data;

       OnStateChange?.Invoke(gameSate);
       switch (gameSate)
        {
            case GameSate.calm:
               Calm?.Invoke();
               //timer.Reset_TImer();
            break;
            case GameSate.excalation:
            Excalating?.Invoke();
            break;

             case GameSate.climax:
            Climax?.Invoke();
            break;
            case GameSate.end:
            End?.Invoke();
            break;
        }
            
            
    }
#endregion


    public System.Action<GameSate>OnStateChange;



    public static TaskManager ins;

#endregion

     void Awake()
    {
        if(ins==null)
        {
        ins=this;
        return;
        }
        Destroy(this);
    }
    

    IEnumerator Start()
    {
        taskSystem=new taskSystem();
        taskSystem.Setuptask(starttask.unitid,excalationmax-1);
        _EnemyManager.OnEnemyKilled+=Checktask;
        canupdate=true;
        UpdateUi();
        timer=new Jf_Utils.Jf_timer(10,true);
        
        yield return new WaitForSeconds(1);
        ChangeGamestate(GameSate.calm);

    }

    public void Checktask(int id)
    {
        taskSystem.TryAdd(id);
        if (taskSystem.is_Taskcomplete())
        {
           // taskSystem.GiveReward();
          //  taskSystem.clearTask();
            //add new task in next 5 second
           if(canupdate)
           StartCoroutine(nexttask());

        }
        OnChange?.Invoke();
        UpdateUi();
        
        enemykillcount++;
       
    }

[SerializeField]Jf_Utils.Jf_timer timer;
public float enemykillcount=0;
public float excalationmax=10;
public float climaxmax=10;

    void Update()
    {
        switch (gameSate)
        {
            case GameSate.calm:
                if (timer.UpdateTimer_bool(Time.deltaTime))
                {
                    ChangeGamestate(GameSate.excalation);
                   
                   
                }
            break;
            case GameSate.excalation:
            bool done=enemykillcount>excalationmax;
                if (done)
                {
                    enemykillcount=0;
                    ChangeGamestate(GameSate.climax);
                }
            break;

             case GameSate.climax:
            bool isdone=enemykillcount>excalationmax;
                if (isdone)
                {
                    enemykillcount=0;
                    ChangeGamestate(GameSate.end);
                }
            break;
            
            
        }
    }

   

    [SerializeField]bool canupdate;
    [SerializeField]float waittime=5;
    IEnumerator nexttask()
    {

        gamedata_SO.Coin.AddValue(taskSystem.GetReward());
        yield return new WaitForSeconds(waittime);

         taskSystem.Setuptask(starttask.unitid,amount);
         canupdate=true;
         UpdateUi();
    }

    void UpdateUi()
    {
        // Debug.LogWarning("Enemy killed +"+taskSystem);
        string detail_text=$"{taskSystem.TaskDetail()}";
        taskUi.SetDetail(detail_text);
    }


    void OnDestroy()
    {
        _EnemyManager.OnEnemyKilled-=Checktask;

    }
}








[Serializable]
public class Ui_VisualHelper
{
    public CanvasGroup ParentCanvas;
    public Image Icon;
    public Slider slider;
    public TMP_Text detail_text;

    public void SetDetail(string d,float alpha_s=1)
    {
        detail_text.text=$"{d}";
        setalpha(alpha_s);
    }

    private void setalpha(float alpha_s)
    {
        ParentCanvas.alpha=alpha_s;
    }

    public void SetDetail(Sprite d,float alpha_s=1)
    {
        Icon.sprite=d;
        setalpha(alpha_s);
    }
    public void SetDetail(float slideval,float alpha_s=1)
    {
        slider.value=slideval;
        setalpha(alpha_s);
    }
}

public class taskSystem
{
    [SerializeField] task currenttask;

    public String TaskDetail()
    {
        string nd="";
        nd=$"{currenttask.count}/{currenttask.amount}";
        if (currenttask.iscompleted)
        {
            nd=$"Complete";
        }
        return nd;
    }
    public void Setuptask(int _id,float _amount)
    {
        currenttask=new task
        {
            id=_id,
            amount=_amount,
            count=0,iscompleted=false,
            coinreward=10
        };
    }

    public void TryAdd(int id,float amt = 1)
    {
        if (currenttask != null)
        {
            currenttask.Addtask(id,amt);
        }
    }

    public bool is_Taskcomplete()
    {
        if (currenttask != null)
        {
            return currenttask.iscompleted;
        }
        return false;
    }

    internal void clearTask()
    {
        currenttask=null;
    }

    internal float GetReward()
    {
        return currenttask.coinreward;
    }

    public class task
    {
       public int id;
       public float amount;
       public float count;
       public bool iscompleted;
       public float coinreward=10;

        public void Addtask(int id,float amt=1)
        {
            if (this.id == id)
            {
                count++;
            }

            if (count >= amount)
            {
                iscompleted=true;
            }
        }

       
    }
}
