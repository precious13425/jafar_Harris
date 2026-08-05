using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad_System : MonoBehaviour
{
    
    public System.Action OnvalueChange;

    public static LevelLoad_System ins;
    //the gamedata
    public Gamdata_SO myData;
   //current objective to banish
    public fadeUi fadeUi;
    public float fadeintime=.5f;
    
    void Awake()
    {
        if(ins!=null && ins != this)
        {
            Destroy(this);
            return;
        }
        ins=this;
    }

    public void CallScene(int id)
    {
        fadeUi?.FadeInBlack(fadeintime, () =>
        {
            SceneManager.LoadScene(id);
        });
    }

    public void CallScene(string id)
    {
        fadeUi?.FadeInBlack(fadeintime, () =>
        {
            SceneManager.LoadScene(id);
        });
    }

    public static void  FadeInScene(int id)
    {
       
       Debug.Log("Waess");
        ins?.CallScene(id);
    }

     public static void  FadeInScene(string id)
    {
        ins?.CallScene(id);
    }

    public void QUitGame()=>Application.Quit();
}
