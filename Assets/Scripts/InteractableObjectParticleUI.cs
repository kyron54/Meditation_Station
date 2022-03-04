using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectParticleUI : MonoBehaviour
{
    // Whenever the player picks up the object, remove the particle system UI on it


    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.name == "GrabVolumeBig")
         {
                GetComponent<ParticleSystem>().gameObject.SetActive(false);
         }
    }
}
