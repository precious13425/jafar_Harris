using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPawnerHandlerTask : MonoBehaviour
{
   [SerializeField] bool spawnStart;
    public SpawnData[] all_spawn;
   [SerializeField] List<Transform>SpawnedEnemy=new List<Transform>();
    [SerializeField]float activeenemies=0,spawncount=5,timebtwSpawns=1;


    [Serializable]
    public class SpawnData
    {
        public Enemy_SO enemy_SO;
        public float dataLevel;
        public Transform spawnpoints;

        
    }

    void  Start() {
       //yield return new WaitForSeconds(1);
        _EnemyManager.OnEnemyKilled+=handleKilled;
        TaskManager.ins.OnStateChange+=HandleStateChange;
    }


    void Update()
    {
        if(!spawnStart)
        return;
        
        DO_SpawnUnit();
        
        
    }


    private void HandleStateChange(TaskManager.GameSate data)
    {
        Debug.LogWarning("Fumloaai");
        switch(data){
            case TaskManager.GameSate.excalation:
            spawnStart=true;
            spawncount=3;
            break;

            case TaskManager.GameSate.climax:
            spawncount=6;
            spawnStart=true;
            break;

            case TaskManager.GameSate.calm:
            spawnStart=false;
            break;
        }
    }

   


    private void handleKilled(int obj)
    {
        activeenemies--;
        activeenemies=Mathf.Max(0,activeenemies);

        if (activeenemies < 1)
        {
            spawnStart=true;
        }   
    }

    Coroutine spawnroutine;
    public void DO_SpawnUnit()
    {
        if(spawnroutine!=null)
        StopCoroutine(spawnroutine);
       spawnroutine=StartCoroutine(SpawnUnit((int)spawncount));
        Debug.Log("Count DOne");
    }

    IEnumerator  SpawnUnit(int v)
    {
        

        int sp=0;

        while (activeenemies < v)
        {
           
             Debug.LogWarning("Count DOne");
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
            yield return new WaitForSeconds(timebtwSpawns);
        }
        spawnStart=false;
    }


}
