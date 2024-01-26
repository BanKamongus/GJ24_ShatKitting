using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip Sound;
    private AudioSource audioSource;

    public bool loop = true;
    public float Volume = 1;
    public float OffsetStart = 0;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = Volume;
        audioSource.clip = Sound;
        audioSource.loop = loop;
        audioSource.time = OffsetStart;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
