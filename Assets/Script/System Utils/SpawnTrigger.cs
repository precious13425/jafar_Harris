using UnityEngine;

public class SpawnTrigger:MonoBehaviour
{
    public Transform prefab;
   [SerializeField]float _lifetime=3;
   [SerializeField]bool uselifetime=true;
    public void SpawnPrefab()
    {
        if(!prefab)
        return;
        
       var go=GameObject.Instantiate(prefab);
        go.transform.position=transform.position;
      if(uselifetime)
        go.Add_Lifetime(_lifetime);
      
    }

     public Transform _SpawnPrefab()
    {
        if(!prefab)
        return null;
        
       var go=GameObject.Instantiate(prefab);
        go.transform.position=transform.position;
      if(uselifetime)
        go.Add_Lifetime(_lifetime);

        return go.transform;
    }

    


}
