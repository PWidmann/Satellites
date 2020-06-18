using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip explosion;
    public AudioClip buttonClick;
    public AudioClip laserBeam;
    [SerializeField] AudioSource audioSourceFX;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSourceFX.GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSourceFX.volume = Volume(GameManager.SoundVolume);
    }


    public void PlaySound(int index)
    {
        switch (index)
        {
            case 1:
                audioSourceFX.PlayOneShot(explosion);
                break;
            case 2:
                audioSourceFX.PlayOneShot(buttonClick);
                break;
            case 3:
                audioSourceFX.PlayOneShot(laserBeam);
                break;
        }
    }

    public float Volume(float value)
    {
        float soundVolume = (value / 100);
        return soundVolume;
    }
}
