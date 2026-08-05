using UnityEngine;

//extension for interract handler
public class prompt_intrListner : MonoBehaviour
{
    public Interract_Handler handler;
    AimCursor_manager aimCursor_man;
    public Transform PlayerTransform;
    public float min_DIs=2;

    void Start()
    {
        handler=Interract_Handler.ins;
        if (handler)
        {
            handler.OnValueChanged+=HandleChange;
          //  aimCursor_man=handler.aimCursor_Manager;
            PlayerTransform=handler.Playerdata?.GetPlayer;
        }
    }

    void Update()
    {
        HandlePrompt();
    }

    void HandlePrompt()
    {
        if (!handler.curselected || !handler.curselected.isActive)
        {
            HandleChange();
            return;
        }

        if (handler.curselected.requireButton && Vector3.Distance(PlayerTransform.position,handler.curselected.transform.position)<=min_DIs){
           //set the prees e ui prompt
            aimCursor_man.Setprompt();

            //set the variable to allow btn interract
            handler.isUse_ready=true;

           // handler.curselected?.SelectObject();
  
        }

        else
        {
           // handler.curselected?.DeselctObject();
            handler.isUse_ready=false;
            HandleChange();
        }
    }

    void OnDisable()
    {
       
         if(handler)
            handler.OnValueChanged-=HandleChange;
        
    }

    private void HandleChange()
    {
        if (handler.curselected)
        {
           //if(handler.curselected.isActive)
            //aimCursor_man?.SetTarget_UICursor();
        }
        else
        {
            aimCursor_man?.Setnormal_UICursor();
        }
    }


}
