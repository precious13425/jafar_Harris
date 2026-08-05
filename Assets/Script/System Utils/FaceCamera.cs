using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camT;
    void OnEnable()
    {
        camT=Camera.main.transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 rotdir=camT.position-transform.position;
        transform.forward=-rotdir;
    }
}
