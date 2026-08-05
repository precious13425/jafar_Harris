using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootStepHandler : MonoBehaviour
{
        public UnityEvent onstepListner;

    // handle the footstop sound and delay
        float footstep_et = 0;

    public void PlayFootstepSounds(float delay)
    {
        PlayFootstepSounds(delay,true);
    }
    
    public void PlayFootstepSounds(float delay,bool isgrounded)
    {
           

            if (footstep_et < delay)
                footstep_et += Time.deltaTime;
            else
            {
                footstep_et = 0;
                onstepListner?.Invoke();
                //_audioSource.PlayOneShot(FootstepSounds[Random.Range(0, FootstepSounds.Count)]);
            }
    }
}