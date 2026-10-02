using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatDamager_COmp : MonoBehaviour,Idamagable
{
   [Header("Hud Variables")]
    public Slider healthslider;
    public TMP_Text healthtext;
    public CanvasGroup healthcanvas;
    BarAnimator barAnimator=new BarAnimator();
   

   float healthval;
    public Pooltype energy;
    [Space]


    public UnityEvent OnHurt,OnDead;
 
   public System.Action OnValChange;
    public Coroutine fadecorutine;


   public void SetUp(float maxhealth)
   {
      
      energy=new Pooltype(maxhealth);
      healthval=maxhealth;
      barAnimator.OnValueChanged+=HandleChange;
      UpdateHealth();
   }

    private void HandleChange()
    {
      healthslider.value=barAnimator.CurrentValue;
    }

    public void Take_Damage(float val)
    {
      if(energy.isempty)
      return;
      
       energy.Remove_Value(1);
       OnHurt?.Invoke();
      if (energy.isempty)
      {
         
       OnDead?.Invoke();
      }

      //send event
      OnValChange?.Invoke();
      UpdateHealth();
      // Debug.Log(Time.time);
    
    }


 public void UpdateHealth()
    {
      if (energy.isempty)
         {
           StopAllCoroutines();
         healthcanvas.alpha=0;
         return;
         //show GameOver
         }


       healthcanvas.alpha=1;
        // healthslider.value=energy.GetnormalizedValue; 
        barAnimator.AnimateTo(energy.GetnormalizedValue,1);
         healthtext.text=$"{energy.getvalue}/{energy.get_MaxValue}";

           
      if (fadecorutine != null)
      {
         StopCoroutine(fadecorutine);
      }
      fadecorutine=StartCoroutine(JfCorutine.LerpCanvas_Alpha(healthcanvas,0,3));


       
    }


    public bool Is_alive()
    {
      return !energy.isempty;
    }


    public Transform GetTransform()
    {
      return transform;
    }
}


