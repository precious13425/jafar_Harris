using UnityEngine;

public class GizmosDrawer : MonoBehaviour
{
    
    [SerializeField]bool drawgizmos;
    [SerializeField]float drawsize=0.2f;
    [SerializeField] Color drawcolor=Color.red;
    void OnDrawGizmosSelected()
    {
        if(!drawgizmos)
        return;
        Gizmos.color=drawcolor;
        Gizmos.DrawSphere(transform.position,drawsize);
    }

}