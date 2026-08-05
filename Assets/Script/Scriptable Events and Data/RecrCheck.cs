using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecrCheck : MonoBehaviour
{
    public bool IsActive;
   public RectTransform rectTransform;
   public Transform checkpoint;
   public GameEvent Success,Loss;
   

   public bool InRange_B;
   public Vector3 woldout;
   public Transform p1,p2;
   Vector2 newpos=>checkpoint?new Vector2(checkpoint.position.x,checkpoint.position.y):Vector2.zero;
   public Vector3 targetpoint;
    public float movespeed=3;
    
    void Start()
    {
        targetpoint=p1.position;
    }

    void Update()
    {
        if(!IsActive)
        return;

        InRange_B=RectTransformUtility.
        RectangleContainsScreenPoint(rectTransform,newpos);
        checkpoint.position=Vector3.MoveTowards(checkpoint.position,targetpoint,movespeed*Time.deltaTime);
        
        
        if (checkpoint.InRange(p1.position, 0.1f))
        {
            targetpoint=p2.position;
        }
        if (checkpoint.InRange(p2.position, 0.1f))
        {
            targetpoint=p1.position;
        }
        
    }

    public void CheckEvent()
    {
      if(InRange_B)
        Success?.OnRaise();
        else
        Loss?.OnRaise();
    }

    public void StopQTE()=>IsActive=false;

}
