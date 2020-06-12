using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    Vector3 gravityCenterLocation = new Vector3(0, 0, 0);
    Vector3 startingSpeed = new Vector3(0, 0, 1);
    float gravityCenterMass = 100;
    float gravitationalConstant = 1;

    Vector3 movement;

    void Start()
    {
        movement = startingSpeed.normalized;
    }

    
    void FixedUpdate()
    {
        // Richtung zur Erde
        Vector3 direction = (gravityCenterLocation - transform.position).normalized; // Richtung zur Erde mit Länge 1

        //Gravitational Pull (Beschleunigung zum Gravitationscenter)
        float distance = (gravityCenterLocation - transform.position).magnitude;
        float acceleration = (gravitationalConstant * gravityCenterMass) / (distance * distance);
        movement += acceleration * direction * Time.fixedDeltaTime;
        
        //Position anpassen
        transform.Translate(movement * Time.fixedDeltaTime);
    }
}
