using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    OVRPlayerController control;

    [SerializeField] private AudioSource normalStep;

    // Start is called before the first frame update
    void Start()
    {
        control = gameObject.GetComponent<OVRPlayerController>();
        normalStep = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Footsteps();
    }

    void Footsteps()
    {
        if(control.Acceleration > 0)
        {
            normalStep.Play();
        }
        else
        {
            normalStep.Pause();
        }
    }
}
