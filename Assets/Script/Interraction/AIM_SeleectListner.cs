using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIM_SeleectListner : MonoBehaviour
{
   
    public Color normalcursor,SelectColor;
    public Image Selectcursor;
    Interract_Handler handler;
    void Start()
    {
        Setnormal_UICursor();
        handler=Interract_Handler.ins;
    }

    void Update()
    {
        if (handler.curselected)
        {
            SetTarget_UICursor();
        }
        else
        {
            Setnormal_UICursor();
        }
    }

    
    public void Setnormal_UICursor()
    {
        Selectcursor.color=normalcursor;
    }

     public void SetTarget_UICursor()
    {
        Selectcursor.color=SelectColor;
    }

  
  
}
