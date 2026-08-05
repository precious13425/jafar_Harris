using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class _EnemyManager : MonoBehaviour,Idamagable
{

    [SerializeField]float healthval;
    public Pooltype energy;
    public UnityEvent OnHurt,OnDead;



    void Start()
    {
        energy=new Pooltype(healthval);
    }



    public Transform GetTransform()
    {
       return transform;
    }

    public void Take_Damage(float val)
    {
       energy.Remove_Value(1);
       OnHurt?.Invoke();
       if(energy.isempty)
       OnDead?.Invoke();
//       Debug.Log(Time.time);
    }
}
