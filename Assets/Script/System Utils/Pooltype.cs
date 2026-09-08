using System;
using UnityEngine;

[Serializable]
public class Pooltype
{
    [SerializeField]float curVal;
    float Max_Value;

    public float getvalue=>curVal;
   public float getvalue01=>curVal/Max_Value;

    public float get_MaxValue=>Max_Value;
    bool cap_max;
    public Pooltype(float maxval,bool startfull=true)
    {
        Max_Value=maxval;
        curVal=startfull?Max_Value:0;
        cap_max=true;
    }

     public Pooltype(float maxval)
    {
        Max_Value=maxval;
        curVal=Max_Value;
        cap_max=false;
    }

    public float AddValue(float value)
    {
        curVal=cap_max?Mathf.Min(curVal+value,Max_Value):curVal+value;
        return curVal;
    }

    public void Setvalue(float value)
    {
        curVal=value;
    }

     public bool Try_RemoveValue(float value)
    {
        if (curVal >= value)
        {
            Remove_Value(value);
            return true;
        }

        return false;
    }

     public float Remove_Value(float value)
    {
        curVal=Mathf.Max(curVal-value,0);
        return curVal;
    }

    public float SetFull()
    {
        curVal=Max_Value;
        return curVal;
    }

    public float SetEmpoty()
    {
        curVal=0;
        return curVal;
    }

    public bool isempty=>curVal<=0;
    public bool isfull=>curVal>=Max_Value;
}

