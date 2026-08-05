using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public interface IMover
{
    void SetMoveDir(Vector3 dir, float speed);
    void SetRotDir(Vector3 dir, float speed);

    void StopMove();
}

public class RbodyMover : MonoBehaviour, IMover
{
    public Rigidbody rbody;
   [SerializeField] Vector3 forward_dir;
   [SerializeField] float movespeed;
  [SerializeField] bool hasdir;
  [SerializeField]float gravmod=3;
  [SerializeField]float vertialvel=0;

    public void SetMoveDir(Vector3 dir, float speed)
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

   
    

    public void SetRotDir(Vector3 dir, float speed)
    {
        
    }

    public void StopMove()
    {
        forward_dir=Vector3.zero;
        rbody.velocity=Vector3.zero;
    }
}
