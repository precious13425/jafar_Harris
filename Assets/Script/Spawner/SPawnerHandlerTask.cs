using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SPawnerHandlerTask : MonoBehaviour
{
  
    public List<WaveData> AllWaves;
    GameDirector gameDirector;
    WaveData curdata;
   [SerializeField] float curspawncount;
   [SerializeField] Transform[] spawnpoints;



    void  Start() {
       //yield return new WaitForSeconds(1);
        gameDirector=GameDirector.ins;
        gameDirector.OnStateChange+=HandleStateChange;
    }


    //called when director change state
    private void HandleStateChange(GameDirector.GameSate data)
    {
        if(data==GameDirector.GameSate.finished)
        return;
        
        DO_SpawnUnit();
    }

   
   
    //called when script is done with task
   

    Coroutine spawnroutine;
    public void DO_SpawnUnit()
    {
        if(spawnroutine!=null)
        StopCoroutine(spawnroutine);
       spawnroutine=StartCoroutine(SpawnUnit());
        Debug.Log("Count DOne");
    }

    IEnumerator  SpawnUnit()
    {
       
        foreach (var item in AllWaves)
        {
            if(item.gameSate==gameDirector.gameSate)
            {
                curdata=item;
                break;
            }
        }
       
       if(curdata==null)
        yield break;

        curspawncount=0;
        foreach (var item in curdata.waveentry)
        {
            for (int i = 0; i < item.getcount(); i++)
            {
                var dm=_EnemyManager.SpawnPlayer(item.enemy_SO,1);
                dm.position=Getposition;
                curspawncount++;

                yield return null;
            }
        }

        gameDirector.SetKillCount(curspawncount);

    }

    Vector3 Getposition
    {
       
        get
        {
            if( spawnpoints==null||spawnpoints.Length<=0)
            {
                return transform.position;
            }
            return spawnpoints[UnityEngine.Random.Range(0,spawnpoints.Length)].position;
        }
    }

    void OnDestroy()
    {
        gameDirector.OnStateChange-=HandleStateChange;
    }



}
