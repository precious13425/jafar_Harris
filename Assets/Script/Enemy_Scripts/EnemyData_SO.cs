using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyData_SO  {
    
    public GameObject Prefab;

    //enemy base variables
    public float basehealth,basedamage,baseSpeed;
    
    //modifies per level of enemy
    public float healthmod,damagemod,speedmod;
    
}


[System.Serializable]
public class Waveentry
{
    public Enemy_SO enemy_SO;
    [SerializeField] int mincount=1,maxcount=3;

    public int getcount()=>Random.Range(mincount,maxcount);
}

[System.Serializable]
public class WaveData
{
    public GameDirector.GameSate gameSate;
    public List<Waveentry>waveentry;
    
}
