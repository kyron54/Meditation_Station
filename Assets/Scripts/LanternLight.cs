/// <summary>
/// Initial Author: Zayden
/// Name: LanternLight.cs
/// Description: This script detects when an object has tripped the trigger
///              of the lantern object and turns on the light and particles.
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternLight : MonoBehaviour
{
    [SerializeField] private float fadeSeconds;
    [SerializeField] private float rate;
    [SerializeField] private float finalIntensity;

    //Gabriel: Adding to this file to play lantern sounds.
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource soundSource;

    public int soundDecider = -1;

    Light light;
    ParticleSystem particles;

    private void Awake()
    {
        // Get the lantern's light and particle system
        light = gameObject.transform.GetChild(0).GetComponent<Light>();
        particles = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();

        DecideSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Turn of the trigger
            GetComponent<CapsuleCollider>().enabled = false;

            // Start the fade coroutine
            StartCoroutine(LightLantern());

            // Play the particles
            particles.Play();

            // This would be a good place to play the sound effect too
            soundSource.Play();
        }
    }

    IEnumerator LightLantern()
    {
        // Increment the intensity of the light over time
        for (float i = 0; light.intensity < finalIntensity; i += rate)
        {
            float intensity = light.intensity;
            intensity += i;
            light.intensity = intensity;

            yield return new WaitForSecondsRealtime(fadeSeconds);
        }
 
    }

    private void DecideSound()
    {
        if (soundDecider == -1)
        {
            soundDecider = Random.Range(0, sounds.Length);
        }

        soundSource.clip = sounds[soundDecider];
    }
}
