using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInterface : MonoBehaviour
{
    public static GameInterface Instance;

    // Timescale
    [Header("Timescale")]
    [SerializeField] Slider timeScaleSlider;
    [SerializeField] Text timeScaleValue;
    [SerializeField] GameObject timeScaleInfo;
    private static float timeScale;

    // Earth
    [Header("Earth")]
    [SerializeField] Slider earthMassSlider;
    [SerializeField] Text earthMassValue;
    [SerializeField] Slider earthSelfRotationSlider;
    [SerializeField] Text earthSelfRotationValue;
    private static float earthMass;

    // Moon
    [Header("Moon")]
    [SerializeField] Slider moonSelfRotationSlider;
    [SerializeField] Text moonSelfRotationValue;

    // Settings Menu
    [Header("Settings")]
    [SerializeField] GameObject settingsPanel;
    private bool isInSettings = false;

    // Tutorial
    [Header("Tutorial")]
    [SerializeField] GameObject tutorialPanel;
    private bool tutorialActive = true;

    // Satellite
    [Header("Satellite")]
    [SerializeField] Text satelliteCounterText;
    public int satelliteCount = 0;
    [SerializeField] Slider satelliteStartTravelSpeedSlider;
    [SerializeField] Text satelliteStartTravelSpeedValue;
    [SerializeField] Slider satelliteHeightSlider;
    [SerializeField] Text satelliteHeightValue;
    private float destructSatelliteTimer;

    // Destruction Panel
    [Header("Destruction Panel")]
    [SerializeField] GameObject destructionPanel;
    [SerializeField] Text countDownText;
    [SerializeField] GameObject earthExplosion;
    [SerializeField] GameObject explosionLight;
    [SerializeField] GameObject earth;
    private float countDownTime = 60f;
    private float countDown;
    public bool earthDestructed = false;
    private bool laserPlayed = false;

    //Sound
    [Header("Sound")]
    [SerializeField] Slider soundSlider;
    [SerializeField] Text soundValueText;

    // End Screen
    [Header("End Screen")]
    [SerializeField] GameObject endScreenPanel;

    private void Awake()
    {
        Instance = this;
        timeScale = GameManager.TimeScale;
        settingsPanel.SetActive(false);
        destructionPanel.SetActive(false);
        endScreenPanel.SetActive(false);
        earthMassSlider.value = GameManager.EarthMass;
        earthSelfRotationSlider.value = GameManager.EarthSelfRotationSpeed;
        moonSelfRotationSlider.value = GameManager.MoonSelfRotationSpeed;
        satelliteStartTravelSpeedSlider.value = GameManager.SatelliteStartTravelSpeed;
        satelliteHeightSlider.value = GameManager.SatelliteStartHeight;
        countDown = countDownTime;
        
    }

    private void Update()
    {
        if (!GameManager.DestructionSequence)
        {
            timeScale = timeScaleSlider.value;
            Time.timeScale = timeScale;
        }

        // Speichere Values von den Slidern
        GameManager.EarthMass = earthMassSlider.value;
        GameManager.EarthSelfRotationSpeed = earthSelfRotationSlider.value;
        GameManager.MoonSelfRotationSpeed = moonSelfRotationSlider.value;
        GameManager.SatelliteStartTravelSpeed = satelliteStartTravelSpeedSlider.value;
        GameManager.SatelliteStartHeight = satelliteHeightSlider.value;
        GameManager.SoundVolume = soundSlider.value;

        // Update Slider Text
        timeScaleValue.text = Math.Round(timeScale, 1).ToString();
        earthMassValue.text = GameManager.EarthMass.ToString();
        earthSelfRotationValue.text = GameManager.EarthSelfRotationSpeed.ToString();
        moonSelfRotationValue.text = GameManager.MoonSelfRotationSpeed.ToString();
        satelliteStartTravelSpeedValue.text = Math.Round(GameManager.SatelliteStartTravelSpeed, 1).ToString();
        satelliteHeightValue.text = Math.Round(GameManager.SatelliteStartHeight, 1).ToString();
        soundValueText.text = Math.Round(GameManager.SoundVolume).ToString() + " %";

        SatelliteCounterText();
        DestructionPanel();
        DestroyAllSatellitesOnEnd();
        ActivateLaser();

        // Timescale info
        if (timeScale > 15)
            timeScaleInfo.SetActive(true);
        else
            timeScaleInfo.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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
        GameManager.SatelliteStartTravelSpeed = 4f;
        GameManager.SatelliteStartHeight = 1.2f;

        // Set Slider Default Values
        earthMassSlider.value = GameManager.EarthMass;
        timeScaleSlider.value = GameManager.TimeScale;
        earthSelfRotationSlider.value = GameManager.EarthSelfRotationSpeed;
        moonSelfRotationSlider.value = GameManager.MoonSelfRotationSpeed;
        satelliteStartTravelSpeedSlider.value = GameManager.SatelliteStartTravelSpeed;
        satelliteHeightSlider.value = GameManager.SatelliteStartHeight;
    }

    private void DestructionPanel()
    {
        // Visibility
        if (satelliteCount >= 50)
        {
            destructionPanel.SetActive(true);
        }          
        else
        {
            destructionPanel.SetActive(false);
            GameManager.DestructionSequence = false;
            countDown = countDownTime;
            countDownText.text = Math.Round(countDown).ToString();
        }
            
        if(earthDestructed)
            destructionPanel.SetActive(false);

        if (GameManager.DestructionSequence)
        {
            Time.timeScale = 1;
            timeScaleSlider.value = 1;
            timeScaleValue.text = "1";

            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                countDown = 0;
                if (earthDestructed == false)
                {
                    earthDestructed = true;
                    DestructionAndEnd();
                }
            }
                

            countDownText.text = Math.Round(countDown).ToString();
        }
    }

    public void DestructionSequence()
    {
        GameManager.DestructionSequence = true;
    }

    private void DestructionAndEnd()
    {
        explosionLight.SetActive(true);
        earthExplosion.GetComponent<ParticleSystem>().Play();
        SoundManager.instance.PlaySound(1);
        Destroy(earth);
        destructionPanel.SetActive(false);
    }

    private void DestroyAllSatellitesOnEnd()
    {
        if (earthDestructed)
        {
            destructSatelliteTimer += Time.deltaTime;

            if (destructSatelliteTimer >= 3f)
            {
                endScreenPanel.SetActive(true);
                if (satelliteCount > 0)
                {
                    SoundManager.instance.PlaySound(1);
                    SatelliteList.Instance.satellites[satelliteCount - 1].GetComponent<EarthCollision>().Explode();
                    destructSatelliteTimer = 2.4f;
                }
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ActivateLaser()
    {
        if (!earthDestructed)
        {
            if (countDown < 6)
            {
                if (!laserPlayed)
                {
                    SoundManager.instance.PlaySound(3);
                    laserPlayed = true;
                }
                    
                foreach (GameObject satellite in SatelliteList.Instance.satellites)
                {
                    if (countDown > 0.2f)
                    {
                        LineRenderer lr = satellite.GetComponent<LineRenderer>();
                        lr.enabled = true;

                        lr.SetPosition(0, Vector3.zero);
                        lr.SetPosition(1, satellite.transform.position);
                    }
                    else if (countDown <= 0.2f) 
                    {
                        LineRenderer lr = satellite.GetComponent<LineRenderer>();
                        lr.enabled = false;
                    }
                } 
            }
        }
    }
}
