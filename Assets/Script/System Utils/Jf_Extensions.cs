using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public  static  class Jf_Extensions
{
    public static Vector3 ZeroY(this Vector3 T)
    {
        Vector3 reval=T;
        reval.y=0;
        return  reval;
    }

    public static Vector3 SetY(this Vector3 T,float newY)
    {
        Vector3 reval=T;
        reval.y=newY;
        return  reval;
    }


    public static Vector3 AddY(this Vector3 T,float newY)
    {
        Vector3 reval=T;
        reval.y+=newY;
        return  reval;
    }


    public static Vector3 randomDir()
    {
        var newvec= UnityEngine.Random.insideUnitCircle;
        return new Vector3(newvec.x,0,newvec.y);

    }
    
    public static(bool inrange,float curdis) Ckeck_DIstance(this Transform T,Vector3 targPos,float Dis)
    {
        var curdis=Vector3.Distance(T.position,targPos);
        return   (curdis < Dis,curdis);
    }


   public static Transform GetClosest(this Transform T,List<Transform>units,float maxradius){
      float mindis=maxradius;
      Transform closest=null;

      //checkifempty
      for (int i = 0; i < units.Count; i++)
      {
        if(units[i]==null)
        continue;

        if(T.InRange(units[i].position, mindis)){
            closest=units[i];
            mindis=Vector3.Distance(T.position,closest.position);
        }
      }
      return closest;  
    }

     public static Transform GetClosest(this Vector3 T,List<Transform>units,float maxradius){
      float mindis=maxradius+0.5f;
      Transform closest=null;

      //checkifempty
      for (int i = 0; i < units.Count; i++)
      {
        if(units[i]==null)
        continue;

        if(T.InRange(units[i].position, mindis)){
            closest=units[i];
            mindis=Vector3.Distance(T,closest.position);
        }
      }
      return closest;  
    }

    internal static float CheckLayer(Transform data, LayerMask targetlayer)
    {
        
         var retval= ~data.gameObject.layer<<targetlayer;
        return retval;
    }

    
    #region  check range extensions and toogle object
      public static bool InRange(this Transform T,Vector3 targetpos,float mindis)
    {
        bool inrange=Vector3.Distance(T.position,targetpos)<=mindis;
        return inrange;

    }

     public static bool InRange(this Vector3 T,Vector3 targetpos,float mindis)
    {
        bool inrange=Vector3.Distance(T,targetpos)<=mindis;
        return inrange;

    }

    public static Transform SpawnAt(this GameObject T,Vector3 targetpos)
    {
        Transform retval=GameObject.Instantiate(T).transform;
       retval.position=targetpos;
       return retval;

    }

    


    public static Transform SpawnAt(this Transform T,Vector3 targetpos)
    {
        Transform retval=GameObject.Instantiate(T);
       retval.position=targetpos;
       return retval;

    }

    public static float Angle_ToVector(Vector3 forward_Dir, Vector3 targetposition)
    {
        float retval=9999;
        retval=Vector3.Angle(forward_Dir,targetposition);
        return retval;
    }

    public static float vec_ToAngle_Degree(Vector3 vec)
    {
       float retval=01;
       retval=Mathf.Atan2(vec.y,vec.x)*Mathf.Rad2Deg;
       return retval;
    }

    public static Vector3 vector_FromAngle(float angle)
    {
        Vector3 retval=Vector3.zero;
        retval=new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle));

        return retval;
       
    }

    public static void ToggleObject(this GameObject T)
    {
        T.SetActive(false);
        T.SetActive(true);
    }

#endregion


}
   
   //a group of helper class

public static class Jf_Utils
{

    #region  raycast CheckDistance
    public static RaycastHit Can_move(Vector3 origin,Vector3 direction,float maxdis,LayerMask collide_mask)
    {
        var collided=Physics.Raycast(origin,direction,out RaycastHit hit,maxdis,collide_mask);
        return hit;
    }


     public static bool Canmove(Vector3 origin,Vector3 direction,float maxdis,LayerMask collide_mask)
    {
       return Can_move(origin,direction,maxdis,collide_mask).collider;
    }


    public static float getmatchValue(float cur_value,float minvalue,float maxvalue,float newmin,float newmax)
    {
        float olderpval=Mathf.InverseLerp(minvalue,maxvalue,cur_value); //get the 0-1 value of the float within old range
        float  matchval=Mathf.Lerp(newmin,newmax,olderpval); //get the lerp value using the old0-1 value 
        
        
        return matchval;
    }

    #endregion

     #region  spherecast CheckDistance
    public static RaycastHit Can_move(Vector3 origin,Vector3 direction,float maxdis,float radius,LayerMask collide_mask)
    {
        
        var collided=Physics.SphereCast(origin,radius,direction,out RaycastHit hit,maxdis,collide_mask);
        return hit;
    }


     public static bool Canmove(Vector3 origin,Vector3 direction,float maxdis,float radius,LayerMask collide_mask)
    {
       return Can_move(origin,direction,maxdis,radius,collide_mask).collider;
    }


    #endregion

 public static (bool success,List<Transform> resultHit) GetHit_atPoint(Vector3 center,LayerMask hitlayer,Collider[]mubuffer,float radius=1)
    {
        Vector3 returnhit=Vector3.zero;
        var buffer=mubuffer;

       
        var dm=Physics.OverlapSphereNonAlloc(center,radius,buffer,hitlayer);

        //adds to all list
        List<Transform> allhitTransform=new List<Transform>();
        for (int i = 0; i < buffer.Length; i++)
        {
            if(buffer[i]==null)
            continue;

            allhitTransform.Add(buffer[i].transform);
        }

        return(allhitTransform.Count>0,allhitTransform);
    }


    public static bool isWihinViewAngle(Transform origin,Vector3 targetpos,float Fov_angle)
    {
        Vector3 f_dir=origin.forward;
        Vector3 dir=targetpos-origin.position;
        float angle=Vector3.Angle(f_dir.ZeroY(),dir.ZeroY());
        if(angle<=Fov_angle*0.5f)
        return true;

        return false;
    }

   

}


//random dice function to check probability
 public static class JfDIce
{
    public static int diceRoll(int dicefaceCount)=>UnityEngine.Random.Range(1,dicefaceCount+1);

    public static int diceRool(int no_of_Dice,int Dice_Facecount)
    {
        int totalval=0;
        for (int i = 0; i < no_of_Dice; i++)
        {
            var newval=diceRoll(Dice_Facecount);
            totalval+=newval;
        }
        return totalval;
    }

    public static int diceRollCap(int dicefaceCount,float mincap)=>(int)UnityEngine.Random.Range(mincap,dicefaceCount+1);

}

  
 
 [Serializable]
public class MouseInput
{
    public LayerMask groundlayer;

   
   
   //this accounts for terrain height
    public (bool success,Vector3 hitpoint,Collider hitcollider) GetMouseHit(Vector3 startpoint,Vector3 direction)
    {
        
        bool success=false;
        Vector3 returnhit=Vector3.zero;
        RaycastHit hit;
        Collider hitcollider=null;
       // RaycastHit[] hitall;
        //var Ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        var  Ray=new Ray(startpoint,direction);
        success=Physics.Raycast(Ray,out hit,1000f);
        if (success)
        {
            returnhit=hit.point;
            hitcollider=hit.collider;
        }

        return(success,returnhit,hitcollider);
    }


    public static (bool success,Vector3 hitpoint,Collider hitcollider) GetMouseHit_Point(LayerMask hitlayer)
    {
        bool success=false;
        Vector3 returnhit=Vector3.zero;
        RaycastHit hit;
        Collider hitcollider=null;
       // RaycastHit[] hitall;
        var Ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        success=Physics.Raycast(Ray,out hit,1000f,hitlayer);
        if (success)
        {
            returnhit=hit.point;
            hitcollider=hit.collider;
        }

        return(success,returnhit,hitcollider);
    }

     public static (bool success,List<Transform> resultHit) GetMouseHit_Targets(LayerMask hitlayer,float radius=1)
    {
        Vector3 returnhit=Vector3.zero;
        RaycastHit[] buffer=new RaycastHit[10];

        //gets ray fromcamera screenpoint
        var Ray=Camera.main.ScreenPointToRay(Input.mousePosition);
   
       
        var dm=Physics.SphereCastNonAlloc(Ray,radius,buffer,1000,hitlayer);

        //adds to all list
        List<Transform> allhitTransform=new List<Transform>();
        for (int i = 0; i < buffer.Length; i++)
        {
            
            allhitTransform.Add(buffer[i].transform);
        }

        return(allhitTransform.Count>0,allhitTransform);
    }

  
   

//this is a cheaper raycast and shows th epostion of mouse i world object.
//this does not account for level/terrain height;
//best for flat layout
    public static Vector3 Get_MouseWorldPos3D(Camera Kam,float planeheight = 0)
    {
        Vector3 retval=Vector3.zero;
        Ray ray=Kam.ScreenPointToRay(Input.mousePosition);
        Plane plane=new Plane(Vector3.up,Vector3.up*planeheight);
        if(plane.Raycast(ray,out float distance))
        {
            retval=ray.GetPoint(distance);
        }
        return retval;
    }

   

   
}
