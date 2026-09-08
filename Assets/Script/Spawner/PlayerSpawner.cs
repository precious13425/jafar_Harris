using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]Player_SO gamedata;
    

    void Start()
    {
        gamedata.SpawnPlayer(transform.position);
    }

}
