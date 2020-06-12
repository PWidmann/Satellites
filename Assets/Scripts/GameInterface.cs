using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public static GameInterface Instance;

    // Timescale
    [SerializeField] Slider timeScaleSlider;
    private static float timeScale;

    // Settings Menu
    public GameObject settingsPanel;
    private bool isInSettings = false;

    // Tutorial
    public GameObject tutorialPanel;
    private bool tutorialActive = true;

    // Satellite counter
    [SerializeField] Text satelliteCounterText;
    public int satelliteCount = 0;

    public static float TimeScale { get => timeScale; set => timeScale = value; }
    public bool IsInSettings { get => isInSettings; set => isInSettings = value; }

    private void Awake()
    {
        Instance = this;
        timeScale = 50f;
        settingsPanel.SetActive(false);
    }

    private void Update()
    {
        timeScale = timeScaleSlider.value;
        Time.timeScale = timeScale;

        SatelliteCounterText();
    }

    public void SettingsMenu()
    {
        isInSettings = !isInSettings;

        if (isInSettings)
            settingsPanel.SetActive(true);
        else
            settingsPanel.SetActive(false);
    }

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
}
