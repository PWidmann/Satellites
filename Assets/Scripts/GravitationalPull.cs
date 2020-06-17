using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalPull : MonoBehaviour
{
    public GameObject gravityCenterObject;
    public Vector3 startingSpeed;
    public float gravitationalConstant;

    Vector3 movement;

    void Start()
    {
        movement = startingSpeed.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Richtung zur Erde
        Vector3 direction = (gravityCenterObject.transform.position - transform.position).normalized; // Richtung zur Erde mit Länge 1

        //Gravitational Pull (Beschleunigung zum Gravitationscenter)
        float distance = (gravityCenterObject.transform.position - transform.position).magnitude;
        float acceleration = (gravitationalConstant * GameManager.EarthMass) / (distance * distance);
        movement += acceleration * direction * Time.fixedDeltaTime;

        //Position anpassen
        transform.Translate(movement * Time.fixedDeltaTime);
    }

}
