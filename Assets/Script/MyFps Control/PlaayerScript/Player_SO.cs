using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_SO", menuName = "playerData", order = 0)]

public class Player_SO : ScriptableObject
{
    #region  PlayerTransform World object
    [NonSerialized]
    [SerializeField] Transform playerTransform;

    public void NewPlayer(Transform mytransform)
    {
        playerTransform=mytransform;
    }

    public Transform GetPlayer=>playerTransform;

    #endregion

}
