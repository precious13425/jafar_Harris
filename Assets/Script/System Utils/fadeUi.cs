using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class fadeUi : MonoBehaviour
{
    [SerializeField] bool FadeOnStart;
    public CanvasGroup fadecanvas;
    [SerializeField] float TargetValue,fade_duration;
    public  UnityEvent  OnFadeComplete;
    public float timer;


    Coroutine Faderoutine;
    
    public System.Action On_FadeComplete ;

    void OnEnable()
    {
        if(fadecanvas==null)fadecanvas=GetComponent<CanvasGroup>();
        StopAllCoroutines();
        if (FadeOnStart)
        {
            FadeOutBlack(fade_duration);
        }
    }

    public void FadeOutBlack(float fadetime=3,System.Action endarg=null)
    {
        if(!fadecanvas)
        return;

        On_FadeComplete=endarg;
         fadecanvas.blocksRaycasts=false;
       TargetValue=0;
        fade_duration=fadetime;
        Faderoutine=StartCoroutine(callFade_ROutine());
    }

    public void FadeInBlack(float fadetime=3, System.Action endarg=null)
    {
        if(!fadecanvas)
        return;

        TargetValue=1;        
        On_FadeComplete=endarg;
        fade_duration=fadetime;
        fadecanvas.blocksRaycasts=true;
        
       
        Faderoutine=StartCoroutine(callFade_ROutine());
    }

  
    IEnumerator callFade_ROutine()
    {  
         timer=0;
        float startval=fadecanvas.alpha;

        while (timer < fade_duration)
        {
            timer+=Time.deltaTime;
            fadecanvas.alpha=Mathf.Lerp(startval,TargetValue,timer/fade_duration);
            yield return null;
        }
       

       
        
        OnFadeComplete?.Invoke();
        On_FadeComplete?.Invoke();
        On_FadeComplete=null;
    }

    public  void DisposeObJ()
    {
        Destroy(gameObject,0.2f);
    }

    public void Fade_CutBlack(float value)
    {
        fadecanvas.alpha=value;
        fadecanvas.blocksRaycasts=value>0?true:false;
    }
     public void CallFade_corutine(float value)
    {
        if (value <= 0)
        {
            FadeOutBlack();
        }
        else{FadeInBlack();
        }
    }
}
