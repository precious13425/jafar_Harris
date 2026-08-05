using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//extended class of interract handler
// handles the center cursor and press e  prompt
public class AimCursor_manager : MonoBehaviour
{
    public GameObject normalcursor,prompt_Ui;
    public TMP_Text btn_prompttext;

    void Start()
    {
        Setnormal_UICursor();
        
    }
    public void Setnormal_UICursor()
    {
        HideallCursor();
        normalcursor?.SetActive(true);
    }

     public void SetTarget_UICursor()
    {
        HideallCursor();
       // targetcursor?.SetActive(true);
    }

    public void Setprompt(string newstring = "Inspect")
    {
       HideallCursor();
       prompt_Ui?.SetActive(true); 
       btn_prompttext.text=$"[ {newstring} ]";
    }

   
      

   void HideallCursor()
    {
      // targetcursor?.SetActive(false);
        normalcursor?.SetActive(false); 
        prompt_Ui?.SetActive(false); 
    }
}
