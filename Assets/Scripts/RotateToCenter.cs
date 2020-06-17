using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCenter : MonoBehaviour
{
    public Transform directionPoint;
    Vector3 earth = new Vector3(0, 0, 0);

    void FixedUpdate()
    {
        transform.LookAt(earth);
    }
}
