using UnityEngine;

public class PlaceatGround:MonoBehaviour
{
   [SerializeField] Transform roottransform;
    [SerializeField] Vector3 offset;
    [SerializeField] LayerMask hitmask;
    public Transform checkposition;

    void Start()
    {
        PlaceGround(roottransform);
    }

    public void PlaceGround(Transform myransform)
    {
        Physics.Raycast(checkposition.position,-Vector3.up,out RaycastHit hit,1000,hitmask);
        if (hit.collider != null)
        {
            myransform.position=hit.point+offset;
        }
    }
}