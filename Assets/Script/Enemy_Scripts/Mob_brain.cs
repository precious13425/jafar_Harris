using System;
using JetBrains.Annotations;
using UnityEngine;

public class Mob_brain : MonoBehaviour
{

   public _EnemyManager manager;
   [SerializeField] BossAttackSystem bossAttackSystem;

    [Header("Attack Variables")]
    public bool attackready;
    [SerializeField]float Damage_Amount=1,stoppingDistance=3;
   public MelleSwing actionHitBox;
   [SerializeField]float att_time=2;
   [SerializeField]Jf_Utils.Jf_timer attack_timer;
   
   public int  PHASE=1;
   //JfCorutine jfCorutine=new JfCorutine();


   [Header("chase Variable")]
   [SerializeField] navMover mover;
   [SerializeField] float chaseSpeed=3;
   [SerializeField] FootStepHandler footstep;
   [SerializeField]float footstepdelay=0.5f;

    void Start()
    {
        attack_timer=new Jf_Utils.Jf_timer(att_time);
         //setup attack hitbox
     actionHitBox.SetUp(manager.HandleTarget);

    }

    void Update()
    {
      if(!manager.isAiActive)
      return;

      if(!Gamdata_SO.isaggro)
      return;

      DOLogic();
    }

    public virtual void DOLogic()
   {
      //UpdateAttackTime();

      
      TryAttack();
     // TryMove();
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
      if (manager.distanceToPlayer > stoppingDistance)
      {
         mover.SetMovePosition(manager.getTarget.position,chaseSpeed);
         footstep.PlayFootstepSounds(footstepdelay);  
      }
      else
      transform.rotation=Quaternion.LookRotation(manager.getTarget.position-transform.position);
     
      
   }

   void TryAttack()
   {

      if (bossAttackSystem.IsBusy) {
         transform.rotation=Quaternion.LookRotation(manager.getTarget.position-transform.position);
         return;
      }

       var next = bossAttackSystem.Pick(manager.distanceToPlayer, PHASE);

       if (next != null)
        bossAttackSystem.Begin(next);
         else TryMove();

     
      
   }


}




/*

public class BossBrain : MonoBehaviour
{

   public _EnemyManager manager;
    [Header("Attack Variables")]
    public bool attackready;
  
   public MelleSwing[] actionHitBox;
   Transform getTarget;
   [SerializeField]Jf_Utils.Jf_timer SelectTime;
   //JfCorutine jfCorutine=new JfCorutine();


   [Header("chase Variable")]
   [SerializeField] navMover mover;
   [SerializeField] float chaseSpeed=3;
   [SerializeField] FootStepHandler footstep;
   [SerializeField]float footstepdelay=0.5f;

   public System.Action OnTelegraphstart,onattackfire,onattackend; 

    public virtual void DOLogic()
   {
      UpdateAttackTime();

      
   if(! TryAttack())
      TryMove();
   }

   void UpdateAttackTime()
   {
      
      if (SelectTime.UpdateTimer_bool(Time.deltaTime) && !attackready)
      {
         attackready=true;
      }

   }

   void TryMove()
   {
      if (manager.distanceToPlayer > 2)
      {
         mover.SetMovePosition(manager.getTarget.position,chaseSpeed);
         footstep.PlayFootstepSounds(footstepdelay);  
      }
   }

   bool TryAttack()
   {
      return true;

      if (!attackready)
      {
         attackready=true;
         OnTelegraphstart?.Invoke();
      } 
      
   }


}

   
*/