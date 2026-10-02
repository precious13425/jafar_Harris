using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShootHelper : MonoBehaviour
{
   
    // Start is called before the first frame update
    [SerializeField]_EnemyManager manager;
    [SerializeField]Transform rootTransform;
    [SerializeField]GameObject spawnprefab;
    [SerializeField]Transform spawnposition;

    //public damageHandler damager;



    public Transform Target;

    void Start()
    {
       if(!manager)
       manager=GetComponentInParent<_EnemyManager>();
        
    }

    public void DOShoot()
    {

       

       var tmpdata=getSpawnedObj();
        
        if(!tmpdata)
        return;

            var hitbox=tmpdata.GetComponent<ActionHitBox>();
            if (hitbox)
            {
                hitbox.Setup(rootTransform);
                hitbox.StartAction(manager.HandleTarget);
            }
        

        if (!Target)
        {
            Target=manager.getTarget;
        }

        if(Target)
        tmpdata.transform.LookAt(Target.position+Vector3.up*0.2f);
      
    }


//spawns enemy gameobject
    GameObject getSpawnedObj()
    {
        GameObject dm=spawnprefab?Instantiate(spawnprefab):null;
        if (dm)
        {
          dm.transform.position=spawnposition.position;
          dm.transform.forward=spawnposition.forward;
        }
        return dm;
    }
}


