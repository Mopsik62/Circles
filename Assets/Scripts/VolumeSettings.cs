using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        Debug.Log("Music volume = " + volume);
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        GameManager.instance.SaveSomething("musicVolume", volume);
    }

    // Update is called once per frame
    public void SetSFXVolume()
    {
        float volume = soundSlider.value;
        myMixer.SetFloat("sound", Mathf.Log10(volume) * 20);
        GameManager.instance.SaveSomething("soundVolume", volume);

    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    

        SetMusicVolume();
        SetSFXVolume();
    }

}
