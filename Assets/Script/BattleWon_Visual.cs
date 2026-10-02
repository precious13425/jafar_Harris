using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleWon_Visual : MonoBehaviour
{
    public Gamdata_SO gamdata_SO;
    public Player_SO player;
    public CanvasGroup parent_canvas;
    

[Header("Progression system")]
    public Slider levelupslider;
    public TMP_Text  Leveltext,coinLeveluptext;
    public Progression_data progression_Data ;
    public Button levelup_Btn;


    void Start()
    {
        if (progression_Data)
        {
            progression_Data.Setup(1);
            progression_Data.OnLevelChanged+=UpdaUi;
        }

        if (levelup_Btn)
        {
            levelup_Btn.onClick.AddListener(() =>
            {
                TryLevelUp();
            });
        }

        if (gamdata_SO)
        {
            gamdata_SO.OnCoinChange+=UpdateHud;
        }
    }

    private void UpdateHud()
    {
         gamdata_SO.SaveCoin();
        Leveltext.text=$"Lv {player.Getlevel()}";
        coinLeveluptext.text=$"{gamdata_SO.Coinsaved.getvalue}/{progression_Data.PayCost}";
        levelupslider.value=gamdata_SO.Coinsaved.getvalue/progression_Data.PayCost;
    }

    private void UpdaUi(int obj)
    {
        UpdateHud();
    }

    public void HandleShow()
    {
        UpdateHud();
        parent_canvas.alpha=1;
        Gamdata_SO.isaggro=false;
        //healthtext.text=$"Hp {player}"
    }

    public void TryLevelUp()
    {
        if (progression_Data.TryPay((int)gamdata_SO.Coinsaved.getvalue))
        {
            gamdata_SO.SpendCoin(progression_Data.PayCost);
            progression_Data.LevelUp();
            UpdateHud();
            player.SetLevel(progression_Data.Level);
        }

       
    }


    private void OnDestroy() {
         if (gamdata_SO)
        {
            gamdata_SO.OnCoinChange-=UpdateHud;
        }
    }

}

