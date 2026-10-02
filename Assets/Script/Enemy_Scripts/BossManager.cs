using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    
   #region  Variables
   public Enemy_SO enemy_SO;
   [SerializeField]Player_SO Player;
   [SerializeField]Transform myTarget;
    public bool attackready,performing_action;

   [Header("health Variables")]
   [SerializeField]bool isAiActive;
    [SerializeField]float healthval;
    public StatDamager_COmp mystats;



    [Header("Charge attack Variables")]
    public DashComponent dashattack;
    [SerializeField]float Damage_Amount=1,dis_toAttack=3;
   [SerializeField]float att_time=2;
   [SerializeField]Jf_Utils.Jf_timer decideTime;
   //JfCorutine jfCorutine=new JfCorutine();
    public float mylevel;


#endregion

   IEnumerator Start()
    {
      yield return null;

      isAiActive=true;

     //setup player attack power 
      Damage_Amount=enemy_SO?enemy_SO.Get_AttackValue(mylevel):Damage_Amount;

      decideTime=new Jf_Utils.Jf_timer(att_time);

   //setup target
      if (Player)
      {
         myTarget=Player.GetPlayer;
         
      }

      //setup attack hitbox

      //setuphealth
      mystats.OnValChange+=Handle_Damage;
      mystats.SetUp(healthval);

      dashattack.setup(DamageTarget);
      

    }

    private void DamageTarget(Transform transform)
    {
       Debug.Log(transform.name+"/////");
    }

    void Update()
    {
        if(!myTarget)
        return;

        handleLogic();
    }

    private void Handle_Damage()
    {
        if (!mystats.Is_alive())
        {
            _EnemyManager.OnEnemyKilled?.Invoke(45);
        }
    }

    public void handleLogic()
    {
        //countdownto decision
        if(decideTime.UpdateTimer_bool(Time.deltaTime)&& !attackready)
        {
           attackready=true; 
        }

        // decide based on rng after countdown
        if (attackready && !performing_action)
        {
            
        // int rg=Random.Range(0,5);
        //     if (rg < 2)
        //     {
               performing_action=true;
               DoMelle(); 
            
        }
    }

    private void DoMelle()
    {
        dashattack.onendattack = () =>
        {
          decideTime.Reset_TImer();  
          performing_action=false;
          attackready=false;
        };

        dashattack.DODash(myTarget.position-transform.position);
    }
}
