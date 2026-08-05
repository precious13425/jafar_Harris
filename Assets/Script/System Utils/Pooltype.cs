using System;
using UnityEngine;

[Serializable]
public class Pooltype
{
    [SerializeField]float curhealth;
    float maxhealth;

    float getvalue01=>curhealth/maxhealth;

    public Pooltype(float maxval,bool startfull=true)
    {
        maxhealth=maxval;
        curhealth=startfull?maxhealth:0;
    }

    public float AddValue(float value)
    {
        curhealth=Mathf.Min(curhealth+value,maxhealth);
        return curhealth;
    }

     public bool Try_RemoveValue(float value)
    {
        if (curhealth >= value)
        {
            Remove_Value(value);
            return true;
        }

        return false;
    }

     public float Remove_Value(float value)
    {
        curhealth=Mathf.Max(curhealth-value,0);
        return curhealth;
    }

    public float SetFull()
    {
        curhealth=maxhealth;
        return curhealth;
    }

    public float SetEmpoty()
    {
        curhealth=0;
        return curhealth;
    }

    public bool isempty=>curhealth<=0;
    public bool isfull=>curhealth>=maxhealth;
}


[Serializable]
public class Timer_
{
    [SerializeField]float cur_Val;
    float Max_Val;
    bool countdown;

    float getvalue01=>cur_Val/Max_Val;

    public Timer_(float maxval,bool count_Down=true)
    {
        Max_Val=maxval;
        cur_Val=count_Down?Max_Val:0;
    }

    public bool UpdateValue(float value)
    {
        float changeval=countdown?value*-1:value;
        cur_Val=Mathf.Min(cur_Val+changeval,Max_Val);
        
        return Has_Elasped();
    }

    public bool Has_Elasped()
    {
        if (countdown)
        {
            return isempty;
        }
       
            return isfull;
        

    }
       
    public void Reset_TImer()
    {
        if (countdown)
        {
            SetFull();
        }
        else
        {
            SetEmpty();
        }
    }

    public float SetFull()
    {
        cur_Val=Max_Val;
        return cur_Val;
    }

    public float SetEmpty()
    {
        cur_Val=0;
        return cur_Val;
    }

    public bool isempty=>cur_Val<=0;
    public bool isfull=>cur_Val>=Max_Val;
}
