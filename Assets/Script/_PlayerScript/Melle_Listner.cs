using UnityEngine;

public class Melle_Listner : MonoBehaviour
{
   [SerializeField] MelleSwing melleSwing;
    public LayerMask layerMask;
    public float attackradius=1;
    public Transform attackpoint;
    public System.Action<Transform> on_HasTarget;

   
    public void DOShoot()
    {
       
     var retval= Physics.OverlapSphere(attackpoint?attackpoint.position:transform.position,attackradius,layerMask);

        if (retval.Length > 0)
        {
            foreach (var enemy in retval)
            {
               melleSwing.HandleTarget(enemy.transform);
               on_HasTarget?.Invoke(enemy.transform);
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