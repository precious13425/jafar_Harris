using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//listner for handler to use the current interractable
public class Inspect_Handler : MonoBehaviour
{
    public GameEvent gameEvent;
    public Interract_Handler handler;
    
    
    void Start()
    {
        handler=Interract_Handler.ins;
        gameEvent.Register(UseCurrent);
    }

    private void UseCurrent()
    {
        if (handler)
        {
            if (handler.curselected && handler.isUse_ready)
            {
                handler.curselected.Do_interract();
            }
        }
    }

}
