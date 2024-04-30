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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        musicSource.clip = musicSound;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSound(AudioClip audioClip)
    {
        SFXSource.clip = audioClip;
        SFXSource.Play();
    }

    /*public void setMute()
    {
        if (SFXSource.volume == 0f)
        { SFXSource.volume = 1f; }
        else { SFXSource.volume = 0; }
    }*/
}
