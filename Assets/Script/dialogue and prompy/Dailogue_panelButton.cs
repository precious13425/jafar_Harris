using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dailogue_panelButton : UIbase,IPointerClickHandler,ISubmitHandler
{
        public  System.Action<Dailogue_panelButton>Onvaluechanged;

   public override void SetDetail(string d, float alpha_s = 1)
    {
        base.SetDetail(d, alpha_s);
    }

    public void Callnext(){
        Onvaluechanged?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       Callnext();
    }

    public void OnSubmit(BaseEventData eventData)
    {
       Callnext();
    }
}
