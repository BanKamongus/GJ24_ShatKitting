using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip DefaultSound;
    public List<AudioClip> RandomSounds = new List<AudioClip>();
    private AudioSource audioSource;

    public bool loop = true;
    public bool playRandomSound = false; // Toggle for playing random sound
    public float Volume = 1;
    public float OffsetStart = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = Volume;
        audioSource.loop = loop;
        audioSource.time = OffsetStart;

        // Choose which sound to play
        if (playRandomSound && RandomSounds.Count > 0)
        {
            audioSource.clip = RandomSounds[Random.Range(0, RandomSounds.Count)];
        }
        else
        {
            audioSource.clip = DefaultSound;
        }

        audioSource.Play();
    }
}