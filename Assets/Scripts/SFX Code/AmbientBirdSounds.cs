using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientBirdSounds : EnvironmentSFXBase
{
    // Start is called before the first frame update
    void Start()
    {
        DecideSound();
    }

    // Update is called once per frame
    void Update()
    {
        //Only plays sound after some time has passed.
        if(shouldAudioRestart)
        {
            //Immediately makes it so sound doesn't keep playing.
            shouldAudioRestart = false;
            StartCoroutine(PlaySound());
        }
    }
}
