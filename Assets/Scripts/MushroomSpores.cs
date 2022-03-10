using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpores : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GrabVolumeBig")
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
}
