using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    Transform spawnMesh;

    public float satelliteHeightMultiplier = 2;
    private Vector3 gravityCenter = new Vector3(0, 0, 0);

    void Start()
    {
        spawnMesh = spawnObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000.0f))
            {
                if (hit.transform.name == "Earth")
                {
                    GameObject satellite = Instantiate(spawnObject, hit.point, Quaternion.identity);
                    satellite.transform.parent = GameObject.Find("SatelliteContainer").transform;
                    
                    Vector3 spawnPosition = (hit.point - gravityCenter) * satelliteHeightMultiplier;
                    
                    satellite.transform.position = spawnPosition;
                    GameInterface.Instance.satelliteCount += 1;
                    Debug.Log("Spawned a satellite...");
                }
            }
        }
    }
}
