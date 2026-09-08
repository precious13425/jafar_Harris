using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_BaseComp : MonoBehaviour
{
    
    public CanvasGroup ParentCanvas;
    public Image Icon;
    public Slider slider;
    public TMP_Text detail_text;

    public void SetDetail(string d,float alpha_s=1)
    {
        detail_text.text=$"{d}";
        setalpha(alpha_s);
    }

    private void setalpha(float alpha_s)
    {
        ParentCanvas.alpha=alpha_s;
    }

    public void SetDetail(Sprite d,float alpha_s=1)
    {
        Icon.sprite=d;
        setalpha(alpha_s);
    }
    public void SetDetail(float slideval,float alpha_s=1)
    {
        slider.value=slideval;
        setalpha(alpha_s);
    }
}



