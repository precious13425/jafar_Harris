using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class lifetime : MonoBehaviour
{
   public float mylifetime=5;
    public bool destroyonelaspe=true;
    public UnityEvent LifeTimeEnd;

    void OnEnable()
    {
     
        Invoke(nameof(Doaction),mylifetime);
    }

    private void Doaction()
    {
        if(destroyonelaspe)
        {
            Destroy(gameObject,0.03f);
        }
        gameObject.SetActive(false);
        LifeTimeEnd?.Invoke();
    }

    
    

}

public static class LifetimeExtension{
        public static Transform Add_Lifetime(this Transform T,float newdata)
    {   lifetime gm=T.GetComponent<lifetime>();
        if (gm == null)
        {
            T.gameObject.AddComponent<lifetime>().mylifetime=newdata;
        }
      
        return T;
    }

}
