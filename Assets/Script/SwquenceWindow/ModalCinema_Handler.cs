using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalCinema_Handler : Jf_Singleton<ModalWindowHandler>
{
   
    [SerializeField] CinemaIntroBark.ModalBark[] startBark;
    public CinemaIntroBark modalSystem;
    public float fadecavastime=1;
    public bool isShowing;

    [Header("Visual")]
    public CanvasGroup rootcanvas;
    public Button skipbtn;

    public UnityEvent on_cinemaEnd;


    void Setup()
    {
        modalSystem=new CinemaIntroBark(this);
        skipbtn.onClick.AddListener(SkipIntro);
    }

    private void SkipIntro()
    {
       on_cinemaEnd?.Invoke();
    }

    void Start()
    {
        Setup();
        if (startBark.Length >= 0)
        {
            foreach (var item in startBark)
            {
                modalSystem.AddBark(item);
            }
        }

        Debug.LogWarning(modalSystem.GEtBarkcount()+"????");
        //Show(); //texsting purpose

    }


   

    public void Show()
    {


         if (modalSystem.isbarkempty)
        {
            SkipIntro();

            return;
        }
        
        if (!isShowing)
        StartCoroutine(ProcessQueue());

        rootcanvas.alpha=1;
         rootcanvas.blocksRaycasts=true;
    }

    

    IEnumerator ProcessQueue()
    {
        isShowing = true;

        while (!modalSystem.isbarkempty)
        {
            var entry = modalSystem.getBark();
            entry.messageboard.alpha=1;
            entry.messageboard.transform.SetAsLastSibling();

            yield return new WaitForSeconds(entry.duration);
            entry.onendcall?.Invoke();

            yield return FadeCanvas(entry.messageboard);
            Debug.LogWarning(modalSystem.isbarkempty);
            //yield return new WaitForSeconds(0.15f); // small gap between consecutive barks
        }
        isShowing=false;

        SkipIntro();
    }

    IEnumerator FadeCanvas(CanvasGroup data)
    {
        float fadetime=0;
        data.alpha=1;
        
        while (fadetime < fadecavastime)
        {
            fadetime+=Time.deltaTime;
            data.alpha-=fadetime/fadecavastime;
            yield return null;
        }
    }


}


[System.Serializable]
public class CinemaIntroBark
{

     [System.Serializable]
    public struct ModalBark
    {
     public  CanvasGroup messageboard;
     public float duration;
     
     public UnityEvent onendcall;
    }

    public Queue<ModalBark>QuedBark=new Queue<ModalBark>();
    ModalCinema_Handler handler;
    public CinemaIntroBark(ModalCinema_Handler handler)
    {
        this.handler=handler;
        QuedBark=new Queue<ModalBark>();
    }

    public void AddBark(ModalBark bark)
    {
        QuedBark.Enqueue(bark);
    }

    public bool isbarkempty=>QuedBark.Count<=0;

    public ModalBark getBark()
    {

        return QuedBark.Dequeue();
    }

    public void Clear()
    {
        QuedBark.Clear();
    }

    internal string GEtBarkcount()
    {
       return QuedBark.Count.ToString();
    }
}