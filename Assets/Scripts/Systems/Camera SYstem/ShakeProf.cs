using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShakeProf : ScriptableObject
{
    
        public float Shakepower = 1;
        public float shakeTime=0.3f;
        public bool fadeshake;

        public void DoShake(){
        Cinemachine_Shaker.ShakeCamera(Shakepower, shakeTime,fadeshake);
        }
}
