using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollision : MonoBehaviour
{
    Transform earth;
    float radiusEarth = 61f;
    float distance;
    float explosionTimer = 0.6f;
    bool isExploding = false;

    ParticleSystem explosion;

    void Start()
    {
        earth = GameObject.Find("Earth").transform;
        explosion = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        if(earth)
            distance = Vector3.Distance(earth.position, transform.position);

        if (distance <= radiusEarth && isExploding == false)
        {
            Explode();
            SoundManager.instance.PlaySound(1);
        }

        if (isExploding)
        {
            explosionTimer -= Time.deltaTime;

            if (explosionTimer <= 0)
            {
                GameInterface.Instance.satelliteCount -= 1;
                SatelliteList.Instance.satellites.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void Explode()
    {
        isExploding = true;
        
        if(explosion)
            explosion.Play();
    }
}
