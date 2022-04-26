using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpTrigger : MonoBehaviour
{

    //AudioSource harp;
    private ParticleSystem notes;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource soundSource;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        notes = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            DecideSound();
            soundSource.Play();
            notes.Play();
        }
    }

    private void DecideSound()
    {
        soundSource.clip = sounds[Random.Range(0, sounds.Length)];
    }
}
