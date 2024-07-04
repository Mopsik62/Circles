using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource musicSource;

    public AudioClip mergeSound;
    public AudioClip musicSound;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        musicSource.clip = musicSound;
        musicSource.Play();
        Debug.Log("started play music from Unity");
    }


    public void playSound(AudioClip audioClip)
    {
        SFXSource.clip = audioClip;
        SFXSource.Play();
    }

    public void setMute()
    {
        musicSource.volume = 0f;
        SFXSource.volume = 0f;
        //if (musicSource.volume == 0f)
        //{ musicSource.volume = 1f; }
        //else { musicSource.volume = 0f; }
    }
    public void setUnmute ()
    {
        musicSource.volume = 1f;
        SFXSource.volume = 1f;

    }
}
