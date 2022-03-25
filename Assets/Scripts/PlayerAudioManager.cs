using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    OVRPlayerController control;

    [SerializeField] private AudioSource normalStep;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        control = gameObject.GetComponent<OVRPlayerController>();
        normalStep = gameObject.GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Footsteps();
    }

    void Footsteps()
    {
        Vector3 forwardMove = controller.velocity;
        if(controller.velocity.magnitude > 0)
        {
            normalStep.Play();
        }
        else
        {
            normalStep.Pause();
        }
    }
}
