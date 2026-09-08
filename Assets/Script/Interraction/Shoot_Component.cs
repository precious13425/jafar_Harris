using System;
using UnityEngine;
using UnityEngine.Events;

//handles the effects part 
public class Shoot_Component : MonoBehaviour
{
    [SerializeField]PlayerManager manager;
    public GameEvent Shootevent;
    //public damageHandler damager;
    public UnityEvent OnShoot_Start;
    public float shoot_rate=.5f;
    Jf_Utils.Jf_timer shoottimer;
    bool Shoot_ready=true;
    bool applydamage=true;

    public float bulletcount
    {
        get
        {
            if (manager && manager.playerdata!=null)
            {
             return manager.playerdata.current_bulletcount.getvalue;  
            }
            return 1;
        }
        
    }
    void Start()
    {
         Shootevent.Register(DOShoot);
         shoottimer=new Jf_Utils.Jf_timer(shoot_rate);
    }

  

    void Update()
    {
        if (!Shoot_ready)
        {
            if (shoottimer.UpdateTimer_bool(Time.deltaTime))
            {
                Shoot_ready=true;
                applydamage=true;
                shoottimer.Reset_TImer();
            }
            
        }
    }

    

    public void DOShoot()
    {
        if(!Shoot_ready)
        return;
        if(bulletcount<=0)
        return;

        Shoot_ready=false;
        USeBullet(1);
        OnShoot_Start?.Invoke();
        Debug.LogWarning(Time.time+"??"+name);
    }


   void USeBullet(float value)
    {
        if (manager && manager.playerdata!=null)
        {
            manager.playerdata.current_bulletcount.Remove_Value(value); 
        }
    }

    void OnDisable()
    {
        Shootevent.UnRegister(DOShoot);


    }

    public void HandleTarget(Transform target)
    {
        
      Endarg?.Invoke(target);
    }

    System.Action<Transform>Endarg;
     public void SetUp(System.Action<Transform>handleTarget)
    {
        Endarg=handleTarget;
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
    bool Is_alive();
    void Revive();
    void Take_Damage(float val);
    Transform GetTransform();
}