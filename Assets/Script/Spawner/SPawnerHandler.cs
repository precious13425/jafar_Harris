using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPawnerHandler : MonoBehaviour
{
    public SpawnData[] all_spawn;
   [SerializeField] List<Transform>SpawnedEnemy=new List<Transform>();
    [SerializeField]float activeenemies=0;

    [Serializable]
    public class SpawnData
    {
        public Enemy_SO enemy_SO;
        public float dataLevel;
        public Transform spawnpoints;

        
    }

    IEnumerator  Start() {
       yield return new WaitForSeconds(3);
        SpawnUnit(5);
        _EnemyManager.OnEnemyKilled+=handleKilled;
    }

    private void handleKilled(int obj)
    {
        activeenemies--;
    }

    public void DO_SpawnUnit()
    {
        SpawnUnit(7);
    }
    
    private void SpawnUnit(int v)
    {
        int sp=0;

        while (activeenemies < v)
        {
           

            Enemy_SO data_T=all_spawn[sp].enemy_SO;
            float level_T=all_spawn[sp].dataLevel;
            var spawnpos_T=all_spawn[sp].spawnpoints;
            Transform spawndT;

            spawndT=_EnemyManager.SpawnPlayer(data_T,level_T);
            spawndT.position=spawnpos_T.position;

            if (spawndT != null)
            {
                SpawnedEnemy.Add(spawndT);
                sp++;
                activeenemies++;
            }

             if (sp > all_spawn.Length-1)
            {
                sp=0;
            }
        }
    }


}
