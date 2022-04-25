using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAmbientSounds : MonoBehaviour
{
    public AudioClip[] sounds;

    protected AudioSource soundSource;

    public float minDownTime;
    public float maxDownTime;

    public int soundDecider = -1;

    protected bool shouldAudioRestart = false;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = gameObject.GetComponent<AudioSource>();

        DecideSound();
        StartCoroutine(InitialDelay());
    }

    // Update is called once per frame
    void Update()
    {
        //Only plays sound after some time has passed.
        if (shouldAudioRestart)
        {
            //Immediately makes it so sound doesn't keep playing.
            shouldAudioRestart = false;
            StartCoroutine(PlaySound());
        }
    }

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
        if (soundDecider == -1)
        {
            soundDecider = Random.Range(0, sounds.Length - 1);
        }

        soundSource.clip = sounds[soundDecider];
    }
}
