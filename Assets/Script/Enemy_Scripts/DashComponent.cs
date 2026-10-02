using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashComponent : MonoBehaviour
{
    [SerializeField]Transform roottransform;
    public float dashtime=1,dashspeed=3;
    public float timer=0;

    public bool isdashing,candash=true,facedir=true;

    public UnityEvent OnDashStart,OnDashEnd;

    public System.Action onendattack;

   [SerializeField] RbodyMover rbodyMover;
   public Melle_Listner melle;

    Vector3 dashdir;
    public System.Action<Transform>brainDamager;

    public void DODash(Vector3 dir)
    {
        if(!candash)
        return;
        if(isdashing)
        return;
        dashdir=dir;
        isdashing=true;
        timer=dashtime;
        OnDashStart?.Invoke();

         Vector3 _facedir=dashdir;
         if (facedir)
        {
            roottransform.forward=_facedir;
        }

        dashdir.y=transform.position.y;

    }

    void Start()
    {
        melle.on_HasTarget+=DamageEnemy;
    }

    public void setup(System.Action<Transform> endt)
    {
        brainDamager=endt;
    }

    bool donedamage=false;
    private void DamageEnemy(Transform transform)
    {
        if(donedamage)
        return;

       donedamage=true;
       brainDamager.Invoke(transform);

    }

    void Update()
    {
        if(!isdashing)
        return;

        if (timer <= 0)
        {
           
            rbodyMover.StopMove();
            isdashing=false;
            OnDashEnd?.Invoke();
            onendattack?.Invoke();
            donedamage=false;
            //shoot damage
            return;
        }

        melle.DOShoot();
        timer-=Time.deltaTime;
        rbodyMover.SetMovePosition(dashdir,dashspeed);

       
    }


    ///testing
    public Transform dashdirT;

  public  void DODash()
    {
        Vector3 dir=dashdirT.position-transform.position;
        Vector3 facedir=dir;
        
        dashdir.y=transform.position.y;
        DODash(dir);
        

       
    }

}
