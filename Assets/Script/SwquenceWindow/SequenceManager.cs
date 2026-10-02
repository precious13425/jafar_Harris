using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
       const string INTRO_SEEN_KEY = "BoundDebt_IntroSeen";

    [SerializeField] GameDirector vnController; // your existing VN system
    [SerializeField] ModalWindowHandler barkPanel; 
    public GameObject actorbox;               // your existing bark system
    //[SerializeField] RoomStateManager roomState;         // fires the Calm→Escalate events

    [SerializeField]bool replay;
   
    public bool donotplay;  //debug to skill buton intro

    private void Awake() {
        if (donotplay)
        {
            OnVNComplete();
            return;
        }
        
          actorbox.SetActive(false);
        if(vnController)vnController.isactive=false;
    
       
    }
    
    void Start()
    {
      
        if (replay)
        {
            replay=false;
             PlayerPrefs.SetInt(INTRO_SEEN_KEY, 0);
        PlayerPrefs.Save();

        }


        if (!HasSeenIntro())
        {
            vnController.isactive=false;
            barkPanel.Callnext();
        }
        else
        {
            OnVNComplete();
        }

        
    }

    void Update()
    {
         Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;

    }

    public void OnVNComplete()
    {
        enabled=false;
        MarkIntroSeen();
        SpawnPlayer();
      
    }

 

    void SpawnPlayer()
    {
        vnController.isactive=true;
        barkPanel.enabled=false;

        actorbox.SetActive(true);

    }

    bool HasSeenIntro() => PlayerPrefs.GetInt(INTRO_SEEN_KEY, 0) == 1;

    void MarkIntroSeen()
    {
        PlayerPrefs.SetInt(INTRO_SEEN_KEY, 1);
        PlayerPrefs.Save();
    }
}



