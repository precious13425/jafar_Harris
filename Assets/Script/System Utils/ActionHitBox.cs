using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ActionHitBox : MonoBehaviour
{
    public float attack_range=3;
    

    Transform myroot_transform;
   
   [SerializeField] LayerMask hitLayer;
    public Queue<Transform> attackedenemies=new Queue<Transform>();
    
    public System.Action<Transform>OnUnitHit;

//chck for target to damage
    bool ischecking;
    float checktime=1,c_timer=0;


    public void Setup(Transform roottransform, float range=-1)
    {
      if(range>0) attack_range=range;
      
        if (roottransform != null)
        {
            myroot_transform=roottransform;
        }
        
        buffer=new Collider[12];
    }


    //runthis logic at update
    Collider[]buffer;
    void Doaction()
    {

       var attackdata=Jf_Utils.GetHit_atPoint(transform.position,hitLayer,buffer,attack_range);
           
         
        if(attackdata.success)
        {
            foreach (var gm in attackdata.resultHit)
            {
              
              OnUnitHit?.Invoke(gm);

              //this part goes to the brain to decide
                if(gm==myroot_transform || attackedenemies.Contains(gm))
                continue;

                attackedenemies.Enqueue(gm);

                //
            } 
        }


    }


    //already has target in his sight using just a lazy dis check
    public void CallAction()
    {
        var buffer=new Collider[13];
        c_timer=checktime;
        ischecking=true;
        attackedenemies.Clear();

    }


    void Update()
    {
        if (!ischecking)
        return;
        
        //handles countdown
        c_timer-=Time.deltaTime;
        if (c_timer <= 0)
        {
            ischecking=false;
            EndAction();
        }

        Doaction();

     }

     
    public void EndAction()
    {
       attackedenemies.Clear();
    }


 
    

[SerializeField]bool showgizmos;
[SerializeField]Color gizcolor=Color.blue;
    void OnDrawGizmosSelected()
    {
        if(!showgizmos)
        return;

        Gizmos.color=gizcolor;
        Gizmos.DrawWireSphere(transform.position,attack_range);
    }

}

