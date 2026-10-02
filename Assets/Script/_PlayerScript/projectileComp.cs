using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileComp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Transform rootTransform;
    [SerializeField]Shoot_Component shoot_Component;
    [SerializeField]GameObject spawnprefab;
    [SerializeField]Transform spawnposition;
    Interract_Handler handler;

    //public damageHandler damager;



    public Transform Target;

    void Start()
    {
        handler=Interract_Handler.ins;
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
                hitbox.StartAction(shoot_Component.HandleTarget);
            }
        

        if (handler)
        {
            if (handler.curselected != null)
            {
                Target=handler.curselected.transform;
                tmpdata.transform.LookAt(Target.position+Vector3.up*0.2f);
            }
        }
      
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


