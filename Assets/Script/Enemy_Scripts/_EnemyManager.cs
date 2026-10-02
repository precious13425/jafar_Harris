using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class _EnemyManager : MonoBehaviour,Ireward_able
{

   #region  Variables
   public Enemy_SO enemy_SO;
   [SerializeField]Player_SO target;
   public bool usedchasemove;

   [Header("health Variables")]
   public bool isAiActive;
    [SerializeField]float healthval;
    public StatDamager_COmp mystats;
   public Transform getTarget=>target.GetPlayer;



#endregion

   IEnumerator Start()
    {
      yield return null;

      isAiActive=true;


      //setuphealth
      mystats.OnValChange+=Handle_Damage;
      mystats.SetUp(healthval);
      

    }
  
    public void Handle_Damage()
    {
      if (mystats.Is_alive())
      {
         isAiActive=false;
         StaticUpdater.DelayedCall(()=>isAiActive=true,2);
      }
      

    }

    public void UpdateKillQUest()=>OnEnemyKilled?.Invoke(enemy_SO.unitid);


   /*

   public virtual void DOLogic()
   {
      UpdateAttackTime();

      
      TryAttack();
      TryMove();
   }

   void UpdateAttackTime()
   {
      
      if (attack_timer.UpdateTimer_bool(Time.deltaTime) && !attackready)
      {
         attackready=true;
      }

   }

   void TryMove()
   {
      if (distanceToPlayer > dis_toAttack && usedchasemove)
      {
         mover.SetMovePosition(target.GetPlayer.position,chaseSpeed);
         footstep.PlayFootstepSounds(footstepdelay);  
      }
   }

   void TryAttack()
   {
      if(distanceToPlayer>dis_toAttack)
      return;

      if (actionHitBox && attackready)
      {
        // usedchasemove=false;
         attackready=false;
         actionHitBox.DOShoot();
         attack_timer.Reset_TImer();
         Debug.Log("enemy attacking");
      }
      
   }

*/


#region  Others
   public static System.Action<int> OnEnemyKilled;

    public void HandleTarget(Transform target)
    {
      //check if target is a valid enemy
       var result=DamageHandler.CheckDamage(target);
        if (!result.isvalid)
            return;
            
         //check if target exist
        if(result.target!=null)
            result.target.Take_Damage(4);
        
        //check if target is still alive
        if (!result.target.Is_alive())
      {
        isAiActive=true;
      }
      
    }


   public float distanceToPlayer
   {
      get
      {
         if (getTarget != null)
         {
            return Vector3.Distance(transform.position,getTarget.position);
         }
         return 9000000;
      }
   }

    public float Level { get; private set; }

    public static Transform SpawnPlayer(Enemy_SO data,float level=1)
    {
       var player_T=GameObject.Instantiate(data.PlayerPrefab).transform;
       player_T.GetComponent<_EnemyManager>().SetUp(data,level);
       return player_T;
    }

   void SetUp(Enemy_SO data,float level)
   {
      this.Level=level;
      this.enemy_SO=data;
   }

   public (float coin,float bullet) GetRewardval()
   {
      return (Mathf.Max(1,enemy_SO.coinrewardbase*Level),Mathf.Max(1,enemy_SO.bulletrewardbase));
         
   }

    public void Revive()
    {
      mystats.SetUp(healthval);
      isAiActive=true;
    }

  
    #endregion

}

public interface Ireward_able
{
   (float coin, float bullet) GetRewardval();
}

   