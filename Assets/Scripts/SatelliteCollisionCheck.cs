using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollisionCheck : MonoBehaviour
{
    float checkTimer = 0.5f;
    float distance;
    float satelliteRadius = 5f;

    void Update()
    {
        if (!GameManager.DestructionSequence)
        {
            checkTimer -= Time.deltaTime;

            if (checkTimer <= 0) // Reduce performance hit with bigger checkTimer
            {
                foreach (GameObject satellite in SatelliteList.Instance.satellites)
                {
                    if (satellite != gameObject)
                    {
                        distance = Vector3.Distance(satellite.transform.position, transform.position);

                        if (distance <= satelliteRadius)
                        {
                            SoundManager.instance.PlaySound(1);
                            // Destroy this satellite
                            SatelliteList.Instance.satellites.Remove(gameObject);
                            transform.GetComponent<EarthCollision>().Explode();

                            // Destroy colliding satellite
                            SatelliteList.Instance.satellites.Remove(satellite);
                            satellite.GetComponent<EarthCollision>().Explode();

                            break;
                        }
                    }
                }
            }
        }
    }
}
