using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MelleSwing : MonoBehaviour
{
    //public damageHandler damager;
    public UnityEvent OnSwingStart;
    public float shoot_rate=.5f;
    Jf_Utils.Jf_timer attacktimer;
    bool swing_ready=true;
    bool applydamage=true;

    System.Action<Transform>Endarg;
    void Start()
    {
        attacktimer=new Jf_Utils.Jf_timer(shoot_rate);
       Melle_Listner.OnValueChange+=HandleTarget;
    }

  

    void Update()
    {
        if (!swing_ready)
        {
            if (attacktimer.UpdateTimer_bool(Time.deltaTime))
            {
                swing_ready=true;
                applydamage=true;
                attacktimer.Reset_TImer();
            }
            
        }
    }

    public void SetUp(System.Action<Transform>handleTarget)
    {
        Endarg=handleTarget;
    }

    public void DOShoot()
    {
        if(!swing_ready)
        return;
       

        swing_ready=false;
        OnSwingStart?.Invoke();
       // Debug.LogWarning(Time.time+"??"+name);
    }

    public void HandleTarget(Transform target)
    {
       Endarg?.Invoke(target);
      
       // result=(false,null);
    }

    void OnDisable()
    {
        Melle_Listner.OnValueChange-=HandleTarget;


    }
}
