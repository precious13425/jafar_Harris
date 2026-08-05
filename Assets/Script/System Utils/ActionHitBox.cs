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


    public void Setup(Transform roottransform, Vector3 facedir, float range)
    {
      if(range>0) attack_range=range;
      
        if (roottransform != null)
        {
            myroot_transform=roottransform;
        }
        
    }


    //runthis logic at update
    Collider[]buffer;
    public void Doaction(Transform T)
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


    public void CallAction(Transform target)
    {
        c_timer=checktime;
        ischecking=true;
        attackedenemies.Clear();
        var buffer=new Collider[13];

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



     }

     
    public void EndAction()
    {
       attackedenemies.Clear();
    }


    


    


[SerializeField]Color gizcolor=Color.blue;
    void OnDrawGizmosSelected()
    {
        Gizmos.color=gizcolor;
        Gizmos.DrawWireSphere(transform.position,attack_range);
    }

}

