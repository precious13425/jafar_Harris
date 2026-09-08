using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue_Manager : MonoBehaviour
{
    [Header("Simple Message prompt")]
    public DialoguePanel dialoguevisuals,Full_dialogueMessage;
    public PromptSystem promptSystem;
    public DialogueSystem dialogueSystem;

   
    #region  MessagePrompt
    private void ValueChanged(DialoguePanel panel)
    {
        panel.setalpha(0);
       CueNextDialogue();
    }

    public void SeNewdialogue(string dialogue,System.Action onendDialogue=null)
    {
        promptSystem.SetString(dialogue);
        promptSystem.end_dialogue=onendDialogue;

        dialoguevisuals.SetDetail(promptSystem.cur_Dialogue);

    }

    void CueNextDialogue()
    {

        promptSystem.queNextDialogue();
    }

    #endregion



   
    #region  MvvvessagePrompt
     

    public void SetNewdialogue(DialogueSystem.Dialogue dialogue,System.Action onendDialogue=null)
    {
        dialogueSystem.SetNewDialogue(dialogue);
        dialogueSystem.end_dialogue=onendDialogue;

        SetDialogue();
        
        
    }

private void ValueChanged2(DialoguePanel panel)
    {
        if (dialogueSystem.hasDialogueLines)
        {
           SetDialogue();
        }
       else dialogueSystem.queNextDialogue();

    }

    void SetDialogue()
    {

        

        string dm=dialogueSystem.GetDialgoue_string();
        Full_dialogueMessage.SetDetail(dm);

        //check for exit
    }

    
    #endregion




    public static Dialogue_Manager instance;

    private void OnEnable() {
        instance=this;

        promptSystem=new PromptSystem();
        dialoguevisuals.Onvaluechanged+=ValueChanged;
        dialoguevisuals.setalpha(0);

         dialogueSystem=new DialogueSystem();
        Full_dialogueMessage.Onvaluechanged+=ValueChanged2;
        Full_dialogueMessage.setalpha(0);



    }

    

    public class PromptSystem
    {
        public System.Action end_dialogue;
        public string cur_Dialogue{get;private set;}

        public void SetString(string dm)=>cur_Dialogue=dm;

        public void queNextDialogue()
        {
            //que the next till the last and close dialogue
            CloseDialogue();
        }

        public void CloseDialogue()
        {
            end_dialogue?.Invoke();
        }
    }



    public class DialogueSystem
    {
        public System.Action end_dialogue;
        public Dialogue cur_Dialogue{get;private set;}
        int d_index=0;

        public void SetNewDialogue(Dialogue dm)
        {
            cur_Dialogue=dm;
            d_index=0;
            
        }

        public string GetDialgoue_string()
        {
            string retval="";

            if (cur_Dialogue.all_Lines.Length>=0 && d_index < cur_Dialogue.all_Lines.Length)
            {
               retval=cur_Dialogue.all_Lines[d_index]; 
                d_index++;
            }

            return retval;
        }


        public bool hasDialogueLines=>d_index<cur_Dialogue.all_Lines.Length;

        public void queNextDialogue()
        {
            if (!hasDialogueLines)
            CloseDialogue();
        }

        public void CloseDialogue()
        {
            end_dialogue?.Invoke();
        }

        [Serializable]
        public class Dialogue
        {
            [TextArea(2,4)]
            public string[] all_Lines;
        }


    }




}



public abstract class UIbase:MonoBehaviour
{
    public CanvasGroup ParentCanvas;
   
    public TMP_Text detail_text;

    public virtual void SetDetail(string d,float alpha_s=1)
    {
        detail_text.text=$"{d}";
        setalpha(alpha_s);
    }

    public virtual void setalpha(float alpha_s)
    {
        ParentCanvas.alpha=alpha_s;
    }

   
}


