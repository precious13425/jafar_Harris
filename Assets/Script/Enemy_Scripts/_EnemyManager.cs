using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class _EnemyManager : MonoBehaviour,Idamagable
{

   #region  Variables
   public Enemy_SO enemy_SO;
   [field:SerializeField]public bool isperformingaction{get;private set;}
    [Header("health Variables")]
   [SerializeField]bool isover;
    [SerializeField]float healthval;
    public Pooltype energy;
    public UnityEvent OnHurt,OnDead;
   [SerializeField]float att_time=2;
   [SerializeField]Jf_Utils.Jf_timer attack_timer;
   //JfCorutine jfCorutine=new JfCorutine();


    [Header("Attack Variables")]
    public bool attackready;
    [SerializeField]float Damage_Amount=1,dis_toAttack=3;
   public MelleSwing actionHitBox;
   [SerializeField]Player_SO target;
   Transform getTarget;

    [Header("Hud Variables")]
    public Slider healthslider;
    public TMP_Text healthtext;
    public CanvasGroup healthcanvas;
    public Coroutine fadecorutine;
   

    [Header("chase Variable")]
   [SerializeField] navMover mover;
   [SerializeField] float chaseSpeed=3;
   [SerializeField] FootStepHandler footstep;
   [SerializeField]float footstepdelay=0.5f;

#endregion

   public float endval;
   IEnumerator Start()
    {
      yield return null;

      energy= enemy_SO?enemy_SO.GEt_health(Level):new Pooltype(healthval);
      Damage_Amount=enemy_SO?enemy_SO.Get_AttackValue(Level):Damage_Amount;

      attack_timer=new Jf_Utils.Jf_timer(att_time);

      if (target)
      {
         getTarget=target.GetPlayer;
         
      }

      //setup attack hitbox
      actionHitBox.SetUp(HandleTarget);
      UpdateHealth();

      

    }


    public Transform GetTransform()
    {
       return transform;
    }

    public void Take_Damage(float val)
    {
       energy.Remove_Value(1);
       OnHurt?.Invoke();
      if (energy.isempty)
      {
         
       OnDead?.Invoke();
       OnEnemyKilled?.Invoke(enemy_SO.unitid);
      }

      //send event

      UpdateHealth();
      // Debug.Log(Time.time);
    
    }

    public bool Is_alive()
    {
       return !energy.isempty;
    }

    public void Revive()
    {
       energy.SetFull();
    }


    void Update()
    {
      if(isover)
      return;

      UpdateAttackTime();

      if(isperformingaction)
      return;


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
      if (distanceToPlayer > dis_toAttack)
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
         attackready=false;
         actionHitBox.DOShoot();
         attack_timer.Reset_TImer();
         Debug.Log("enemy attacking");
      }
      
   }

   public static System.Action<int> OnEnemyKilled;

    public void HandleTarget(Transform target)
    {
      //check if target is a valid enemy
       var result=DamageHandler.CheckDamage(target);
        if (!result.isvalid)
            return;
            
         //check if target exist
        if(result.target!=null)
            result.target.Take_Damage(Damage_Amount);
        
        //check if target is still alive
        if (!result.target.Is_alive())
      {
        isover=true;
      }
      
    }

#region  other health
   public void UpdateHealth()
    {
      if (energy.isempty)
         {
           StopAllCoroutines();
         healthcanvas.alpha=0;
         isover=true;
         return;
         //show GameOver
         }


       healthcanvas.alpha=1;
         healthslider.value=energy.getvalue01; 
         healthtext.text=$"{energy.getvalue}/{energy.get_MaxValue}";

           
      if (fadecorutine != null)
      {
         StopCoroutine(fadecorutine);
      }
      fadecorutine=StartCoroutine(JfCorutine.LerpValue2(healthcanvas,0,3));


       
    }

    float distanceToPlayer
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

    public void SetUp(Enemy_SO data,float level)
   {
      this.Level=level;
      this.enemy_SO=data;
   }

   public (float coin,float bullet_Mod) GetRewardval()
   {
      float coin=energy.get_MaxValue*0.5f;
      return (coin,Level);
         
   }
#endregion

   
}

public class JfCorutine
{
  
  

    public  static IEnumerator LerpValue2(CanvasGroup retval,float targetval,float duration,System.Action Onend=null)
     {
        float timer=0;
        float startval=retval.alpha;
        while (timer < duration)
        {
            timer+=Time.deltaTime;
            retval.alpha=Mathf.Lerp(startval,targetval,timer/duration);
            Debug.Log(retval+"|||....");
            yield return null;
        }
      
        Onend?.Invoke();
        
    }
}


