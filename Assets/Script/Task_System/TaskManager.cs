using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskManager : Jf_Singleton<TaskManager>
{

   public System.Action OnUpdateTask;
    Gamdata_SO gamedata_SO;
   //[SerializeField]DialoguePanel taskUi;
   public float enemyKillcount;

    IEnumerator Start()
    {
        //_taskSystem=new taskSystem();
       // _taskSystem.Setuptask(starttask.unitid,excalationmax-1);
        _EnemyManager.OnEnemyKilled+=Checktask;
        yield return new WaitForSeconds(1);
        //canupdate=true;
        UpdateUi();
        enemyKillcount=0;
        

    }

//check foe enemy killed
    public void Checktask(int id)
    {
      
      
        
        enemyKillcount++;
        OnUpdateTask?.Invoke();
       
    }

/*
    //a quest system prototype for killquest
    
    taskSystem   _taskSystem;
    [SerializeField]bool canupdate;
    [SerializeField]float waittime=5;
    IEnumerator nexttask()
    {

        gamedata_SO.Coin.AddValue(_taskSystem.GetReward());
        yield return new WaitForSeconds(waittime);

         _taskSystem.Setuptask(starttask.unitid,amount);
         canupdate=true;
         UpdateUi();
    }*/

    void UpdateUi()
    {
        // Debug.LogWarning("Enemy killed +"+taskSystem);
       // string detail_text=$"x {enemyKillcount}";
       // taskUi.SetDetail(detail_text);
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
