using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSFXBase : MonoBehaviour
{
    public AudioClip[] sounds;

    public AudioSource soundSource;

    public float minDownTime;
    public float maxDownTime;

    protected int soundDecider = 0;

    protected bool shouldAudioRestart = true;

    protected IEnumerator PlaySound()
    {
        soundSource.Play();

        yield return new WaitForSeconds(Random.Range(minDownTime, maxDownTime));

        shouldAudioRestart = true;
    }

    protected void DecideSound()
    {
        if(soundDecider == 0)
        {
            soundDecider = Random.Range(0, sounds.Length - 1);
        }

        for(int i = 0; soundDecider == i + 1; ++i)
        {
            soundSource.clip = sounds[i];
        }
    }
}
