using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]Player_SO gamedata;
    [SerializeField]Transform playerobj;
    public enum starttype{spawn,reposition}
    public starttype _starttype;
    void Start()
    {
        if(_starttype==starttype.spawn)
      {
        if(gamedata) playerobj=gamedata.SpawnPlayer(transform.position);
      }
       else if (_starttype == starttype.reposition)
        {
            if (playerobj)
            {
                playerobj.position=transform.position;
            }
        }
    }

}
