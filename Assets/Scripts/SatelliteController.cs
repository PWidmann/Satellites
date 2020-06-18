using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    public float travelSpeed; // public zum später realtime anpassen

    public Vector3 startingDirection = new Vector3(0, 0, 1);
    Vector3 gravityCenterLocation = new Vector3(0, 0, 0);
    float startTravelSpeed;
    float gravityCenterMass = GameManager.EarthMass;
    float gravitationalConstant = 1;
    Vector3 direction;
    float distance;
    float acceleration;
    Vector3 movement;

    public bool launch = false;

    void Start()
    {
        movement = startingDirection.normalized;
        startTravelSpeed = GameManager.SatelliteStartTravelSpeed;
        travelSpeed = startTravelSpeed;
    }

    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (!GameInterface.Instance.earthDestructed)
        {
            if (launch)
            {
                // Momentane Erdmasse anpassen
                gravityCenterMass = GameManager.EarthMass;

                // Richtung zur Erde mit Länge 1
                direction = (gravityCenterLocation - transform.position).normalized;

                //Gravitational Pull (Beschleunigung zum Gravitationszenter)
                distance = (gravityCenterLocation - transform.position).magnitude;
                acceleration = (gravitationalConstant * gravityCenterMass) / (distance * distance);
                movement += acceleration * direction * Time.fixedDeltaTime;

                //Position anpassen
                transform.Translate(movement * travelSpeed * Time.fixedDeltaTime);
            }
            else
            {
                movement = startingDirection.normalized;
            }
        }
    }
}
