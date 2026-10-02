using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_SO", menuName = "playerData", order = 0)]

public class Player_SO : ScriptableObject
{
    #region  PlayerTransform World object
   
   [SerializeField]GameObject PlayerPrefab;
    Transform playerTransform;
    [SerializeField] float playerLevel=1, Bullet_count_start,Health_Start,gundamage_base=3,axedamage_base=1;
    [SerializeField] float staminastart=10;
    public float axe_StaminaCost=3;
    
    [NonSerialized]public Pooltype health;
    [NonSerialized]public Pooltype current_bulletcount;
    


    public void NewPlayer(Transform mytransform)
    {
        playerTransform=mytransform;
        current_bulletcount=new Pooltype(Bullet_count_start);
        health=new Pooltype(Health_Start,true);
    }

    public Transform GetPlayer=>playerTransform;

    internal Transform SpawnPlayer(Vector3 position)
    {
        if (!PlayerPrefab)
        {
            Debug.LogWarning("Missing player");
            return null;
        }
        
       var player_T=GameObject.Instantiate(PlayerPrefab).transform;
       player_T.position=position;
       return player_T;
    }

    internal float getGunDamage()
    {
       return gundamage_base+axeMultiplier();
    }

    private float axeMultiplier()
    {
        return 3.5f*Getlevel();
    }

    internal float GetAxeDamage()
    {
        return axedamage_base+GunMultiplier();
    }

    private float GunMultiplier()
    {
        return 2*Getlevel();
    }

    public float Getlevel()
    {
        return playerLevel;
    }

     public void SetLevel(float val)
    {
        playerLevel=val;
    }

     public float getStamina()
    {
        return staminastart+(playerLevel*3);
    }
    #endregion

}
