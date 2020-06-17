using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public static GameInterface Instance;

    // Timescale
    [SerializeField] Slider timeScaleSlider;
    [SerializeField] Text timeScaleValue;
    private static float timeScale;
    [SerializeField] GameObject timeScaleInfo;


    // Earth
    [SerializeField] Slider earthMassSlider;
    [SerializeField] Text earthMassValue;
    [SerializeField] Slider earthSelfRotationSlider;
    [SerializeField] Text earthSelfRotationValue;
    private static float earthMass;

    // Moon
    [SerializeField] Slider moonSelfRotationSlider;
    [SerializeField] Text moonSelfRotationValue;

    // Settings Menu
    [SerializeField] GameObject settingsPanel;
    private bool isInSettings = false;

    // Tutorial
    [SerializeField] GameObject tutorialPanel;
    private bool tutorialActive = true;

    // Satellite
    [SerializeField] Text satelliteCounterText;
    public int satelliteCount = 0;
    [SerializeField] Slider satelliteStartTravelSpeedSlider;
    [SerializeField] Text satelliteStartTravelSpeedValue;

    private void Awake()
    {
        Instance = this;
        timeScale = GameManager.TimeScale;
        settingsPanel.SetActive(false);
        earthMassSlider.value = GameManager.EarthMass;
        earthSelfRotationSlider.value = GameManager.EarthSelfRotationSpeed;
        moonSelfRotationSlider.value = GameManager.MoonSelfRotationSpeed;
        satelliteStartTravelSpeedSlider.value = GameManager.SatelliteStartTravelSpeed;
    }

    private void Update()
    {
        timeScale = timeScaleSlider.value;
        Time.timeScale = timeScale;

        // Speichere Values von den Slidern
        GameManager.EarthMass = earthMassSlider.value;
        GameManager.EarthSelfRotationSpeed = earthSelfRotationSlider.value;
        GameManager.MoonSelfRotationSpeed = moonSelfRotationSlider.value;
        GameManager.SatelliteStartTravelSpeed = satelliteStartTravelSpeedSlider.value;

        // Update Slider Text
        timeScaleValue.text = Math.Round(timeScale, 1).ToString();
        earthMassValue.text = GameManager.EarthMass.ToString();
        earthSelfRotationValue.text = GameManager.EarthSelfRotationSpeed.ToString();
        moonSelfRotationValue.text = GameManager.MoonSelfRotationSpeed.ToString();
        satelliteStartTravelSpeedValue.text = GameManager.SatelliteStartTravelSpeed.ToString();

        SatelliteCounterText();

        if (timeScale > 15)
            timeScaleInfo.SetActive(true);
        else
            timeScaleInfo.SetActive(false);
    }

    public void SettingsMenu()
    {
        isInSettings = !isInSettings;

        if (isInSettings)
            settingsPanel.SetActive(true);
        else
            settingsPanel.SetActive(false);
    }

    // Triggered with tutorial checkbox
    public void Tutorial()
    {
        tutorialActive = !tutorialActive;

        if (tutorialActive)
            tutorialPanel.SetActive(true);
        else
            tutorialPanel.SetActive(false);
    }

    public void SatelliteCounterText()
    {
        satelliteCounterText.text = satelliteCount.ToString();
    }

    public void DefaultValues()
    {
        // Set GameManager Default Values
        GameManager.EarthMass = 300f;
        GameManager.TimeScale = 10;
        GameManager.EarthSelfRotationSpeed = 1f;
        GameManager.MoonSelfRotationSpeed = 1f;
        GameManager.SatelliteStartTravelSpeed = 3f;

        // Set Slider Default Values
        earthMassSlider.value = GameManager.EarthMass;
        timeScaleSlider.value = GameManager.TimeScale;
        earthSelfRotationSlider.value = GameManager.EarthSelfRotationSpeed;
        moonSelfRotationSlider.value = GameManager.MoonSelfRotationSpeed;
        satelliteStartTravelSpeedSlider.value = GameManager.SatelliteStartTravelSpeed;
    }
}
