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
        musicSource.clip = musicSound;
        musicSource.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
    }


    public void playSound(AudioClip audioClip)
    {
        SFXSource.clip = audioClip;
        SFXSource.Play();
    }

    public void setMute()
    {
        Debug.Log("SetMute");
        musicSource.volume = 0f;
        SFXSource.volume = 0f;
        //if (musicSource.volume == 0f)
        //{ musicSource.volume = 1f; }
        //else { musicSource.volume = 0f; }
    }
    public void setUnmute ()
    {
        Debug.Log("SetUnmute");
        musicSource.volume = 1f;
        SFXSource.volume = 1f;
        musicSource.Play();


    }
}
