using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    public AudioSource audioSource;
    public AudioClip button;
    public AudioClip door;


    public void PlayButton()
    {
        audioSource.PlayOneShot(button);
    }
    public void PlayDoor()
    {
        audioSource.PlayOneShot(door, 0.2f);
    }
}
