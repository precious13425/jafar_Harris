using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public float movespeed;
    RangeBox rangeCaller;

    //calculate the ditance to disable bullet
    float range;
    Vector3 startpos;
   [SerializeField] bool isactive,ispenetrating;
    Transform myroot;
    
    public UnityEvent OnCollideEnd;


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
            OnCollideEnd?.Invoke();
        }
    }

    
  void OnTriggerEnter(Collider other)
    {
       if (myroot != null)
        {
            if(myroot==other.transform)
            return;
            
        }

        if (rangeCaller != null)
        {
            // unitEntity enemyvar=other.GetComponent<unitEntity>();
            // if(enemyvar && rangeCaller)
            // {
            //     if (!unitEntity.isValidEnemy(rangeCaller.getEntity(), enemyvar))
            //     {
            //         return;
            //     }
            // }

            rangeCaller.ProcessTarget(other.transform);
            if (!ispenetrating)
            {
                isactive=false;
                 OnCollideEnd?.Invoke();
            }
        }

    }
   

}

