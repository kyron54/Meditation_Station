using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSFXBase : MonoBehaviour
{
    public AudioClip[] sounds;

    protected AudioSource soundSource;

    public float minDownTime;
    public float maxDownTime;

    protected int soundDecider = -1;

    protected bool shouldAudioRestart = false;

    protected IEnumerator PlaySound()
    {
        soundSource.Play();

        yield return new WaitForSeconds(Random.Range(minDownTime, maxDownTime));

        shouldAudioRestart = true;
    }

    protected IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(Random.Range(0, minDownTime));
        shouldAudioRestart = true;
    }

    protected void DecideSound()
    {
        if(soundDecider == -1)
        {
            soundDecider = Random.Range(0, sounds.Length);
        }

        soundSource.clip = sounds[soundDecider];
    }
}
