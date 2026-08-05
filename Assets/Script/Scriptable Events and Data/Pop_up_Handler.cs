using UnityEngine;
using UnityEngine.UI;

class Pop_up_Handler: MonoBehaviour{
    public EventListner_FloatV3 strut_GameEvent;
    public Transform prefab;

    Vector3 SpawnPosition
    {
        get
        {
            if (strut_GameEvent)
            {
               return strut_GameEvent.dataval.data_position;
            }
            else
            {
                return transform.position;
            }
        }
    }

   
    float DataValue
    {
        get
        {
            if (strut_GameEvent)
            {
               return strut_GameEvent.dataval.data_value;
            }
            else
            {
                return 0;
            }
        }
    }
    
   
    public void DOAction()
    {
       
       if(!prefab)
        return;
        
        

        //SET the position
       var go=GameObject.Instantiate(prefab);
        go.position=SpawnPosition;
      
    }   
}
