using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Hud_VisualHandler : MonoBehaviour
{
   [SerializeField] Player_SO player_so;
   [SerializeField]Gamdata_SO gamdata_SO;
   
   
   //for the bullet visual
   public DialoguePanel bulletText,cointext;

[Header("Game over and world info")]
   public CanvasGroup Information_UI;
   public TMP_Text Information_text;
   public float wait_time=3;

    [Header("Playerhealth")]
    public CanvasGroup DamageHud;
    public float fadetime=3;
     public GameEvent OnPlayerHurt;


    public Slider healthslider;
    public TMP_Text healthtext;
   [SerializeField] bool isover;


   public CanvasGroup gameovercvanvas;
    Coroutine healthfade;
    IEnumerator Start()
    {
        yield return null;
        //Show_Information("Wave Started");

        OnPlayerHurt.Register(HandleHurt);

        TaskManager.ins.OnStateChange+=HandleStateChange;
        HandleStateChange(TaskManager.GameSate.calm);

    }

    private void HandleHurt()
    {
        if (DamageHud)
        {
            DamageHud.alpha=1;
           if(healthfade!=null)
            StopCoroutine(healthfade);
           healthfade=StartCoroutine(JfCorutine.LerpValue2(DamageHud,0,fadetime));
        }
    }

    public void Show_Information(string v)
    {
        Information_UI.alpha=1;
        Information_text.text=$"[{v}]";
        Invoke(nameof(DisableInformation),wait_time);
         
    }

    void DisableInformation()
    {
        Information_UI.alpha=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isover)
        return;

        if (player_so)
        {

         bulletText.SetDetail($"Bullets: {player_so.current_bulletcount.getvalue}");
            UpdateHealth();
        }
        
        if(gamdata_SO)
        cointext.SetDetail($" {gamdata_SO.Coin.getvalue}");
    }


    public void UpdateHealth()
    {
         healthslider.value=player_so.health.getvalue01; 
         healthtext.text=$"{player_so.health.getvalue}/{player_so.health.get_MaxValue}";

            if (player_so.health.isempty)
            {
              isover=true;   
           // Show_Information("You DIed");
            gamdata_SO.PauseGame();
            if (gameovercvanvas)
            {
                gameovercvanvas.alpha=1;
                gameovercvanvas.blocksRaycasts=true;
                gameovercvanvas.interactable=true;
            }
            //show GameOver
        }
    }

 private void HandleStateChange(TaskManager.GameSate sate)
    {
        switch (sate)
        {
            case TaskManager.GameSate.calm:
                break;
            case TaskManager.GameSate.excalation:
            Show_Information("THe Spirits are Coming");
                break;
            case TaskManager.GameSate.climax:
                Show_Information("THe Champion is coming");
                break;
            case TaskManager.GameSate.end:
                break;
        }
    }


    void OnDestroy()
    {
        OnPlayerHurt.UnRegister(HandleHurt);
        TaskManager.ins.OnStateChange-=HandleStateChange;

    }
}
