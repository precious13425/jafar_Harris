using UnityEngine;
using UnityEngine.AI;

public interface ImovePos
{
    void SetMovePosition(Vector3 position, float speed);
    void StopMove();
}

[RequireComponent (typeof(NavMeshAgent))]
public class navMover : MonoBehaviour, ImovePos
{
    public UnityEngine.AI.NavMeshAgent agent;
    Vector3 targ;
    float movespeed;

    public void SetMovePosition(Vector3 position, float speed)
    {
        targ = position;
        movespeed = speed;
        agent.enabled = true;
    }

    void Update()
    {
        if (transform.InRange(targ, 1))
        {
            agent.enabled = false;
        }
        else
        {
            agent.speed = movespeed;
            agent.destination = targ;

        }
    }

    public void StopMove() => agent.enabled = false;

}


public class Dir2PosCoverter : MonoBehaviour
{   
    [SerializeField]Transform roottansform;
    public ImovePos Mover;
   
     public void Convert2_Position(Vector3 Dir,float speed)
    {
        var newpos=roottansform.position+Dir.ZeroY().normalized;
        Mover.SetMovePosition(newpos,speed);
       
    }
}


public class Pos2DirCOnverter : MonoBehaviour
{   
    [SerializeField]Transform roottansform;
    public ImovePos Mover;
     public void Convert2_Dir(Vector3 position,float speed)
    {
        var newdir=roottansform.position-position;
        Mover.SetMovePosition(newdir.ZeroY().normalized,speed);
       
    }

    

}
   
