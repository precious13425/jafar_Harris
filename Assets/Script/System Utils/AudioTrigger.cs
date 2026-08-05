using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger:MonoBehaviour
{
     [Header("Sounds")]
    [SerializeField] List<AudioClip> allsounds;
    
    public void Playsound()
    {
        if (allsounds.Count <= 0)
        {
         Debug.Log("No sound here");   
        return;
        }

        var finalsound=allsounds[Random.Range(0,allsounds.Count)];
        AudioManager.PlaySound(finalsound);
    }
}
