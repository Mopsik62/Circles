using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void setMute()
    {
        if (audioSource.volume == 0f)
        { audioSource.volume = 1f; }
        else { audioSource.volume = 0; }
    }
}
