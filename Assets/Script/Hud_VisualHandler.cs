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
    BarAnimator barAnimator=new BarAnimator();
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

        GameDirector.ins.OnStateChange+=HandleStateChange;
        gamdata_SO.OnBulletChange+=BulletChanged;
        gamdata_SO.OnCoinChange+=coinchange;
        barAnimator.OnValueChanged+=UpdateHealth;


        HandleStateChange(GameDirector.GameSate.calm);
        BulletChanged();
        coinchange();
        barAnimator.AnimateTo(1,.5f);

      

    }

  

    private void coinchange()
    {
         if(isover)
        return;

       
        
        if(gamdata_SO)
        cointext.SetDetail($" {gamdata_SO.Coinsaved.getvalue}");
    }

    private void BulletChanged()
    {
        if (player_so)
        {

        if(player_so.current_bulletcount!=null)  bulletText.SetDetail($"Bullets: {player_so.current_bulletcount.getvalue}");
        }
    }


    // void Update()
    // {
    //     UpdateHealth();
    // }


    
    private void HandleHurt()
    {
        if (DamageHud)
        {
            DamageHud.alpha=1;
           if(healthfade!=null)
           barAnimator.AnimateTo(player_so.health.GetnormalizedValue,0.5f);

            StopAllCoroutines();
           healthfade=StartCoroutine(JfCorutine.LerpCanvas_Alpha(DamageHud,0,fadetime));
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


    public void UpdateHealth()
    {
        //healthslider.value=player_so.health.GetnormalizedValue; 
        healthslider.value=barAnimator.CurrentValue;
         healthtext.text=$"{player_so.health.getvalue}/{player_so.health.get_MaxValue}";

            if (player_so.health.isempty)
            {
              isover=true;   
           // Show_Information("You DIed");
            gamdata_SO.PauseGame();
            StaticUpdater.DelayedCall(() =>
            {
                
             if (gameovercvanvas)
            {
                gameovercvanvas.alpha=1;
                gameovercvanvas.blocksRaycasts=true;
                gameovercvanvas.interactable=true;
            }
            },
            3);
            //show GameOver
        }
    }

 private void HandleStateChange(GameDirector.GameSate sate)
    {
        switch (sate)
        {
            case GameDirector.GameSate.calm:
                break;
            case GameDirector.GameSate.excalation:
            Show_Information("THe Spirits are Coming");
                break;
            case GameDirector.GameSate.climax:
            Show_Information("more Spirits are Coming");
                break;
            case GameDirector.GameSate.boss:
            Show_Information("THe Champion is coming");
                break;
        }
    }


    void OnDestroy()
    {
        OnPlayerHurt.UnRegister(HandleHurt);
        GameDirector.ins.OnStateChange-=HandleStateChange;

        gamdata_SO.OnBulletChange-=BulletChanged;
        gamdata_SO.OnCoinChange-=coinchange;
        barAnimator.OnValueChanged-=UpdateHealth;

    }
}
