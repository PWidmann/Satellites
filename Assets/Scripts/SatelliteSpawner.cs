using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] GameObject arrowObject;
    //Transform satelliteMesh;
    
    
    private Vector3 gravityCenter = Vector3.zero;
    RaycastHit hit;
    Vector3 spawnPosition;
    Vector3 startingDirection;

    bool isSatellitePlaced = false;
    //bool isSetStartingDirection = false;
    bool arrowActive = false;
    float pointerAngle;
    Vector3 pointerRotationAxis;
    Transform directionPoint;
    Vector3 arrowDirection;

    GameObject satellite;
    GameObject arrow;

    void Start()
    {
        arrow = Instantiate(arrowObject, new Vector3(0, 0, 0), Quaternion.identity);
        arrow.SetActive(false);
    }

    void Update()
    {
        // First left mouse button to place satellite in orbit
        if (Input.GetMouseButtonDown(0) && isSatellitePlaced == false)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000.0f))
            {
                if (hit.transform.name == "Earth")
                {
                    SoundManager.instance.PlaySound(2);
                    spawnPosition = (hit.point - gravityCenter) * GameManager.SatelliteStartHeight;
                    pointerRotationAxis = (hit.point - gravityCenter);
                    satellite = (GameObject)Instantiate(spawnObject, spawnPosition, Quaternion.identity);
                    
                    directionPoint = satellite.GetComponentInChildren<RotateToCenter>().directionPoint;

                    satellite.transform.parent = GameObject.Find("SatelliteContainer").transform;
                    //satelliteMesh = satellite.transform.GetChild(0);

                    isSatellitePlaced = true;
                    arrowActive = true;
                }
            }
        }

        // Display direction arrow on satellite
        if (arrowActive && satellite)
        {
            arrow.SetActive(true);
            pointerAngle = Input.GetAxis("Mouse X") * 3;

            directionPoint.transform.RotateAround(spawnPosition, pointerRotationAxis, pointerAngle);

            arrow.transform.position = directionPoint.transform.position;
            
            arrowDirection = spawnPosition - directionPoint.transform.position;
            arrow.transform.rotation = Quaternion.LookRotation(spawnPosition - directionPoint.transform.position);

            satellite.GetComponent<SatelliteController>().startingDirection = -arrowDirection;

            if (Input.GetMouseButtonUp(0))
            {
                SoundManager.instance.PlaySound(2);
                satellite.GetComponent<SatelliteController>().launch = true;
                satellite.AddComponent<SatelliteCollisionCheck>();
                SatelliteList.Instance.satellites.Add(satellite);
                GameInterface.Instance.satelliteCount += 1;


                arrowActive = false;
                isSatellitePlaced = false;
            }
        }
        else
        {
            arrow.SetActive(false);
        }
    }
}
