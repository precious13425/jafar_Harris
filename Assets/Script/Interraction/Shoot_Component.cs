using System;
using UnityEngine;
using UnityEngine.Events;

//handles the effects part 
public class Shoot_Component : MonoBehaviour
{
    public GameEvent Shootevent;
    //public damageHandler damager;
    public UnityEvent OnShoot_Start;
    public float shoot_rate=.5f;
    Timer_ shoottimer;
    bool Shoot_ready=true;
    bool applydamage=true;

    void Start()
    {
         Shootevent.Register(DOShoot);
         shoottimer=new Timer_(shoot_rate);
         Interract_Handler.ins.OnRangeAction+=HandleTarget;
    }

  

    void Update()
    {
        if (!Shoot_ready)
        {
            if (shoottimer.UpdateValue(Time.deltaTime))
            {
                Shoot_ready=true;
                applydamage=true;
                shoottimer.Reset_TImer();
            }
            
        }
    }

    private void DOShoot()
    {
        if(!Shoot_ready)
        return;
        Shoot_ready=false;
        OnShoot_Start?.Invoke();
        Debug.LogWarning(Time.time+"??"+name);
    }

      private void HandleTarget(Transform target)
    {
        // if(!applydamage)
        // return;
        // applydamage=false;
       var result=DamageHandler.CheckDamage(target);
        if (result.isvalid)
        {
            result.target.Take_Damage(1);
        }
        Debug.LogWarning("Do_Dmg");
    }

    void OnDisable()
    {
        Shootevent.UnRegister(DOShoot);
        Interract_Handler.ins.OnRangeAction-=HandleTarget;


    }
}

public class DamageHandler
{
    public static (bool isvalid,Idamagable target) CheckDamage(Transform target)
    {
        if(!target)
        return (false,null);
       Idamagable targD=target.GetComponent<Idamagable>();
        if (targD != null)
        {
            return (true,targD);
        }

        return (false,null);
    }
}

public interface Idamagable
{
    void Take_Damage(float val);
    Transform GetTransform();
}