using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUPManager : MonoBehaviour
{
   public static PopUPManager instance;
   

    void Awake()
    {
        if(instance==null)
        instance=this;
    }
    public Transform PopupPrefab;
    public Transform SpawnParent;


    void DoPopUp(string newmsg,Vector3 sp_pos)
    {
        var newpos=Camera.main.WorldToScreenPoint(sp_pos);
       var spawnedprefab= PopupPrefab.SpawnAt(newpos);
        spawnedprefab.parent=SpawnParent;

        //get the tmp pro through ui master
    //    var mesgUi= spawnedprefab.GetComponent<UiMaster>();
    //     if (mesgUi)
    //     {
    //         mesgUi.SetText(newmsg);
    //     }
    
    }


    public static void Do_PopUp(string tipMsg,Vector3 sp_position)
    {
        instance.DoPopUp(tipMsg,sp_position);
    }

}
