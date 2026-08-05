using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface Iintr_Comp
{
    Transform getTransform { get; }

    void Deactiveinterractable();
    void Do_interract();
    void HideCursor();
    void SelectObject();
  
    void ShowCursor();
}


//interface  for each word iteract object
public class intr_Comp : MonoBehaviour, Iintr_Comp
{

    public bool isActive = true,requireButton=true;
    [SerializeField] Transform _cursor;

    [SerializeField] UnityEvent OnInterract;
     public float distance_cutoff=5; //distance to show or hide ui


    public Transform getTransform => transform;

    public void Setup()
    {

        ShowCursor();
       //if(_cursor) Interract_Handler.RegisterHud(this);
       
    }


    void Start()
    {
        Setup();
    }


    
    public void Do_interract()
    {
        if (!isActive)
            return;

        HideCursor();
        OnInterract?.Invoke();
    }

#region  Show visual UI
    public void ShowCursor()
    {
        if (!isActive)
            return;

       if(_cursor) _cursor.gameObject.SetActive(true);
    }

    public void HideCursor()
    {

       if(_cursor) _cursor.gameObject.SetActive(false);
    }


    public void SelectObject()
    {
        HideCursor();

    }

    internal void DeselctObject()
    {
        ShowCursor();
    }
    public void Deactiveinterractable()
    {
        isActive = false;
       // Interract_Handler.UnRegisterHud(this);
        HideCursor();
    }

    #endregion

    public void UpdateUi(Vector3 targetpos)
    {
        if(!_cursor)
        return;

        float mydis=Vector3.Distance(transform.position,targetpos);
    
        if (mydis > distance_cutoff)
        {
            HideCursor();
        }
        else
        {
            ShowCursor();
        }
    }


}
