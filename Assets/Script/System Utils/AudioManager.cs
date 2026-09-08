using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSystem audiosystem;

    public static AudioManager instance;

    void Awake()
    {
        instance=this;
        if (audiosystem.globalaudio == null)
        {
            audiosystem.globalaudio=GetComponent<AudioSource>();
        }
    }


    public static void PlaySound(AudioClip myclip)=>instance?.audiosystem.PlaySound(myclip);
   

    [System.Serializable]
public class AudioSystem
{
    public AudioSource globalaudio,AmbientSource;
    public AudioClip ButtonSelect,ButtonAccept;
    [Range(0,1)]
    public float volume=.3f;


    public void PlayBtnSelect()=>PlaySound(ButtonSelect);
    public void playBtnAccept()=>PlaySound(ButtonAccept);
    public void PlaySound(AudioClip data)
    {
        globalaudio.pitch=UnityEngine.Random.Range(0.9f,1.1f);
        globalaudio.PlayOneShot(data,volume);
    }

    
}


}
