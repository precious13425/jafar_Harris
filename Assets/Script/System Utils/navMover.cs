using UnityEngine;

public class navMover:MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    Vector3 targ;
    float movespeed;

    public void SetMove(Vector3 position,float speed)
    {
        targ=position;
        movespeed=speed;
        agent.enabled=true;
    }

    void Update()
    {
        if (transform.InRange(targ, 1))
        {
        agent.enabled=false;
        }
        else
        {
            agent.speed=movespeed;
            agent.destination=targ;
            
        }
    }
                
    public void StopMove()=>agent.enabled=false;

}


public class Dir2PosCoverter : MonoBehaviour
{   
    [SerializeField]Transform roottansform;
    public ImoveInterface Mover;
   
     public void Convert2_Position(Vector3 Dir,float speed)
    {
        var newpos=roottansform.position+Dir.ZeroY().normalized;
        Mover.SetMove(newpos,speed);
       
    }
}


public class Pos2DirCOnverter : MonoBehaviour
{   
    [SerializeField]Transform roottansform;
    public ImoveInterface Mover;
     public void Convert2_Dir(Vector3 position,float speed)
    {
        var newdir=roottansform.position-position;
        Mover.SetMove(newdir.ZeroY().normalized,speed);
       
    }

    

}
    public interface ImoveInterface
    {
      void  SetMove(Vector3 input,float speed);
      void StopMove();
    }
