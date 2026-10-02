using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpComponent : MonoBehaviour
{
    public float scaleDuration=1,scalespeed;
   [SerializeField] Rigidbody rbody;
   public float startjumpval=3;
   public TMP_Text tMP_Text;

    // Start is called before the first frame update
    void Start()
    {
        rbody.velocity=Vector3.up*startjumpval;
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
       float timer=0;
       while(timer<=scaleDuration){
        timer+=Time.deltaTime;
            transform.localScale-=Vector3.one*timer*scalespeed;
           
                yield break;
            
       }
        DIsableObj();
    }

    private void DIsableObj()
    {
        gameObject.SetActive(false);
    }

    public void Setup(string dd)
    {
        if (tMP_Text)
        {
            tMP_Text.text=$"{dd}";
        }
        transform.localScale=Vector3.one;
    }
}
