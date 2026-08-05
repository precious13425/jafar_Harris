using System.Collections.Generic;
using UnityEngine;

public class Ui_promptExtension : MonoBehaviour
{
    public List<intr_Comp>allInterracable;
    public Camera mycamera=>Camera.main;
    Interract_Handler handler;
    void Start()
    {
        handler=Interract_Handler.ins;
    }


    private void Handlefloating_UI()
    {
       foreach (var data in allInterracable)
       {
           
            var view_arc=mycamera.WorldToViewportPoint(data.transform.position);
            if(view_arc.z>0)
            data.UpdateUi(handler.Playerdata.GetPlayer.position);
            else
            {
                data.HideCursor();
            }
       }
    }

   
   
}
