using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void OnEnable()
    {
        DontDestroyOnLoad(transform.root.gameObject);
    }
}
