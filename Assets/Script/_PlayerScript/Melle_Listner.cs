using UnityEngine;

public class Melle_Listner : MonoBehaviour
{
   [SerializeField] MelleSwing melleSwing;
    public static event System.Action<Transform>OnValueChange;
    public LayerMask layerMask;
    public float attackradius=1;
    public Transform attackpoint;
   
    public void DOShoot()
    {
       
     var retval= Physics.OverlapSphere(attackpoint?attackpoint.position:transform.position,attackradius,layerMask);

        if (retval.Length > 0)
        {
            foreach (var enemy in retval)
            {
               melleSwing.HandleTarget(enemy.transform); 
            }
        }
    }


  
    
     public bool drawgizmos;
    void OnDrawGizmosSelected()
    {
        if(!drawgizmos)
        return;

        Gizmos.color=Color.cyan;
        Gizmos.DrawWireSphere(attackpoint?attackpoint.position:transform.position,attackradius);
    }

}