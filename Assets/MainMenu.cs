using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
   public CanvasGroup fadecanvas;
   public UnityEvent OnPressedPlay,onPressContinue;
   public float fadeduration=3;

   public void PressPlay()
    {
        StopAllCoroutines();
        StartCoroutine(startplay(()=>OnPressedPlay?.Invoke()));
    }

    IEnumerator startplay(System.Action endarg)
    {
        float timer=0;
        fadecanvas.alpha=0;
        while (timer <= fadeduration)
        {
            timer+=Time.deltaTime;
            fadecanvas.alpha+=timer/fadeduration;
            yield return null;
        }
        fadecanvas.alpha=1;
        endarg?.Invoke();
    }


    public void Continue()
    {
       StopAllCoroutines();
        StartCoroutine(startplay(()=>onPressContinue?.Invoke()));
    }
}
