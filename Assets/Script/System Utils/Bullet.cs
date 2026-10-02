using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public float movespeed;
    RangeBox rangeCaller;

    //calculate the ditance to disable bullet
   [SerializeField] float range;
    Vector3 startpos;
   [SerializeField] bool isactive;
    Transform myroot;
    
    public UnityEvent OnDistanceReached;

    void Start()
    {
        startpos=transform.position;
    }

    public void SetUp(RangeBox rbox,Transform rootT,float maxrange)
    {
        rangeCaller=rbox;
        myroot=rootT;
       range=maxrange;
        startpos=transform.position;
        isactive=true;
    }

    void Update()
    {
        if(!isactive)
        return;
       

       transform.position+=transform.forward*movespeed*Time.deltaTime;

        if (Vector3.Distance(transform.position, startpos) > range + 1)
        {
            Debug.Log("Exceed");
            isactive=false;
            OnDistanceReached?.Invoke();
        }
    }
    
  
}

