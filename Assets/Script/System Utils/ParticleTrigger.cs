using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    ParticleSystem myparticle;
    void Awake()
    {
        myparticle=GetComponent<ParticleSystem>();
        if (myparticle)
        {
            myparticle.Stop();
        }
    }


    public void StartVFX(){
        StopVFX();
        myparticle.gameObject.SetActive(true);
        myparticle?.Play();
    }
    public void StopVFX()=>myparticle?.gameObject.SetActive(false);
}

