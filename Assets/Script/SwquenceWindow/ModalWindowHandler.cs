using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalWindowHandler : Jf_Singleton<ModalWindowHandler>
{
    [System.Serializable]
    public struct ModalBark
    {
     public  GameObject messageboard;
     public UnityEvent onendcall;
    }

    [SerializeField] ModalBark[] startBark;
    public ModalSystem modalSystem;

    [Header("Visual")]
    public CanvasGroup rootcanvas;
    public Button ContinueButton;

   [SerializeField] ModalBark prevoiusbark=new ModalBark();

    void Setup()
    {
        modalSystem=new ModalSystem(this);
        ContinueButton.onClick.AddListener(Callnext);
    }

    void Start()
    {
        Setup();
            rootcanvas.gameObject.SetActive(false);

        if (startBark.Length >= 0)
        {
            foreach (var item in startBark)
            {
                modalSystem.AddBark(item);
            }
        }

        Debug.LogWarning(modalSystem.GEtBarkcount()+"????");
       //Callnext(); //texsting purpose

    }


    public void Callnext()
    {
        if (prevoiusbark.onendcall != null)
        {
            prevoiusbark.onendcall?.Invoke();
        }
        
        if (modalSystem.isbarkempty)
        {
            rootcanvas.alpha=0;
            rootcanvas.blocksRaycasts=false;
            return;
        }


        var newdata=modalSystem.getBark();
        Show(newdata);
        Debug.LogWarning(modalSystem.GEtBarkcount());
    }
    
     public void Addbark(GameObject text,System.Action endarg)
    {
        UnityEvent newdat=new UnityEvent();
        newdat.AddListener(()=>endarg?.Invoke());
       modalSystem.AddBark(new ModalBark{
        messageboard=text,
       onendcall=newdat
       });
    }

    public void Show(ModalBark modalBark)
    {
        prevoiusbark=modalBark;

        modalBark.messageboard.SetActive(true);
                 
        rootcanvas.gameObject.SetActive(true);

        rootcanvas.alpha=1;
         rootcanvas.blocksRaycasts=true;
    }


}


[System.Serializable]
public class ModalSystem
{
    public Queue<ModalWindowHandler.ModalBark>QuedBark=new Queue<ModalWindowHandler.ModalBark>();
    ModalWindowHandler handler;
    public ModalSystem(ModalWindowHandler handler)
    {
        this.handler=handler;
        QuedBark=new Queue<ModalWindowHandler.ModalBark>();
    }

    public void AddBark(ModalWindowHandler.ModalBark bark)
    {
        QuedBark.Enqueue(bark);
    }

    public bool isbarkempty=>QuedBark.Count<=0;

    public ModalWindowHandler.ModalBark getBark()
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


