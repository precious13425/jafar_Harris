using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad_System : MonoBehaviour
{
    
    public System.Action OnvalueChange;

    public static LevelLoad_System ins;
    //the gamedata
    public SceneFade fadeUi;
    public float fadeintime=.5f;
    Coroutine Faderoutine;
    public bool fade_OnStart=true;

    void Awake()
    {
        if(ins!=null && ins != this)
        {
            Destroy(this);
            return;
        }
        ins=this;
    }

    IEnumerator Start()
    {
        if (fade_OnStart)
        {
        fadeUi.Fade_CutBlack(1);   
         yield return new WaitForSeconds(1);
        EmptyFade();
        }
        yield return null;
    }

   public void CallScene(int id)
    {
        fadeUi?.FadeInBlack(fadeintime, () =>
        {
            SceneManager.LoadScene(id);
        });
              
        Faderoutine=StartCoroutine(fadeUi.callFade_ROutine());

    }

     void CallScene(string id)
    {
        fadeUi?.FadeInBlack(fadeintime, () =>
        {
            SceneManager.LoadScene(id);
        });
        Faderoutine=StartCoroutine(fadeUi.callFade_ROutine());

    }

     void EmptyFade(bool fadeout=true)
    {
        //sets fadeing to or from black variables
       if(fadeout)
        {
            fadeUi.FadeOutBlack(fadeintime);
        }
       else
       {
        
        fadeUi.FadeInBlack(fadeintime);
       }

        //start the fade corutine
        Faderoutine=StartCoroutine(fadeUi.callFade_ROutine());
       

    }


    public static void  FadeInScene(int id)
    {
        ins?.CallScene(id);
    }

     public static void  FadeInScene(string id)
    {
        ins?.CallScene(id);
    }


    public void QuitGame()=>Application.Quit();


}


[Serializable]
public class SceneFade
{
     public CanvasGroup fadecanvas;
    [SerializeField] float TargetValue,fade_duration;
    public float timer;



    
    public System.Action On_FadeComplete ;



    public void FadeOutBlack(float fadetime=3,System.Action endarg=null)
    {
        if(!fadecanvas)
        return;

        On_FadeComplete=endarg;
         fadecanvas.blocksRaycasts=false;
       TargetValue=0;
        fade_duration=fadetime;
        //Faderoutine=StartCoroutine(callFade_ROutine());
    }

    public void FadeInBlack(float fadetime=3, System.Action endarg=null)
    {
        if(!fadecanvas)
        return;

        TargetValue=1;        
        On_FadeComplete=endarg;
        fade_duration=fadetime;
        fadecanvas.blocksRaycasts=true;
        
       
       // Faderoutine=StartCoroutine(callFade_ROutine());
    }

  
   public IEnumerator callFade_ROutine()
    {  
         timer=0;
        float startval=fadecanvas.alpha;

        while (timer < fade_duration)
        {
            timer+=Time.deltaTime;
            fadecanvas.alpha=Mathf.Lerp(startval,TargetValue,timer/fade_duration);
            yield return null;
        }
      
        On_FadeComplete?.Invoke();
        On_FadeComplete=null;
        fadecanvas.blocksRaycasts=fadecanvas.alpha>0?true:false;
    }

   
    public void Fade_CutBlack(float value)
    {
        fadecanvas.alpha=value;
        fadecanvas.blocksRaycasts=value>0?true:false;
    }

    
}

