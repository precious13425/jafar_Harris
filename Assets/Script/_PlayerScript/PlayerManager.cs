using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,Idamagable
{
    public Player_SO playerdata;
    public Gamdata_SO gamdata_SO;

    public GameEvent OnPlayerHurt;
    
    public float bulletcount;
    public Shoot_Component shoot_Component;
    public PlayerControl control;

    //mellee swing
    public GameEvent mellee_attack;
    public MelleSwing melleSwing;

    //shootco
    public GameEvent range_attack;
    public Shoot_Component gunShoot;


    void Awake()
    {
        playerdata?.NewPlayer (transform);
        gamdata_SO?.newGameRun();
    }


    void Start()
    {
        mellee_attack?.Register(melleSwing.DOShoot);
        range_attack?.Register(gunShoot.DOShoot);

         melleSwing.SetUp(HandleTarget_Axe);
        gunShoot.SetUp(HandleTarget_Gun);
    }

    private void HandleTarget_Axe(Transform target)
    {
    //check if target is a valid enemy
       var result=DamageHandler.CheckDamage(target);
        if (!result.isvalid)
            return;
            
         //check if target exist
        if(result.target!=null)
            result.target.Take_Damage(playerdata.getGunDamage());
        
      //if the enemy is killed adds coin to player
        if( !result.target.Is_alive())
        {
            float coinreward=GetCoin(target);
            float bulletreward=GetBullet(target);
           gamdata_SO.Coin.AddValue(coinreward);
           playerdata.current_bulletcount.AddValue(bulletreward); 
        }
     
    }

     public void HandleTarget_Gun(Transform target)
    {
        
       var result=DamageHandler.CheckDamage(target);
        if (!result.isvalid)
        return;

        
        result.target.Take_Damage(playerdata.getGunDamage());
        
        Debug.LogWarning("Do_Dmg");

        
        //if the enemy is killed adds coin to player
        if( !result.target.Is_alive())
        {
            float coinreward=GetCoin(target);
           gamdata_SO.Coin.AddValue(coinreward); 
        }
    }


    float GetCoin(Transform target)
    {
        if (target.GetComponent<_EnemyManager>())
        {
           float coinval=target.GetComponent<_EnemyManager>().GetRewardval().coin; 
           return coinval;
        }
        //if the enemy is killed adds coin to player
        
        return 1;
    }

    float GetBullet(Transform target)
    {
        if (target.GetComponent<_EnemyManager>())
        {
           float bulletamount=target.GetComponent<_EnemyManager>().GetRewardval().bullet_Mod*5; 
           return bulletamount;
        }
        //if the enemy is killed adds coin to player
        
        return 2;
    }



    public Transform GetTransform()
    {
       return transform;
    }

    public void Take_Damage(float val)
    {
        if(playerdata.health.isempty)
        return;
        
        playerdata.health.Remove_Value(1);
        OnPlayerHurt?.OnRaise();
    }

    public bool Is_alive()
    {
        return !playerdata.health.isempty;
    }

    public void Revive()
    {
       playerdata.health.SetFull();
    }


    void OnDestroy()
    {
        mellee_attack?.UnRegister(melleSwing.DOShoot);
        range_attack?.UnRegister(gunShoot.DOShoot);

    }
}
