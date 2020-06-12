using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCenter : MonoBehaviour
{
    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.LookAt(new Vector3(0, 0, 0));
    }
}
