using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Cinemachine_Shaker : MonoBehaviour
{
    public static Cinemachine_Shaker instance;
    public bool FadeShake;

    [SerializeField] ShakeProf softhit,Hanrdhit,greatshake;

        public void Dosoft_Shake() => softhit?.DoShake();
        public void DoHard_Shake() => Hanrdhit?.DoShake();
        public void DoEarthQuake() => greatshake?.DoShake();



    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }
    CinemachineVirtualCamera cameraMachine;
    public bool isshaking;

    [SerializeField] float shakePower;
    [SerializeField] float timershake, timershakeTotal;


    public 


    void Start()
    {
        cameraMachine = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (!isshaking)
            return;
        timershake -= Time.deltaTime;
        if (timershake <= 0)
        {
            StopShake();
        }
        if (FadeShake)
        {

            var newdata = cameraMachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            newdata.m_AmplitudeGain = Mathf.Lerp(shakePower, 0, timershake / timershakeTotal);
        }



    }

    private void StopShake()
    {
        var newdata = cameraMachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        newdata.m_AmplitudeGain = 0;
        isshaking = false;
    }
    public void _ShakeCamera(float s_amplitude, float s_timer, bool fade)
    {
        instance.shakePower = s_amplitude;
        instance.timershake = s_timer;
        var newdata = cameraMachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        newdata.m_AmplitudeGain = shakePower;
        isshaking = true;
        FadeShake = fade;
    }

    public static void ShakeCamera(float s_amplitude, float s_timer, bool fade = false)
    {
        instance._ShakeCamera(s_amplitude, s_timer, fade);
    }
}

