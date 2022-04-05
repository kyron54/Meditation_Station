using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientBirdSounds : MonoBehaviour
{
    //Range of time for sound to play again.
    public float minDownTime = 30f;
    public float maxDownTime = 45f;

    //Decides which bird sound will play, currently between 1-3 are viable
    //options.
    public int soundDecider = 0;

    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;

    //Controls when object's sound can play again.
    private bool audioRestart = true;

    // Start is called before the first frame update
    void Start()
    {
        //If soundDecider hasn't been set in inspector then...
        if(soundDecider == 0)
        {
            //Random value 1-3 is chosen.
            soundDecider = Random.Range(1, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Only plays sound after some time has passed.
        if(audioRestart)
        {
            //Immediately makes it so sound doesn't keep playing.
            audioRestart = false;
            StartCoroutine(PlaySound());
        }
    }

    //Uses the soundDecider to choose an bird sound to play and then waits
    //before it can be played again.
    IEnumerator PlaySound()
    {
        if(soundDecider == 1)
        {
            sound1.Play();
        }
        else if(soundDecider == 2)
        {
            sound2.Play();
        }
        else if(soundDecider == 3)
        {
            sound3.Play();
        }

        //Down time between repeats.
        yield return new WaitForSeconds(Random.Range(minDownTime, maxDownTime));
        //Once time has passed, audio can be played from this object again.
        audioRestart = true;
    }
}
