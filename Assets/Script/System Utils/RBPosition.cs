using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBPosition : MonoBehaviour
{
   
    public Transform Roottransform;
   Vector3 movedir,Rotdir;
    float movespeed,rotspeed;
    [SerializeField]float decelerationspeed=2;
    

    void Update()
    {
        HandleRotate();
       //HandleMove();
    }

    void FixedUpdate()
    {
       HandleMove();
    }

        public void SetMoveDir(Vector3 dir,float speed)
    {
        movedir=dir;
        movespeed=speed;
    }

    public void SetRotDir(Vector3 dir,float speed)
    {
        Rotdir=dir;
        rotspeed=speed;
    }

    void HandleMove()
    {


        if (movedir == Vector3.zero)
        {
            Roottransform.position=transform.position;
            return;
        }
        
        Roottransform.position+=movedir.ZeroY().normalized*movespeed*Time.deltaTime;

    }

    void HandleRotate()
    {
        if(Rotdir==Vector3.zero)
        return;

        Rotdir=Rotdir.normalized.ZeroY();
        Roottransform.LookAt(Rotdir);
    }

}
