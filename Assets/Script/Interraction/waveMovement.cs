using System;
using UnityEngine;

public class waveMovement:MonoBehaviour
{
    public float heightmax,speed;
    public enum Movetype{wavecurve,animcurve}
    public Movetype movetype;
    [SerializeField]AnimationCurve animcurve;
    float ypos=0;
    void Update()
    {
        switch (movetype)
        {
         case Movetype.wavecurve  :
            ypos= Mathf.Sin(Time.time*speed)*heightmax;
        break;
        case Movetype.animcurve:
            ypos=animcurve.Evaluate(Time.deltaTime)*heightmax;
        break;
        }
        transform.localPosition=new Vector3(transform.localPosition.x,ypos,transform.localPosition.z);
    }
}
