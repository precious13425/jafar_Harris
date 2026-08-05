using System;
using UnityEngine;
using UnityEngine.Events;

public class RangeBox :MonoBehaviour
{
    public Transform prefab;
    public UnityEvent onRangeEnd;
    [SerializeField]Transform spawnpoint;
    Vector3 facedir;
    [SerializeField]Transform rootT;
    [SerializeField]float shotrange=6;

    public event Action<Transform> OntryDamage;



    public void Doaction(Transform T)
    {
        if(!prefab)
        return;
       var gm= prefab.SpawnAt(spawnpoint.position);
       gm.forward=facedir.normalized;
    
       
            
       //get the projectile script and setit up
       Bullet bullet=gm.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetUp(this,rootT,shotrange);
        }
       
    }

    public void EndAction()
    {
       
    }

    public void ProcessTarget(Transform T)
    {
       Debug.LogWarning("fushanmi");
    
        if(T==rootT)
            return;

        OntryDamage?.Invoke(T);    
        onRangeEnd?.Invoke();
        
    }


    public void Setup(Transform roottransform, Vector3 facedir, float range)
    {
        rootT=roottransform;
        this.facedir=facedir;
       if(range>=1)
        shotrange=range;
        

    }

    
}