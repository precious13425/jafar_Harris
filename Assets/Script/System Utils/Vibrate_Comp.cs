using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate_Comp : MonoBehaviour
{
     Coroutine actionCor;
    [SerializeField] float duration=.5f,timer;
    [SerializeField] float shakeMagnitude=.1f;
    [SerializeField] bool useLocalTransform;
    [SerializeField] bool vibrateX=true,vibrateY,vibrateZ;

    Vector3 originpos;
    [SerializeField]bool isvibrating;

    void OnEnable()
    {
        if (useLocalTransform)
        {
            originpos=transform.localPosition;
        }
        else
        {
            originpos=transform.position;
        }
    }

    public void Doaction(float _duration,float _Magnitude)
    {
        duration=_duration;
        shakeMagnitude=_Magnitude;
        Doaction();
    }

    public void Do_Vibrate()=>Doaction();
   
    void Doaction()
    {
        originpos=transform.position;

        if (!isvibrating)
        {
            if (actionCor != null)
            {
                StopCoroutine(actionCor);
            }
            actionCor=StartCoroutine(DOVibrate());
        }

    }

    private IEnumerator DOVibrate()
    {
         timer=0;
        isvibrating=true;
        while (timer<=duration)
        {
            float movX=vibrateX?Random.Range(-1,1):0;
            float movY=vibrateY?Random.Range(-1,1):0;
            float movZ=vibrateZ?Random.Range(-1,1):0;

            Vector3 newpos=new Vector3(movX,movY,movZ);
            newpos=newpos.normalized*shakeMagnitude;
            if (useLocalTransform)
            {
                transform.localPosition=originpos+newpos;
            }
            else
            {
                transform.position=originpos+newpos;
            }
            timer+=Time.deltaTime;
            yield return null;
        }
        timer=0;
        isvibrating=false;
        actionCor=null;
        ResetPosition();
    }

    private void ResetPosition()
    {
        if (useLocalTransform)
        {
            transform.localPosition=originpos;
        }
        else
        transform.position=originpos;
    }
}
