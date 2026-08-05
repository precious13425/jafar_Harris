using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
   	public Animator animator;
	public string ActionName;

	
    public void PlayAction()
    {
        PlayAnyAction(ActionName);
    }

     public void PlayAnyAction(string nm)
    {
        if(isbusy)
        return;

        isbusy=true;
        animator.CrossFade(nm,0.1f);
    }

    bool isbusy;
    public void AnimeEnd()
    {
        isbusy=false;
    }
}
