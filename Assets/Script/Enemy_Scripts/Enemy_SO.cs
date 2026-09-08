using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu]
public class Enemy_SO : ScriptableObject
{
     #region  PlayerTransform World object
   
   public GameObject PlayerPrefab;
    public int unitid;
    [SerializeField] float Enemy_Attack_Dmg,Health_Start;
    
    public Pooltype GEt_health(float level)
    {
        
            return new Pooltype(Health_Start+level*2);
        
    }

    public float Get_AttackValue(float level)
    {
        
        return Enemy_Attack_Dmg+level;
        
    }
    

    #endregion

}
