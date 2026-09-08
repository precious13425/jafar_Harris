using System;
using UnityEngine;

public class DialoguePanel : UIbase
{
    public  System.Action<DialoguePanel>Onvaluechanged;
    public bool fadeout=false;
    public float fadetime=3;
    
     float startval;

    public override void SetDetail(string d, float alpha_s = 1)
    {
        base.SetDetail(d, alpha_s);
        startval=ParentCanvas.alpha;
    }

    public void Callnext(){
        Onvaluechanged?.Invoke(this);
    }

   

    void Update()
    {
        if (!fadeout)
        return;
            if(ParentCanvas.alpha<=0)
            return;
            ParentCanvas.alpha=Mathf.Lerp(startval,-0.1f,fadetime);

        
    }

  

}




