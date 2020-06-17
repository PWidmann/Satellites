using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static float earthMass = 300f;
    private static int timeScale = 10;
    private static float earthSelfRotationSpeed = 1f;
    private static float moonSelfRotationSpeed = 1f;
    private static float satelliteStartTravelSpeed = 3f;

    public static float EarthMass { get => earthMass; set => earthMass = value; }
    public static int TimeScale { get => timeScale; set => timeScale = value; }
    public static float SatelliteStartTravelSpeed { get => satelliteStartTravelSpeed; set => satelliteStartTravelSpeed = value; }
    public static float EarthSelfRotationSpeed { get => earthSelfRotationSpeed; set => earthSelfRotationSpeed = value; }
    public static float MoonSelfRotationSpeed { get => moonSelfRotationSpeed; set => moonSelfRotationSpeed = value; }
}
