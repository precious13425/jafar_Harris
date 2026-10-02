using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TelepgrahComp : MonoBehaviour
{
   public GameObject telegrapghvfx;
   public float telegraphtime=0.6f;
   public float recoverytime=0.5f;
   public _EnemyManager manager;
   public UnityEvent onfire;
   bool isready=true;
   public void handleStart()
   {
      if(!isready)
      return;

      StopAllCoroutines();
      StartCoroutine(STartSequence());
   }


   IEnumerator STartSequence()
   {
      isready=false;
      telegrapghvfx?.ToggleObject();
      for (float i = 0; i < telegraphtime; i+=Time.deltaTime)
      {
         handleProgress(i/telegraphtime);
         yield return null;
      }
      onfire?.Invoke();
      
   }

    private void handleProgress(float v)
    {
      
    }

    public void handleAttackStart()
   {
      
   }

   public void HandleAttackEnd()
   {
      
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