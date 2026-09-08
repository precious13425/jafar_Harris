using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;



public class RbodyMover : MonoBehaviour, ImovePos
{
    public Rigidbody rbody;
   [SerializeField] Vector3 forward_dir;
   [SerializeField] float movespeed;
  [SerializeField] bool hasdir;
  [SerializeField]float gravmod=3;
  [SerializeField]float vertialvel=0;

    public void SetMovePosition(Vector3 dir, float speed)
    {
       forward_dir=dir.normalized.ZeroY();
       movespeed=speed;
        if (forward_dir.magnitude < 0.1f)
        {
            StopMove();
            hasdir=false; 
            return;  
        }
        hasdir=true;
    }

  

    void FixedUpdate()
    {
        if(!hasdir)
        return;
        
        forward_dir.y=vertialvel;
        rbody.velocity=forward_dir*movespeed;
    }

   
    

    public void StopMove()
    {
        forward_dir=Vector3.zero;
        rbody.velocity=Vector3.zero;
    }
}
