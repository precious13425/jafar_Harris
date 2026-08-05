using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class Interract_Handler : MonoBehaviour
{
    public RectTransform staticCanvas;
    public Player_SO Playerdata;
    public intr_Comp curselected;
    public Camera mycamera;
    [SerializeField]LayerMask _layerMask;
   
    public float drawdis=10;
    public static Interract_Handler ins;
    public bool isUse_ready;

    [Header("additional subcomponent")]
   // public AimCursor_manager aimCursor_Manager;
    
   // public Ui_promptExtension promptExtension;

    public System.Action OnValueChanged;
    public System.Action<Transform>OnRangeAction;
    
    

   

    void Awake()
    {
        if(ins!=null && ins != this)
        {
            Destroy(this);
            return;
        }
        ins=this;
        mycamera=Camera.main;
    }



    void Update()
    {
        //shoot raycast
        SetCur_Obj(FindCurObj_Raycast());
      
    }

    // public static void RegisterHud(intr_Comp data)
    // {
    //     if(!ins)
    //     return;
    //    if(ins.promptExtension)
    //     ins.promptExtension.allInterracable.Add(data);
       
    // }

    //  public static void UnRegisterHud(intr_Comp cursorObj)
    // {
    //     if(!ins)
    //     return;
  
    //    if(ins.promptExtension)
    //     ins.promptExtension.allInterracable.Remove(cursorObj);
       
    // }

#region  Current selected functions
    intr_Comp FindCurObj_Raycast()
    {
        Ray Ray=new Ray(mycamera.transform.position,mycamera.transform.forward);
        intr_Comp temp_data=null;
        bool success=Physics.Raycast(Ray,out RaycastHit hit,drawdis,_layerMask);
        if (success)
        {
            temp_data=hit.collider.GetComponent<intr_Comp>();
        }
        return temp_data;
    }

     void SetCur_Obj(intr_Comp data)
    {
        if (!data)
        {
            SetCur_null();
            return;
        }
        
        if ( data != ins.curselected)
        {
           

            ins.curselected=data;
            Debug.LogWarning("is selected"+data.name);
            OnValueChanged?.Invoke();
           
        }
    }

     void SetCur_null()
    {
        if (curselected!=null)
        {
           //  Debug.LogWarning("we Unselected"+ins.name);
            ins.curselected.DeselctObject();
            ins.curselected=null;
            OnValueChanged?.Invoke();
        }
    }


    [SerializeField]bool DrawGizmos=true;
    void OnDrawGizmosSelected()
    {
        if(!DrawGizmos)
        return;

        Gizmos.color=Color.yellow;
        Gizmos.DrawLine(mycamera.transform.position,mycamera.transform.position+mycamera.transform.forward*drawdis);
    }

#endregion
}
