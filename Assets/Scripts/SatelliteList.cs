using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteList : MonoBehaviour
{
    public static SatelliteList Instance;

    public List<GameObject> satellites = new List<GameObject>();


    private void Start()
    {
        Instance = this;
    }
}
