using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressFeedSYstem : Jf_Singleton<ProgressFeedSYstem>
{
    public DebtData debtData;
    public Gamdata_SO gamdata_SO;

    public TMP_Text DebtLevel,DebtCost;
    public Button Pay_btn;


    void Start()
    {
        tryFeed();
        
        if (debtData)
        {
            debtData.Setup();
            debtData.OnDebtChanged+=UpdateUI;

        }

        if (gamdata_SO)
        {
            gamdata_SO.OnCoinChange+=UpdateUI;
        }

        if (Pay_btn)
        {
            Pay_btn.onClick.AddListener(()=>TryPay());
        }

       
        UpdateUI(0);
    }

    private void UpdateUI()
    {
        UpdateUI(0);
    }

    private void TryPay()
    {
        if(!debtData)
        return;

        if(!gamdata_SO)
        return;

       if( debtData.TryPay((int)gamdata_SO.Coinsaved.getvalue))
        {
            gamdata_SO.SpendCoin(debtData.PayCost);
        
        }
    }

    private void UpdateUI(int obj)
    {
       DebtLevel.text=$"Feed Level:{debtData.Level}";
       DebtCost.text=$"Cost:{debtData.PayCost}";
    }

    public void tryFeed()
    {
        if(!debtData)
        return;
        debtData.Feed();
        Debug.Log("hasFeed");
    }



    void OnDestroy()
    {
         if (GameDirector.ins)
        {
            GameDirector.ins.onGameFinished-=tryFeed;
        }

         if (debtData)
        {
            debtData.OnDebtChanged-=UpdateUI;

        }
         if (gamdata_SO)
        {
            gamdata_SO.OnCoinChange-=UpdateUI;
        }
    }


}
