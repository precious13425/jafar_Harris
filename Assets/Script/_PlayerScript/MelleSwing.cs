using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MelleSwing : MonoBehaviour
{
    //public damageHandler damager;
    public UnityEvent OnSwingStart,OnKilledTarget;
    public float shoot_rate=.5f,attack_recovery=0.2f;
    Jf_Utils.Jf_timer attacktimer;
    bool swing_ready=true;
   

    public System.Action Onendarg;

    System.Action<Transform>get_Target;
    public bool usetime=true;
    void Start()
    {
        attacktimer=new Jf_Utils.Jf_timer(shoot_rate);
    }

  

    void Update()
    {
        if (!swing_ready)
        {
            if (attacktimer.UpdateTimer_bool(Time.deltaTime) && usetime)
            {
                swing_ready=true;
                attacktimer.Reset_TImer();
            }
            
        }
    }

    public void SetUp(System.Action<Transform>handleTarget,System.Action endattack=null)
    {
        get_Target=handleTarget;
    }

    public void DOShoot()
    {
        if(!swing_ready)
        return;
       

        swing_ready=false;
        OnSwingStart?.Invoke();
        Invoke(nameof(endattack),attack_recovery);
       // Debug.LogWarning(Time.time+"??"+name);
    }

    private void endattack()
    {
       Onendarg?.Invoke();
        if (!usetime)
        {
            swing_ready=true;

        }
    }

    public void HandleTarget(Transform target)
    {
       get_Target?.Invoke(target);
       // result=(false,null);
    }

   

    internal void DOKill()
    {
       OnKilledTarget?.Invoke();
    }
}
