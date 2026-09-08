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
    [SerializeField] float Bullet_count_start,Health_Start,gundamage=3,axedamage=1;
    
    [NonSerialized]public Pooltype health;
    [NonSerialized]public Pooltype current_bulletcount;
    


    public void NewPlayer(Transform mytransform)
    {
        playerTransform=mytransform;
        current_bulletcount=new Pooltype(Bullet_count_start);
        health=new Pooltype(Health_Start,true);
    }

    public Transform GetPlayer=>playerTransform;

    internal void SpawnPlayer(Vector3 position)
    {
       var player_T=GameObject.Instantiate(PlayerPrefab).transform;
       player_T.position=position;
    }

    internal float getGunDamage()
    {
       return gundamage;
    }

    internal float GetAxeDamage()
    {
        return axedamage;
    }
    #endregion

}
